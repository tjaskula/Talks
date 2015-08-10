using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Api.Controllers;
using Api.Models;
using Api.Services;
using Api.Validators;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.Tests
{
    [TestClass]
    public class RegisterControllerTests
    {
        private RegisterController _controller;
        private Mock<IRepresentationValidator> _representationValidatorMock;
        private Mock<IRegistrationService> _registrationServiceMock;
        private Mock<IAccountRepository> _accountRepositoryMock;
        private Mock<INotifier> _notifierMock;

        [TestInitialize]
        public void SetupController()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _representationValidatorMock = new Mock<IRepresentationValidator>();
            _registrationServiceMock = new Mock<IRegistrationService>();
            _notifierMock = new Mock<INotifier>();

            _controller = new RegisterController(_accountRepositoryMock.Object, _representationValidatorMock.Object, _registrationServiceMock.Object, _notifierMock.Object);
            _controller.Request = new HttpRequestMessage(HttpMethod.Post, new Uri("http://localhost/api/register"));
            _controller.ControllerContext = new HttpControllerContext(new HttpConfiguration(), new HttpRouteData(new HttpRoute("api/register")), _controller.Request);
        }

        [TestMethod]
        public void RegisterShouldReturnBadRequestWhenNullRepresentation()
        {
            var result = _controller.Register(null) as InvalidModelStateResult;
            var response = result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("The posted body is not valid.", result.ModelState[string.Empty].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void RegisterShouldReturnBadRequestWhenNotMatchingPassword()
        {
            var registerRepresentation = new RegisterRepresentation();

            _representationValidatorMock.Setup(m => m.Validate(registerRepresentation)).Returns(false);

            var result = _controller.Register(registerRepresentation) as InvalidModelStateResult;
            var response = result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("The password format does not match the policy", result.ModelState["password"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void RegisterShouldReturnConflictIfCannotRegister()
        {
            var registerRepresentation = new RegisterRepresentation();

            _representationValidatorMock.Setup(m => m.Validate(registerRepresentation)).Returns(true);
            _registrationServiceMock.Setup(m => m.UserExists(registerRepresentation.Email, It.IsAny<Func<string, Account>>())).Returns(true);

            var result = _controller.Register(registerRepresentation) as ConflictResult;
            var response = result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }

        [TestMethod]
        public void RegisterShouldReturnOkIfAccountCreated()
        {
            var registerRepresentation = new RegisterRepresentation();
            var account = new Account("other@email.com", "pass", "");

            _representationValidatorMock.Setup(m => m.Validate(registerRepresentation)).Returns(true);
            _registrationServiceMock.Setup(m => m.UserExists(registerRepresentation.Email, It.IsAny<Func<string, Account>>())).Returns(false);
            _registrationServiceMock.Setup(m => m.Register(registerRepresentation)).Returns(account);

            var result = _controller.Register(registerRepresentation) as OkResult;
            var response = result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            _accountRepositoryMock.Verify(m => m.Save(account), Times.Once());
        }

        [TestMethod]
        public void RegisterShouldSendActivationNotificationIfSubscribtionMustBeConfirmed()
        {
            var registerRepresentation = new RegisterRepresentation();
            var account = new Account("other@email.com", "pass", "");

            _representationValidatorMock.Setup(m => m.Validate(registerRepresentation)).Returns(true);
            _registrationServiceMock.Setup(m => m.UserExists(registerRepresentation.Email, It.IsAny<Func<string, Account>>())).Returns(false);
            _registrationServiceMock.Setup(m => m.Register(registerRepresentation)).Returns(account);
            _registrationServiceMock.Setup(m => m.ShouldConfirmSubscription(registerRepresentation)).Returns(true);

            var result = _controller.Register(registerRepresentation) as CreatedNegotiatedContentResult<ConfirmationRepresentation>;
            var response = result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(account.Email, result.Content.Email);
            _notifierMock.Verify(m => m.SendActivationNotification(account.Email), Times.Once());
        }
    }
}
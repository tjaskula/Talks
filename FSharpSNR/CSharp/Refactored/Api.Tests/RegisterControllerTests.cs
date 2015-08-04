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

        [TestInitialize]
        public void SetupController()
        {
            var accountRepositoryMock = new Mock<IAccountRepository>();
            _representationValidatorMock = new Mock<IRepresentationValidator>();
            _registrationServiceMock = new Mock<IRegistrationService>();
            var notifierMock = new Mock<INotifier>();

            _controller = new RegisterController(accountRepositoryMock.Object, _representationValidatorMock.Object, _registrationServiceMock.Object, notifierMock.Object);
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
            Func<string, Account> callback = email => new Account("other@email.com", "pass", "");
            _registrationServiceMock.Setup(m => m.CanRegister(registerRepresentation.Email, callback)).Returns(false);

            var result = _controller.Register(registerRepresentation) as ConflictResult;
            var response = result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }
    }
}
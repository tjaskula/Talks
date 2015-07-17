using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Api.Controllers;
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

        [TestInitialize]
        public void SetupController()
        {
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var representationValidatorMock = new Mock<IRepresentationValidator>();
            var registrationServiceMock = new Mock<IRegistrationService>();
            var notifierMock = new Mock<INotifier>();

            _controller = new RegisterController(accountRepositoryMock.Object, representationValidatorMock.Object, registrationServiceMock.Object, notifierMock.Object);
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
    }
}
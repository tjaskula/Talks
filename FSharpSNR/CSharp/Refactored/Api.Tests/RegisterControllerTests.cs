using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
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
        [TestMethod]
        public void RegisterShouldReturnBadRequestWhenNullRepresentation()
        {
            var accountRepositoryMock = new Mock<IAccountRepository>();
            var representationValidatorMock = new Mock<IRepresentationValidator>();
            var registrationServiceMock = new Mock<IRegistrationService>();
            var notifierMock = new Mock<INotifier>();

            var controller = new RegisterController(accountRepositoryMock.Object, representationValidatorMock.Object, registrationServiceMock.Object, notifierMock.Object);
            controller.Request = new HttpRequestMessage(HttpMethod.Post, new Uri("http://localhost/api/register"));
            controller.ControllerContext = new HttpControllerContext(new HttpConfiguration(), new HttpRouteData(new HttpRoute("api/register")), controller.Request);

            var result = controller.Register(null);
            var response = result.ExecuteAsync(new CancellationToken()).Result;

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}

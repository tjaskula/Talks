using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Domain.Commands;
using Presentation.Controllers;
using Presentation.Helpers;

namespace Presentation
{
    public class CustomControllerActivator : IHttpControllerActivator
    {
        private readonly IDispatcher<ICommand> _dispatcher;

        public CustomControllerActivator(IDispatcher<ICommand> dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            if (typeof(StudentEnrollmentFuncController) == controllerType)
                return new StudentEnrollmentFuncController(_dispatcher);

            throw new Exception(string.Format("The controller is not found for the requested type '{0}'.", controllerType));
        }
    }
}
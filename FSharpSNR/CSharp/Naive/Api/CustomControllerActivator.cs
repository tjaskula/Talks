using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Api.Controllers;

namespace Api
{
    public class CustomControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request,
                                      HttpControllerDescriptor controllerDescriptor,
                                      Type controllerType)
        {
            // We should have been using a regular IoC container to resolve dependencies.
            if (typeof(RegisterController) == controllerType)
                return new RegisterController();

            throw new Exception(string.Format("The controller is not found for the requested type '{0}'.",
                                                controllerType));
        }
    }
}
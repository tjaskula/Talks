using System.Web.Http;
using System.Web.Http.Dispatcher;
using Domain.Commands;
using Newtonsoft.Json.Serialization;
using Presentation.Helpers;

namespace Presentation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config, Dispatcher<ICommand> dispatcher)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IHttpControllerActivator), new CustomControllerActivator(dispatcher));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
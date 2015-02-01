using System.Web.Http;
using System.Web.Http.Dispatcher;
using Newtonsoft.Json.Serialization;

namespace Presentation
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var bootstrapper = new Bootstrapper();

            // Web API configuration and services
            config.Services.Replace(typeof(IHttpControllerActivator), new CustomControllerActivator(bootstrapper.RegisterDependencies()));
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
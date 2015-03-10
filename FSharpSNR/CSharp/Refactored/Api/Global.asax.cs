using System.Web;
using System.Web.Http;

namespace Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = Bootstrapper.Register();
            GlobalConfiguration.Configure(config => WebApiConfig.Register(config, container));
        }
    }
}
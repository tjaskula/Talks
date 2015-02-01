using System.Web;
using System.Web.Http;

namespace Presentation
{
    public class WebApiApplication : HttpApplication
    {
        private Bootstrapper _bootstrapper;

        protected void Application_Start()
        {
            _bootstrapper = new Bootstrapper();

            GlobalConfiguration.Configure(conf => WebApiConfig.Register(conf, _bootstrapper.RegisterDependencies()));
        }

        protected void Application_End()
        {
            _bootstrapper.Dispose();
        }
    }
}

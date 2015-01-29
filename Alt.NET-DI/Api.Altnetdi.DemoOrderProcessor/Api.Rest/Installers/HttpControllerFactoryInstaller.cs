using System.Web.Http.Dispatcher;
using Api.Rest.WebApiInfrastructure;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Api.Rest.Installers
{
	public class HttpControllerFactoryInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<IHttpControllerFactory>().Instance(
					new WindsorHttpControllerFactory(container))
					.LifestyleSingleton());
		}
	}
}
using System.Web.Http.Dispatcher;
using Api.Rest.Infrastructure;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Api.Rest.Installers
{
	public class ControllerFactoryInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<IHttpControllerFactory>().Instance(
					new WindsorHttpControllerFactory(container)));
		}
	}
}
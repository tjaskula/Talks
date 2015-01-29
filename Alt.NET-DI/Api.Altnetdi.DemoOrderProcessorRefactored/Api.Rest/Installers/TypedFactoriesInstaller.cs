using Api.Rest.Domain;
using Api.Rest.Infrastructure.HandlerFactories;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Api.Rest.Installers
{
	public class TypedFactoriesInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IHandlerFactory>()
								.AsFactory()
			                   	.LifestyleSingleton());
			container.Register(Component.For<IOrderProcessorFactory>()
								.AsFactory()
								.LifestyleTransient());
			container.Register(Component.For<IOrderShipperFactory>()
								.AsFactory()
								.LifestyleTransient());
		}
	}
}
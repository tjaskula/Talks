using Api.Rest.Application;
using Api.Rest.Infrastructure;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Api.Rest.Installers
{
	public class MessageHandlerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<MessageDispatcher>().LifestyleSingleton(),
								Classes.FromAssemblyContaining(typeof(IMessageHandler<>))
			                   	.BasedOn(typeof(IMessageHandler<>))
								.WithService.FirstInterface()
			                   	.LifestyleTransient());
		}
	}
}
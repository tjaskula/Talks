using Api.Rest.Infrastructure.Logger;
using Api.Rest.Infrastructure.NHibernate;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Api.Rest.Installers
{
	public class LoggerInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<ILogger>()
			                   	.ImplementedBy<Logger>()
			                   	.LifestyleSingleton()
			                   	.DependsOn(Dependency.OnComponent(typeof(ISession), "singletonSession")));
		}
	}
}
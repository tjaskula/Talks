using Api.Rest.Infrastructure.NHibernate;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Api.Rest.Installers
{
	public class AuditorInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<IAuditor>()
				.ImplementedBy<NullAuditor>()
				.LifestyleSingleton(),
				Component.For<IAuditor>()
				.ImplementedBy<Auditor>()
				.LifestyleTransient().Named("defaultAuditor"));
		}
	}
}
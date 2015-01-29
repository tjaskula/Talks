using System;
using Api.Rest.Infrastructure.NHibernate;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Api.Rest.Installers
{
	public class NHibernateInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			var config = DataBaseConfiguration();
			var loggerConfig = DataBaseConfigurationSpecialLoggerConfiguration();

			container.Register(
				For<ISessionFactory>(k => config.BuildSessionFactory())
					.LifestyleSingleton(),
				For<ISessionFactory>(k => loggerConfig.BuildSessionFactory())
					.LifestyleSingleton().Named("loggerSessionFactory"),
				For<ISession>(k => k.Resolve<ISessionFactory>().OpenSession(k.Resolve<IAuditor>("defaultAuditor")))
					.LifestylePerThread(),
				For<ISession>(k => k.Resolve<ISessionFactory>("loggerSessionFactory").OpenSession(k.Resolve<IAuditor>()))
					.LifestyleSingleton().Named("singletonSession"));
		}

		private Configuration DataBaseConfiguration()
		{
			return new Configuration();
		}

		private Configuration DataBaseConfigurationSpecialLoggerConfiguration()
		{
			return new Configuration();
		}

		private static ComponentRegistration<TService> For<TService>(Func<IKernel, TService> factory) where TService : class 
		{
			return Component.For<TService>()
				.UsingFactoryMethod(factory.Invoke);
		}
	}
}
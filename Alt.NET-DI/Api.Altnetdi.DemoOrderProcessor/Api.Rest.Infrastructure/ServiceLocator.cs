using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Api.Rest.Infrastructure
{
	public class ServiceLocator
	{
		private static volatile ServiceLocator _serviceLocator;
		private static readonly object SyncRoot = new Object();
		private static readonly IWindsorContainer Container = new WindsorContainer();

		public static ServiceLocator Current
		{
			get
			{
				if (_serviceLocator == null)
				{
					lock (SyncRoot)
					{
						if (_serviceLocator == null)
							_serviceLocator = new ServiceLocator();
					}
				}

				return _serviceLocator;
			}
		}

		public void Register<TFor, TImplementation>() where TFor : class where TImplementation : class, TFor
		{
			Container.Register(Component.For<TFor>().ImplementedBy<TImplementation>());
		}

		public T GetService<T>()
		{
			return Container.Resolve<T>();
		}
	}
}
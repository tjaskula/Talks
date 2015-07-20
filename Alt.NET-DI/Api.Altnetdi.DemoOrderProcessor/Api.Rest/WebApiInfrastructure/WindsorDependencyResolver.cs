using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using Castle.Windsor;
using IDependencyResolver = System.Web.Http.Services.IDependencyResolver;

namespace Api.Rest.WebApiInfrastructure
{
	public class WindsorDependencyResolver : IDependencyResolver
	{
		public static IWindsorContainer WindsorContainer;

		public WindsorDependencyResolver(IWindsorContainer windsorContainer)
		{
			if (windsorContainer == null)
				throw new ArgumentNullException("windsorContainer", "The instance of the container cannot be null.");

			WindsorContainer = windsorContainer;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return WindsorContainer.Resolve(serviceType);
			}
			catch (ComponentNotFoundException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return WindsorContainer.ResolveAll(serviceType).Cast<object>();
			}
			catch (ComponentNotFoundException)
			{
				return new List<object>();
			}
		}
	}
}
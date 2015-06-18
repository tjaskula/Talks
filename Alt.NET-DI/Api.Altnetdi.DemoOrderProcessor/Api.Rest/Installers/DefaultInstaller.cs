using System;
using Api.Rest.Application.OrderProcessing;
using Api.Rest.Infrastructure;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Api.Rest.Installers
{
	public class DefaultInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(Environment.CurrentDirectory))
			                   	.Where(t => t.Namespace == typeof (IAuthenticationService).Namespace
                                        // In the normal application this is the way to do. However we have here for the excercice purpose 3 different implementations that we have to
                                        // activate manually because otherwise the last registered wins.
										//|| t.Namespace == typeof(OrderProcessor).Namespace 
                                        || t.Namespace == typeof(AccountsReceivable).Namespace
										|| t.Namespace == "Api.Rest.Data")
								.WithService.FirstInterface()
								.LifestyleTransient());
		}
	}
}
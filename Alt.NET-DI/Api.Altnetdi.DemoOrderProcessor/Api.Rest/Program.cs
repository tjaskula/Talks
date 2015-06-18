using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Api.Rest.Application.OrderProcessing;
using Api.Rest.Application.OrderProcessing.Processors;
using Api.Rest.Domain.Processors;
using Api.Rest.WebApiInfrastructure;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Api.Rest
{
	public class Program
	{
		static void Main(string[] args)
		{
			var container = new WindsorContainer();
			var kernel = container.Kernel;
			kernel.Resolver.AddSubResolver(new CollectionResolver(kernel));
			kernel.AddFacility<TypedFactoryFacility>();
			container.Install(FromAssembly.This());
			//OrderShipperFactory.CreationClosure = () => container.Resolve<IOrderShipper>();

            // for the example purpose we will register manually. If you're interested on how it should be done properly
            // please look into the DefaultInstaller class.
            // please chose one implementation of the IOrderProcessor
		    container.Register(Component.For<IOrderProcessor>().ImplementedBy<OrderProcessor>().LifestyleTransient());
            //container.Register(Component.For<IOrderProcessor>().ImplementedBy<OrderProcessorContainer>().LifestyleTransient());
            //container.Register(Component.For<IOrderProcessor>().ImplementedBy<OrderProcessorServiceLocator>().LifestyleTransient());

			var config = new HttpSelfHostConfiguration("http://localhost:6666");
			config.ServiceResolver.SetResolver(new WindsorDependencyResolver(container));
			config.Routes.MapHttpRoute("defualt", "{controller}/{id}", new { id = RouteParameter.Optional });
			
			using (var server = new HttpSelfHostServer(config))
			{
				server.OpenAsync().Wait();

				Console.WriteLine("The server is running.");
				Console.ReadLine();
			}

			container.Dispose();
			GC.Collect();
		}
	}
}
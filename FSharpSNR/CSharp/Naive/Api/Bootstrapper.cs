using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Api
{
    public class Bootstrapper
    {
        public static WindsorContainer Register()
        {
            var container = new WindsorContainer();
            var kernel = container.Kernel;
            kernel.Resolver.AddSubResolver(new CollectionResolver(kernel));
            kernel.AddFacility<TypedFactoryFacility>();
            container.Install(FromAssembly.This());

            return container;
        }
    }
}
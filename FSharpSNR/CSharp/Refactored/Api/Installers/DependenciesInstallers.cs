using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Domain;
using Infrastructure.EF;
using Api.Mappers;
using Api.Services;
using Api.Validators;

namespace Api.Installers
{
    public class DependenciesInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRegistrationService>().ImplementedBy<RegistrationService>());
            container.Register(Component.For<IAccountRepository>().ImplementedBy<SqlAccountRepository>());
            container.Register(Component.For<IRegistrationMapper>().ImplementedBy<RegistrationMapper>());
            container.Register(Component.For<IRepresentationValidator>().ImplementedBy<RepresentationValidator>());
            container.Register(Component.For<ICryptographer>().ImplementedBy<Cryptographer>());
            container.Register(Component.For<INotifier>().ImplementedBy<Notifier>());
            container.Register(Component.For<RegistrationContext>());
        }
    }
}
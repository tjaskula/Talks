namespace Api

module Services =

    open System.Web.Http.Controllers
    open System.Web.Http.Dispatcher
    open Controllers

    type CustomControllerActivator() =
        interface IHttpControllerActivator with
            member x.Create(request, controllerDescriptor, controllerType) =
                new RegisterController(UseCases.Registration.start) :> IHttpController
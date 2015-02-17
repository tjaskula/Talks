namespace Api

module Controllers =

    open System.Web.Http
    open Representations
    open Common
    open Domain
    open System

    type RegisterController
        (
            startProcess : RegisterRepresentation -> Result<Account, Error>
        ) =
        inherit ApiController()
    
        [<HttpPost>]
        [<Route("api/register")>]
        member x.Register(representation : RegisterRepresentation) =

            let confirmationUrl = Uri(x.Request.RequestUri, "/confirmation");

            match startProcess representation with
                | Success s -> x.Ok() :> IHttpActionResult
                | Failure f -> x.BadRequest() :> IHttpActionResult
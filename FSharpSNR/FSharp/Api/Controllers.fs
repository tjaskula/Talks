namespace Api

module Controllers =

    open System.Web.Http
    open Representations
    open Common

    type RegisterController
        (
            startProcess : RegisterRepresentation -> _
        ) =
        inherit ApiController()
    
        [<HttpPost>]
        [<Route("api/register")>]
        member x.Register(representation : RegisterRepresentation) =
                    
                    startProcess representation |> ignore

                    match x.ModelState.IsValid with
                        | true -> x.Ok() :> IHttpActionResult
                        | false -> x.BadRequest() :> IHttpActionResult
                    
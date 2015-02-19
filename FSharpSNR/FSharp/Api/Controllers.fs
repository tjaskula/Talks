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
                | Success s -> match s.Confirmation with
                                | Some c -> match c.Activation with
                                                | Some a -> x.Created(confirmationUrl,
                                                                        { 
                                                                            Email = fst (getEmail s)
                                                                            Code = a.ActivationCode 
                                                                            ExpirationTime = a.ActivationCodeExpirationTime
                                                                        }) :> IHttpActionResult
                                                | None -> x.Ok() :> IHttpActionResult
                                | None -> x.Ok() :> IHttpActionResult
                | Failure f -> match f with
                                | ValidationError ve -> x.ModelState.AddModelError("", ve)
                                                        x.BadRequest(x.ModelState) :> IHttpActionResult
                                | AccountExists _ -> x.Conflict() :> IHttpActionResult
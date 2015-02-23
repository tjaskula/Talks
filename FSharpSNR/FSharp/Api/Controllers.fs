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
                                | DatabaseError de -> x.InternalServerError(Exception(de)) :> IHttpActionResult

        [<HttpPost>]
        [<Route("api/register")>]
        member x.Register2(representation : RegisterRepresentation) =
 
            let confirmationUrl = Uri(x.Request.RequestUri, "/confirmation");
 
            match startProcess representation with
                | Success s -> 
                    let rsp = 
                        maybeRes
                            {
                                let! confirmation = s |> (fun a -> a.Confirmation) 
                                let! activation = confirmation |> (fun c -> c.Activation)
                                let! r = (s, activation) |> (fun (ss, a) -> Some (getConfirmationRepresentation (fst (getEmail ss)) a))
                                return r
                            }
                    match rsp with
                    | Some r -> x.Created(confirmationUrl, r) :> IHttpActionResult
                    | None -> x.Ok() :> IHttpActionResult
                
                | Failure f -> 
                    match f with
                    | ValidationError ve -> x.ModelState.AddModelError("", ve)
                                            x.BadRequest(x.ModelState) :> IHttpActionResult
                    | AccountExists _ -> x.Conflict() :> IHttpActionResult
                    | DatabaseError de -> x.InternalServerError(Exception(de)) :> IHttpActionResult
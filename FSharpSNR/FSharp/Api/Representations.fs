namespace Api
 
module Representations =
 
    open System
    open System.ComponentModel.DataAnnotations
 
    [<CLIMutable>]
    type ConfirmationRepresentation =
        {
            [<Required>]
            [<EmailAddress>]
            Email : string
 
            [<Required>]
            Code : Guid
 
            [<Required>]
            ExpirationTime : DateTime
        }
    
    [<CLIMutable>]
    type RegisterRepresentation =
        {
            [<Required>]
            [<EmailAddress>]
            Email : string
 
            [<Required>]
            Password : string
 
            [<Required>]
            Privacy : bool
 
            [<Required>]
            Provider : string
 
            Confirmation : ConfirmationRepresentation
        }
        
 
module Validation =
 
    open Domain
    open Representations
 
    let passwordPolicy registerRepresentation =
                    
        match registerRepresentation.Password with
            | Match @"(?!.*\s)[0-9a-zA-Z!@#\\$%*()_+^&amp;}{:;?.]*$" _ ->
                    registerRepresentation |> Success
            | _ ->  ValidationError("The password format does not match the policy") |> Failure
 
    let checkPrivacy registrationPresentation =
 
        if registrationPresentation.Privacy then
            registrationPresentation |> Success
        else
            ValidationError("User must check the privacy") |> Failure
    
    let concatErrors errors =
        errors
            |> List.map (fun e -> match e with 
                                    | ValidationError str | AccountExists str -> str)
            |> List.fold (fun state str -> state + ";" + str) ""
            |> ValidationError
 
    // create a "plus" function for validation functions
    let (&&&) v1 v2 = 
        let addSuccess r1 r2 = r1 // return first
        let addFailure s1 s2 = 
                        concatErrors [s1; s2]  // concat
        plus addSuccess addFailure v1 v2 
 
    let validateAll =
        passwordPolicy &&& checkPrivacy
 
    let normalizeEmail registerRepresentation =
        {
            Account.Email = registerRepresentation.Email.Trim().ToLower() |> EmailAddress |> Unverified
            Password = registerRepresentation.Password
            Provider = registerRepresentation.Provider
            Confirmation = None
        }
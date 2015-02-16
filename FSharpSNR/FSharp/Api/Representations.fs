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

    let account registerRepresentation =
                    
        match registerRepresentation.Password with
            | Match @"(?!.*\s)[0-9a-zA-Z!@#\\$%*()_+^&amp;}{:;?.]*$" _ ->
                    {
                        Account.Email = registerRepresentation.Email |> EmailAddress |> Unverified
                        Password = registerRepresentation.Password
                        Provider = registerRepresentation.Provider
                        Confirmation = None
                    } |> Success
            | _ ->  ValidationError("The password format does not match the policy") |> Failure
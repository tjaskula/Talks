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
       
        let isConfirmed = match registerRepresentation.Provider with
                            | "OAuth" -> true
                            | _ -> false
        
        match registerRepresentation.Password with
            | Match @"(?!.*\s)[0-9a-zA-Z!@#\\$%*()_+^&amp;}{:;?.]*$" _ -> 
                    Success {
                                Email = registerRepresentation.Email
                                Password = registerRepresentation.Password
                                Provider = registerRepresentation.Provider
                                IsEmailConfirmed = isConfirmed
                                ActivationCode = None
                                ActivationCodeExpirationTime = None
                                ConfirmedOn = None
                            }
            | _ ->  ValidationError("The password format is not correct") |> Failure
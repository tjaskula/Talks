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
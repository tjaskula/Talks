namespace Api

module Domain =
    
    open System

    type Account =
        {
            Email : string
            Password : string
            Provider : string
            IsEmailConfirmed : bool
            ActivationCode : Guid
            ActivationCodeExpirationTime : DateTime option
            ConfirmedOn : DateTime option
        }
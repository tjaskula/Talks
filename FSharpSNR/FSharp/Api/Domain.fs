namespace Api

module Domain =
    
    open System

    [<StructuralEquality;NoComparison>]
    type ConfirmationInfo =
        {
            ActivationCode : Guid
            ActivationCodeExpirationTime : DateTime
            ConfirmedOn : DateTime
        }

    type EmailAddress = EmailAddress of string

    type VerifiedEmailAddress = VerifiedEmail of EmailAddress

    type EmailAddressContactInfo =
        | Unverified of EmailAddress
        | Verified of VerifiedEmailAddress
        
    [<NoEquality;NoComparison>]
    type Account =
        {
            Email : EmailAddressContactInfo
            Password : string
            Provider : string
            Confirmation : ConfirmationInfo option
        }
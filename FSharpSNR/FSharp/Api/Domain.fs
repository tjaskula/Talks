namespace Api

module Domain =
    
    open System
 
    type Error =
        | ValidationError of string
        | AccountExists of string
 
    [<StructuralEquality;NoComparison>]
    type ActivationInfo =
        {
            ActivationCode : Guid
            ActivationCodeExpirationTime : DateTime
        }
 
    [<StructuralEquality;NoComparison>]
    type ConfirmationInfo =
        {
            Activation : ActivationInfo option
            ConfirmedOn : DateTime option
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
 
    let getEmail account =
        match account.Email with
            | Unverified (EmailAddress e) -> e, false
            | Verified (VerifiedEmail (EmailAddress e)) -> e, true
namespace Api
 
module RegistrationService =
 
    open System
    open Domain
    
    let tryConfirmEmail account =
        match account.Email with
            | Unverified e when account.Provider = "OAuth" -> 
                {account with Email = e |> VerifiedEmail |> Verified}|> Success
            | Unverified _ -> account |> Success
            | Verified _ -> 
                    "Email can't be verified at this step"
                     |> ValidationError
                     |> Failure
 
    let setActivationCode account =
        match account.Email with
            | Verified _ -> {account with Confirmation = 
                                                Some({ 
                                                        Activation = None
                                                        ConfirmedOn = Some(DateTime.Now)
                                                     }) 
                            } |> Success
            | Unverified _ -> {account with Confirmation = 
                                                Some({
                                                        Activation = Some({
                                                                            ActivationCode = Guid.NewGuid()
                                                                            ActivationCodeExpirationTime = DateTime.Now
                                                                        })
                                                        ConfirmedOn = None
                                                     }) 
                              } |> Success
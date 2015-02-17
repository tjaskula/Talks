namespace Api
 
module RegistrationService =
 
    open System
    open Domain
    
    let shouldConfirmEmail account =
        match account.Provider, account.Email with
            | "OAuth", Unverified e -> 
                                        {
                                            Email = e |> VerifiedEmail |> Verified
                                            Password = account.Password
                                            Provider = account.Provider
                                            Confirmation = None
                                        } |> Success
            | "OAuth", Verified _ -> account |> Success
            | _ -> account |> Success
 
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
namespace Api

module RegistrationService =

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
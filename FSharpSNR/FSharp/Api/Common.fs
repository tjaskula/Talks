namespace Api

[<AutoOpen>]
module Common =
    
    // the two-track type
    type Result<'TSuccess,'TFailure> = 
        | Success of 'TSuccess
        | Failure of 'TFailure

    type Error =
        | ValidationError of string

    // convert a dead-end function into a one-track function
    let tee f x = 
        f x; x 

    // convert a one-track function into a switch with exception handling
//    let tryCatch f exnHandler x =
//        try
//            f x |> succeed
//        with
//        | ex -> exnHandler ex |> fail
namespace Api

[<AutoOpen>]
module Common =
    
    open System.Text.RegularExpressions
    
    // the two-track type
    type Result<'TSuccess,'TFailure> = 
        | Success of 'TSuccess
        | Failure of 'TFailure

    type Error =
        | ValidationError of string

    // applies a function to a successful result to transform the value
    let map f x =
        match x with
            | Success s -> Success(f s)
            | Failure f -> Failure f

    let bind f x =
        match x with
            | Success s -> f s
            | Failure f -> Failure f

    // convert a dead-end function into a one-track function
    let tee f x =
        f x |> ignore
        x

    // convert a one-track function into a switch with exception handling
    let tryCatch f exnHandler x =
        try
            f x |> Success
        with
        | ex -> exnHandler ex |> Failure

    let (|Match|_|) regex str =
        let m = Regex(str).Match(regex)
        match m.Success with
            | true -> Some (List.tail [for g in m.Groups -> g.Value]) // Note the List.tail, since the first group is always the entirety of the matched string.
            | false -> None
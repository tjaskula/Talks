namespace Api

[<AutoOpen>]
module Common =
    
    open System.Text.RegularExpressions
    
    // the two-track type
    type Result<'TSuccess,'TFailure> = 
        | Success of 'TSuccess
        | Failure of 'TFailure

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

    // add two switches in parallel
    let plus addSuccess addFailure switch1 switch2 x = 
        match (switch1 x),(switch2 x) with
        | Success s1,Success s2 -> Success (addSuccess s1 s2)
        | Failure f1,Success _  -> Failure f1
        | Success _ ,Failure f2 -> Failure f2
        | Failure f1,Failure f2 -> Failure (addFailure f1 f2)

    let bindResult f predicate failureFunc x =
        let res = f x
        if (predicate res) then
            x |> Success
        else
            failureFunc x |> Failure

    let (|Match|_|) regex str =
        let m = Regex(regex).Match(str)
        match m.Success with
            | true -> Some (List.tail [for g in m.Groups -> g.Value]) // Note the List.tail, since the first group is always the entirety of the matched string.
            | false -> None
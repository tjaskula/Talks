// Learn more about F# at http://fsharp.org

open System
open FizzBuzz

[<EntryPoint>]
let main argv = 
    //printfn "Hello World!"
    //printfn "%A" argv

    [1..100]
    |> Seq.map FizzBuzz
    |> Seq.iter (printfn "%s")

    0 // return an integer exit code

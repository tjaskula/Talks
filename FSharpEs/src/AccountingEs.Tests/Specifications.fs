[<AutoOpen>]
module Specifications

open FsUnit.Xunit
open Accounting

let Given (events: Event list) = events
let When (command: Command) events = events, command
let Expect (expected: Event list) (events, command) =
    printGiven events
    printWhen command
    printExpect expected

    events
    |> List.fold apply Uninitialized
    |> handle command
    |> should equal expected

let ExpectThrows<'Ex> (events, command) =
    printGiven events
    printWhen command
    printExpectThrows typeof<'Ex>


    (fun () ->
        events
        |> List.fold apply Uninitialized
        |> handle command
        |> ignore)
    |> should throw typeof<'Ex>
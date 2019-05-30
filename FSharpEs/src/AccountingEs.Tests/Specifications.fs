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
    |> replay Uninitialized
    |> snd
    |> handle command
    |> should equal expected
    
let WhenQuery asAt events = events, asAt

let ExpectAsAt (expected: Account) (events, asAt) =
    printGiven events

    events
    |> replayAsAt Uninitialized asAt
    |> snd
    |> should equal expected

let ExpectThrows<'Ex> (events, command) =
    printGiven events
    printWhen command
    printExpectThrows typeof<'Ex>


    (fun () ->
        events
        |> replay Uninitialized
        |> snd
        |> handle command
        |> ignore)
    |> should throw typeof<'Ex>
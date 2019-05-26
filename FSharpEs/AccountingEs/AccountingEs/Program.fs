open System
open Accounting
open AccountingEs
open InMemoryEventStore
open CommandHandler

[<EntryPoint>]
let main argv =
    
    let store = create()
    let handle = Portfolio.create (readStream store) (appendToStream store)
    
    handle (OpenAccount {AccountId = AccountId 1})
    
    Console.ReadLine() |> ignore
    
    0 // return an integer exit code

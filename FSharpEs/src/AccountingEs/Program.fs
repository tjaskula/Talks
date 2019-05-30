open System
open Accounting
open AccountingEs
open InMemoryEventStore
open CommandHandler

[<EntryPoint>]
let main argv =
    
    let eventHandler = new EventHandler()
    
    use store = 
        create()
        |> subscribe eventHandler.Handle

    let handle = Portfolio.create (readStream store) (appendToStream store)
    
    let accountId = AccountId 1
    let metadata = {RecordDate = DateTime.Now; ValidityDate = DateTime.Now}
    handle (OpenAccount { Dates = metadata; AccountId = accountId; Owner = "Tomasz"})
    handle (Deposit { Dates = metadata; AccountId = accountId; Amount = 1000M})
    handle (Withdraw { Dates = metadata; AccountId = accountId; Amount = 500M})
    handle (Withdraw { Dates = metadata; AccountId = accountId; Amount = 501M})
    
    Console.ReadLine() |> ignore
    
    0 // return an integer exit code

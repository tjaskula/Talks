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
    handle (OpenAccount {AccountId = accountId; Owner = "Tomasz"})
    handle (Deposit {AccountId = accountId; Amount = 1000M})
    handle (Withdraw {AccountId = accountId; Amount = 500M})
    handle (Withdraw {AccountId = accountId; Amount = 501M})
    
    Console.ReadLine() |> ignore
    
    0 // return an integer exit code

namespace AccountingEs

open Accounting

type EventHandler() =
    
    let mutable balance = 0M
    
    let accountId = function AccountId id -> id
    
    member this.Handle =
        function
        | Opened a ->
            printfn "Opened account '%d'" (accountId a.AccountId)
            printfn "The current balance for account '%d' is '%M' euros" (accountId a.AccountId) balance
        | Deposited a ->
            balance <- balance + a.Amount
            printfn "Deposited '%M' euros on account '%d'" a.Amount (accountId a.AccountId)
            printfn "The current balance for account '%d' is '%M' euros" (accountId a.AccountId) balance
        | Withdrawn a ->
            balance <- balance - a.Amount
            printfn "Withdrawn '%M' euros from account '%d'" a.Amount (accountId a.AccountId)
            printfn "The current balance for account '%d' is '%M' euros" (accountId a.AccountId) balance
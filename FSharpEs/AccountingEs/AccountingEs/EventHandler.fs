namespace AccountingEs

open Accounting

type EventHandler() =
    
    let accountId = function AccountId id -> id
    
    member this.Handle =
        function
        | Opened a ->
            printfn "Opened account '%d'" (accountId a.AccountId)
        | Deposited a ->
            printfn "Deposited '%M' euros on account '%d'" a.Amount (accountId a.AccountId)
        | Withdrawn a ->
            printfn "Withdrawn '%M' euros from account '%d'" a.Amount (accountId a.AccountId)
namespace AccountingEs

open Accounting

type EventHandler() =
    
    let accountId = function AccountId id -> id
    
    member this.Handle =
        function
        | Opened a ->
            printfn "Opened account '%d'" (accountId a.AccountId)
            printfn "The current balance for account '%d' is '%M' euros" (accountId a.AccountId) 0M
        | Deposited a ->
            printfn "Deposited '%M' euros on account '%d'" a.Amount (accountId a.AccountId)
            printfn "The current balance for account '%d' is '%M' euros" (accountId a.AccountId) a.CurrentBalance
        | Withdrawn a ->
            printfn "Withdrawn '%M' euros from account '%d'" a.Amount (accountId a.AccountId)
            printfn "The current balance for account '%d' is '%M' euros" (accountId a.AccountId) a.CurrentBalance
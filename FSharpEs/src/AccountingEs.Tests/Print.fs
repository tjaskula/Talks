[<AutoOpen>]
module Print
open Accounting

let accountId = function AccountId id -> id 
let printEvent (event: Event) =
    match event with
    | Opened e -> sprintf "The account n° '%i' has been opened for '%s'" (accountId e.AccountId) e.Owner
    | Deposited e -> sprintf "Deposited '%M' euros on the account n° '%i'" e.Amount (accountId e.AccountId)
    | Withdrawn e -> sprintf "Withdrawn '%M' euros from the account n° '%i'" e.Amount (accountId e.AccountId)
    
let printCommand (command: Command) =
    match command with
    | OpenAccount c -> sprintf "Open account n° '%i' for '%s'" (accountId c.AccountId) c.Owner
    | Deposit c -> sprintf "Deposit '%M' euros on the account n° '%i'" c.Amount (accountId c.AccountId)
    | Withdraw c -> sprintf "Withdraw '%M' euros from the account n° '%i'" c.Amount (accountId c.AccountId)

let printGiven events =
    printfn "Given"
    events 
    |> List.map printEvent
    |> List.iter (printfn "\t%s")
   
let printWhen command =
    printfn "When"
    command |> printCommand  |> printfn "\t%s"

let printExpect events =
    printfn "Expect"
    events 
    |> List.map printEvent
    |> List.iter (printfn "\t%s")

let printExpectThrows ex =
    printfn "Expect"
    printfn "\t%A" ex
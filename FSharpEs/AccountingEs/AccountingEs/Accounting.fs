module Accounting

type AccountId = AccountId of int

// Commands
type Command =
    | OpenAccount of OpenAccount
    | Deposit of Deposit
    | Withdraw of Withdraw

and OpenAccount = {
    AccountId: AccountId }

and Deposit = {
    AccountId: AccountId
    Amount: int }

and Withdraw = {
    AccountId: AccountId
    Amount: int }

// Domain Events
type Event =
    | Opened of Opened
    | Deposited of Deposited
    | Withdrawn of Withdrawn

and Opened = {
    AccountId: AccountId }

and Deposited = {
    AccountId: AccountId
    Amount: decimal }

and Withdrawn = {
    AccountId: AccountId
    Amount: decimal }

// Domain types
type Account =
    | Uninitialized
    | Checking of CheckingAccount

and CheckingAccount = {
    AccountId: AccountId
    Balance: decimal }

// Applies state changes for events
let apply state event =
    match state, event with
    | Uninitialized, Opened e -> Checking {AccountId = e.AccountId; Balance = 0M}
    | Checking account, Deposited {AccountId = _; Amount = amount} -> Checking { account with Balance = account.Balance + amount }
    | Checking account, Withdrawn {AccountId = _; Amount = amount} -> Checking { account with Balance = account.Balance - amount }
    | _ -> state

// Replay function
let replay initialState events =
    let foldLeft events = Seq.fold (fun (version, state) event -> version + 1, apply state event) (0, initialState) events
    events
    |> foldLeft
    
// Map commands to aggregates operations
let handle =
    function
    | OpenAccount command -> (fun (a: Account) -> List.empty<Event>)
    | Deposit command -> (fun (a: Account) -> List.empty<Event>)
    | Withdraw command -> (fun (a: Account) -> List.empty<Event>)
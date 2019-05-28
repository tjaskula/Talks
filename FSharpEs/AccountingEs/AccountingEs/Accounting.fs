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
    Amount: decimal }

and Withdraw = {
    AccountId: AccountId
    Amount: decimal }

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

// Operations on Account aggregate
let openAccount (command: OpenAccount) state =
    match state with
    | Uninitialized -> [ Opened { AccountId = command.AccountId } ]
    | Checking _ -> invalidOp "You cannot open already open account"
    
let deposit (command: Deposit) state =
    match state with
    | Uninitialized -> invalidOp "You cannot deposit money without opening an account"
    | Checking _ when command.Amount < 0M -> invalidOp "Amount has to be positive"
    | Checking a -> [ Deposited {AccountId = a.AccountId; Amount = a.Balance + command.Amount} ]
    
let withdraw (command: Withdraw) state =
    match state with
    | Uninitialized -> invalidOp "You cannot withraw money without opening an account"
    | Checking _ when command.Amount < 0M -> invalidOp "Amount has to be positive"
    | Checking a when a.Balance - command.Amount < 0M -> invalidOp "Overdraft not allowed"
    | Checking a -> [ Deposited {AccountId = a.AccountId; Amount = a.Balance + command.Amount} ]

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
    | OpenAccount command -> openAccount command
    | Deposit command -> deposit command
    | Withdraw command -> withdraw command
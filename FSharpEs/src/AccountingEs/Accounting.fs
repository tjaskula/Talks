module Accounting
open System

type AccountId = AccountId of int

type Dates = {
    RecordDate: DateTime
    ValidityDate: DateTime}

// Commands
type Command =
    | OpenAccount of OpenAccount
    | Deposit of Deposit
    | Withdraw of Withdraw

and OpenAccount = {
    Dates: Dates
    Owner: string
    AccountId: AccountId }

and Deposit = {
    Dates: Dates
    AccountId: AccountId
    Amount: decimal }

and Withdraw = {
    Dates: Dates
    AccountId: AccountId
    Amount: decimal }

// Domain Events
type Event =
    | Opened of Opened
    | Deposited of Deposited
    | Withdrawn of Withdrawn
    member this.Dates =
        match this with
        | Opened e -> e.Dates
        | Deposited e -> e.Dates
        | Withdrawn e -> e.Dates

and Opened = {
    Dates: Dates
    Owner: string
    AccountId: AccountId }

and Deposited = {
    Dates: Dates
    AccountId: AccountId
    CurrentBalance: decimal
    Amount: decimal }

and Withdrawn = {
    Dates: Dates
    AccountId: AccountId
    CurrentBalance: decimal
    Amount: decimal }

// Domain types
type Account =
    | Uninitialized
    | Active of CheckingAccount

and CheckingAccount = {
    Owner: string
    AccountId: AccountId
    Balance: decimal }

// Operations on Account aggregate
let openAccount (command: OpenAccount) state =
    match state with
    | Uninitialized ->
        let metadata = {RecordDate = command.Dates.RecordDate; ValidityDate = command.Dates.ValidityDate}
        [ Opened { Dates = metadata; AccountId = command.AccountId; Owner = command.Owner } ]
    | Active _ -> invalidOp "You cannot open already open account"
    
let deposit (command: Deposit) state =
    match state with
    | Uninitialized -> invalidOp "You cannot deposit money without opening an account"
    | Active _ when command.Amount < 0M -> invalidOp "Amount has to be positive"
    | Active a ->
        let metadata = {RecordDate = command.Dates.RecordDate; ValidityDate = command.Dates.ValidityDate}
        [ Deposited {Dates = metadata; AccountId = a.AccountId; CurrentBalance = a.Balance + command.Amount; Amount = command.Amount} ]
    
let withdraw (command: Withdraw) state =
    match state with
    | Uninitialized -> invalidOp "You cannot withdraw money without opening an account"
    | Active _ when command.Amount < 0M -> invalidOp "Amount has to be positive"
    | Active a when a.Balance - command.Amount < 0M -> invalidOp "Overdraft not allowed"
    | Active a ->
        let metadata = {RecordDate = command.Dates.RecordDate; ValidityDate = command.Dates.ValidityDate}
        [ Withdrawn {Dates = metadata; AccountId = a.AccountId; CurrentBalance = a.Balance - command.Amount; Amount = command.Amount} ]

// Applies state changes for events
let apply state event =
    match state, event with
    | Uninitialized, Opened e -> Active {AccountId = e.AccountId; Balance = 0M; Owner = e.Owner}
    | Active account, Deposited {AccountId = _; Amount = amount} -> Active { account with Balance = account.Balance + amount }
    | Active account, Withdrawn {AccountId = _; Amount = amount} -> Active { account with Balance = account.Balance - amount }
    | _ -> state

// Replay functions
let replay initialState events =
    let foldLeft events = Seq.fold (fun (version, state) event -> version + 1, apply state event) (-1, initialState) events
    events
    |> foldLeft

let replayAsAt initialState asAtDate events =
    events
    |> Seq.filter (fun (event: Event) -> event.Dates.RecordDate <= asAtDate)
    |> replay initialState
    
let replayAsOf initialState asAtDate asOfDate events =
    events
    |> Seq.filter (fun (event: Event) -> event.Dates.RecordDate <= asAtDate && event.Dates.ValidityDate <= asOfDate)
    |> Seq.sortBy (fun (event) -> event.Dates.ValidityDate)
    |> replay initialState
    
// Map commands to aggregates operations
let handle =
    function
    | OpenAccount command -> openAccount command
    | Deposit command -> deposit command
    | Withdraw command -> withdraw command
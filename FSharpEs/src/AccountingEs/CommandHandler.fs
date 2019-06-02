module AccountingEs.CommandHandler

open Accounting

let accountId =
    function
    | OpenAccount { AccountId = AccountId id } -> id
    | Deposit { AccountId = AccountId id }  -> id
    | Withdraw { AccountId = AccountId id }  -> id

module Portfolio =
    
    // this implementation is inspired by https://github.com/thinkbeforecoding/FsUno
    let create readStream appendToStream =

        // this is the "repository"
        let streamId accountId = sprintf "Account-%d" accountId
        let load accountId command =
            readStream (streamId accountId)
            |> match command with
               | Now -> replay Uninitialized
               | AsAt -> replayAsAt Uninitialized command.Dates.RecordDate
               | AsOf -> replayAsOf Uninitialized command.Dates.RecordDate command.Dates.ValidityDate

        let save accountId expectedVersion events = appendToStream (streamId accountId) expectedVersion events

        // the mapsnd function works on a pair.
        // It applies the function on the second element.
        let inline mapsnd f (v,s) = v, f s    

        fun command ->
            let id = accountId command

            load id command
            |> mapsnd (handle command)
            ||> save id

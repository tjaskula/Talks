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
        let load accountId =
            let events = readStream (streamId accountId)
            replay Uninitialized events

        let save accountId expectedVersion events = appendToStream (streamId accountId) expectedVersion events

        // the mapsnd function works on a pair.
        // It applies the function on the second element.
        let inline mapsnd f (v,s) = v, f s    

        fun command ->
            let id = accountId command

            load id
            |> mapsnd (handle command)
            ||> save id

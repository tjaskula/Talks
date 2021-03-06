module AccountingEs.Tests.``When making a money deposit``

open Xunit
open System
open Accounting

let dates = { RecordDate = DateTime.Now; ValidityDate = DateTime.Now }

[<Fact>]
let ``Cannot make deposit on unitialized account``() =
    Given []
    |> When ( Deposit{Dates = dates; AccountId = AccountId 11; Amount = 100M} )
    |> ExpectThrows<InvalidOperationException>

[<Fact>]
let ``When making initial deposit account's balance changes``() =
    Given [Opened{Dates = dates; AccountId = AccountId 11; Owner = "Tomasz"}]
    |> When ( Deposit{Dates = dates; AccountId = AccountId 11; Amount = 100M} )
    |> Expect [ Deposited{Dates = dates; AccountId = AccountId 11; Amount = 100M; CurrentBalance = 100M} ]
    
[<Fact>]
let ``When making deposit account's balance correctly calculates``() =
    Given [Opened{Dates = dates; AccountId = AccountId 11; Owner = "Tomasz"}
           Deposited{Dates = dates; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 150M}]
    |> When ( Deposit{Dates = dates; AccountId = AccountId 11; Amount = 100M} )
    |> Expect [ Deposited{Dates = dates; AccountId = AccountId 11; Amount = 100M; CurrentBalance = 250M} ]

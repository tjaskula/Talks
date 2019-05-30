module AccountingEs.Tests.``When making a money withdraw``

open Xunit
open System
open Accounting

let dates = { RecordDate = DateTime.Now; ValidityDate = DateTime.Now }

[<Fact>]
let ``Cannot make withdraw from unitialized account``() =
    Given []
    |> When ( Withdraw{Dates = dates; AccountId = AccountId 11; Amount = 100M} )
    |> ExpectThrows<InvalidOperationException>

[<Fact>]
let ``When making withdraw from empty account it doesn't succeed``() =
    Given [Opened{Dates = dates; AccountId = AccountId 11; Owner = "Tomasz"}]
    |> When ( Withdraw{Dates = dates; AccountId = AccountId 11; Amount = 100M} )
    |> ExpectThrows<InvalidOperationException>
    
[<Fact>]
let ``When making withdraw from active account's balance correctly calculates``() =
    Given [Opened{Dates = dates; AccountId = AccountId 11; Owner = "Tomasz"}
           Deposited{Dates = dates; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 150M}]
    |> When ( Withdraw{Dates = dates; AccountId = AccountId 11; Amount = 100M} )
    |> Expect [ Withdrawn{Dates = dates; AccountId = AccountId 11; Amount = 100M; CurrentBalance = 50M} ]

[<Fact>]
let ``When making withdraw from active account's cannot overdraft``() =
    Given [Opened{Dates = dates; AccountId = AccountId 11; Owner = "Tomasz"}
           Deposited{Dates = dates; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 150M}]
    |> When ( Withdraw{Dates = dates; AccountId = AccountId 11; Amount = 151M} )
    |> ExpectThrows<InvalidOperationException>
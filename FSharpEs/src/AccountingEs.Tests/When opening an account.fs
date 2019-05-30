module AccountingEs.Tests.``When opening an account``

open Xunit
open System
open Accounting

let dates = { RecordDate = DateTime.Now; ValidityDate = DateTime.Now }

[<Fact>]
let ``Opening and account should succeed``() =
    Given []
    |> When ( OpenAccount{Dates = dates; AccountId = AccountId 11; Owner = "Tomasz"} )
    |> Expect [ Opened {Dates = dates; AccountId = AccountId 11; Owner = "Tomasz"} ]

module AccountingEs.Tests.``When opening an account``

open Xunit
open Accounting

[<Fact>]
let ``Opening and account should succeed``() =
    Given []
    |> When ( OpenAccount{AccountId = AccountId 11; Owner = "Tomasz"} )
    |> Expect [ Opened { AccountId = AccountId 11; Owner = "Tomasz"} ]

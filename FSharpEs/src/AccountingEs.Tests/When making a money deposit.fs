module AccountingEs.Tests.``When making a money deposit``

open Xunit
open System
open Accounting

[<Fact>]
let ``Cannot make deposit on unitialized account``() =
    Given []
    |> When ( Deposit{AccountId = AccountId 11; Amount = 100M} )
    |> ExpectThrows<InvalidOperationException>

[<Fact>]
let ``When making initial deposit account's balance changes``() =
    Given [Opened{AccountId = AccountId 11; Owner = "Tomasz"}]
    |> When ( Deposit{AccountId = AccountId 11; Amount = 100M} )
    |> Expect [ Deposited{AccountId = AccountId 11; Amount = 100M; CurrentBalance = 100M} ]
    
[<Fact>]
let ``When making deposit account's balance correctly calculates``() =
    Given [Opened{AccountId = AccountId 11; Owner = "Tomasz"}
           Deposited{AccountId = AccountId 11; Amount = 150M; CurrentBalance = 150M}]
    |> When ( Deposit{AccountId = AccountId 11; Amount = 100M} )
    |> Expect [ Deposited{AccountId = AccountId 11; Amount = 100M; CurrentBalance = 250M} ]

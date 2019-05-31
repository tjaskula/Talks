module AccountingEs.Tests.``When querying the account asOf``

open Xunit
open System
open Accounting

let january2019 = DateTime(2019, 1, 1)
let february2019 = DateTime(2019, 2, 1)
let mars2019 = DateTime(2019, 3, 1)
let april2019 = DateTime(2019, 4, 1)
let may2019 = DateTime(2019, 5, 1)
let june2019 = DateTime(2019, 6, 1)
    
[<Fact>]
let ``When all deposits and withdrawns apply``() =
    Given [Opened{Dates = {RecordDate = january2019; ValidityDate = january2019}; AccountId = AccountId 11; Owner = "Tomasz"}
           Deposited{Dates = {RecordDate = february2019; ValidityDate = february2019}; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 150M}
           Withdrawn{Dates = {RecordDate = april2019; ValidityDate = april2019}; AccountId = AccountId 11; Amount = 50M; CurrentBalance = 100M}
           Deposited{Dates = {RecordDate = june2019; ValidityDate = mars2019}; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 300M}]
    |> WhenQueryAsOf june2019 june2019
    |> ExpectAsOf (Active {Owner = "Tomasz"; AccountId = AccountId 11; Balance = 250M})
    
[<Fact>]
let ``When back dated deposits and withdraws apply on query until now``() =
    Given [Opened{Dates = {RecordDate = january2019; ValidityDate = january2019}; AccountId = AccountId 11; Owner = "Tomasz"}
           Deposited{Dates = {RecordDate = february2019; ValidityDate = february2019}; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 150M}
           Withdrawn{Dates = {RecordDate = april2019; ValidityDate = april2019}; AccountId = AccountId 11; Amount = 50M; CurrentBalance = 100M}
           Deposited{Dates = {RecordDate = june2019; ValidityDate = mars2019}; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 300M}]
    |> WhenQueryAsOf june2019 april2019 
    |> ExpectAsOf (Active {Owner = "Tomasz"; AccountId = AccountId 11; Balance = 250M})
    
[<Fact>]
let ``When back dated deposits and withdraws apply on back query``() =
    Given [Opened{Dates = {RecordDate = january2019; ValidityDate = january2019}; AccountId = AccountId 11; Owner = "Tomasz"}
           Deposited{Dates = {RecordDate = february2019; ValidityDate = february2019}; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 150M}
           Withdrawn{Dates = {RecordDate = april2019; ValidityDate = april2019}; AccountId = AccountId 11; Amount = 50M; CurrentBalance = 100M}
           Deposited{Dates = {RecordDate = june2019; ValidityDate = mars2019}; AccountId = AccountId 11; Amount = 150M; CurrentBalance = 300M}]
    |> WhenQueryAsOf may2019 april2019 
    |> ExpectAsOf (Active {Owner = "Tomasz"; AccountId = AccountId 11; Balance = 100M})

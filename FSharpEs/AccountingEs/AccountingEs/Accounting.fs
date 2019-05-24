module Accounting

  open System
  
  type AccountId = AccountId of Guid
  
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
  
  // Application types
  type Account =
      | Uninitialized
      | Online of OnlineAccount
  
  and OnlineAccount = {
      AccountId: AccountId
      Balance: decimal }
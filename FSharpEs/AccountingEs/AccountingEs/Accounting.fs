module Accounting

  open System
  
  type AccountId = AccountId of Guid
  
  type Event =
      | Opened of Opened
      | Deposited of Deposited
      | Withdrawn of Withdrawn

  and Opened = {
      AccountId: AccountId }

  and Deposited = {
      AccountId: AccountId
      Amount: int }

  and Withdrawn = {
      AccountId: AccountId
      Amount: int }
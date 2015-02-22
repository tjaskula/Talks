namespace Api
 
open System
open Microsoft.FSharp.Core
open Microsoft.FSharp.Data.TypeProviders
open Domain
 
module Database =
 
    [<Literal>]
    let connectionString = @"Data Source=(LocalDb)\v11.0;AttachDbFileName=|DataDirectory|\FSharpSNR.mdf;Integrated Security=True;MultipleActiveResultSets=True"
    type dbSchema = SqlDataConnection<ConnectionString=connectionString, LocalSchemaFile="App_Data\FSharpSNR.dbml", ForceUpdate=false>
 
    let save map input =
        
        let db = dbSchema.GetDataContext()
        
        let newRecord = map input
       
        db.AccountEntity.InsertOnSubmit(newRecord)
        db.DataContext.SubmitChanges()
 
    let mapRegistration input =
 
        let newRecord = new dbSchema.ServiceTypes.AccountEntity()
 
        newRecord.Password <- input.Password
        newRecord.Provider <- input.Provider
 
        let email, isConfirmed = getEmail input
        newRecord.Email <- email
        newRecord.IsEmailConfirmed <- isConfirmed
                            
        match input.Confirmation with
            | Some c -> match c.Activation with
                            | Some a -> newRecord.ActivationCode <- a.ActivationCode
                                        newRecord.ActivationCodeExpirationTime <- Nullable(a.ActivationCodeExpirationTime)
                            | None -> ()
                        
                        match c.ConfirmedOn with
                            | Some c -> newRecord.ConfirmedOn <- Nullable(c)
                            | None -> ()
            | None -> ()
 
        newRecord
    
    let persistRegistration =
        tryCatch (tee (save mapRegistration)) (fun ex -> ValidationError(ex.Message))
 
    let queryAccountByEmail input =
        
        let db = dbSchema.GetDataContext()
 
        query
            {
                for row in db.AccountEntity do
                where (row.Email = match input.Email with
                                    | Verified (VerifiedEmail (EmailAddress e)) -> e
                                    | Unverified (EmailAddress e) -> e)
                select row.Email
            } |> List.ofSeq
              |> List.length
 
    let findByEmailRegistration =
        bindResult queryAccountByEmail (fun res -> res = 0) (fun input -> AccountExists(sprintf "The email '%s' already exists" (fst (getEmail input))))

module Notification =

    let sendActivationEmail input =
        match input.Email with
            | Verified (VerifiedEmail (EmailAddress e)) -> Success(input) // not sending anything
            | Unverified (EmailAddress e) -> (* Sending and returning success *) Success(input)         
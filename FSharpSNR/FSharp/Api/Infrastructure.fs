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

        match input.Email with
            | Verified (VerifiedEmail (EmailAddress e)) -> newRecord.Email <- e
                                                           newRecord.IsEmailConfirmed <- true
            | Unverified (EmailAddress e) -> newRecord.Email <- e
                            
        match input.Confirmation with
            | Some c -> newRecord.ActivationCode <- c.ActivationCode
                        newRecord.ActivationCodeExpirationTime <- Nullable(c.ActivationCodeExpirationTime)
                        newRecord.ConfirmedOn <- Nullable(DateTime.Now)
            | None -> ()

        newRecord
    
    let persistRegistration =
        tryCatch (tee (save mapRegistration)) (fun ex -> ValidationError(ex.Message))

//    let queryAccountByEmail input =
//        
//        let db = dbSchema.GetDataContext()
//
//        query
//            {
//                for row in db.AccountEntity do
//                where (row.Email = input.Email)
//                select row.Email
//            } |> List.ofSeq
//              |> List.length
//
//    let findByEmailRegistration =
//        tryCatch (tee queryAccountByEmail) (fun ex -> ValidationError(ex.Message))
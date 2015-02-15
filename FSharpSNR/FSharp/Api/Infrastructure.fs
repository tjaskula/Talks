namespace Api

open System
open Microsoft.FSharp.Core
open Microsoft.FSharp.Data.TypeProviders
open Domain

module Database =

    [<Literal>]
    let connectionString = @"Data Source=(LocalDb)\v11.0;AttachDbFileName=|DataDirectory|\FSharpSNR.mdf;Integrated Security=True;MultipleActiveResultSets=True"
    type dbSchema = SqlDataConnection<ConnectionString=connectionString, LocalSchemaFile="App_Data\FSharpSNR.dbml", ForceUpdate=false>
    
    let persistRegistration input =
        
        let db = dbSchema.GetDataContext()
        let newRecord = new dbSchema.ServiceTypes.AccountEntity()
        newRecord.Email <- input.Email
        newRecord.Password <- input.Password
        newRecord.Provider <- input.Provider
        newRecord.IsEmailConfirmed <- input.IsEmailConfirmed
        match input.ActivationCode with
            | Some v -> newRecord.ActivationCode <- v
            | None -> ()
        match input.ActivationCodeExpirationTime with
            | Some v -> newRecord.ActivationCodeExpirationTime <- Nullable<DateTime>(v)
            | None -> ()
        match input.ConfirmedOn with
            | Some v -> newRecord.ConfirmedOn <- Nullable<DateTime>(v)
            | None -> ()
        db.AccountEntity.InsertOnSubmit(newRecord)
//        try
//            db.DataContext.SubmitChanges()
//            Success()
//        with
//            | exn -> Failure(ValidationError(ex.Message))
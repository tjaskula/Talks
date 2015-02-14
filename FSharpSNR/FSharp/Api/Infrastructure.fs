namespace Api

open Common
open Microsoft.FSharp.Core
open Microsoft.FSharp.Data.TypeProviders

module Database =

    [<Literal>]
    let connectionString = @"Data Source=(LocalDb)\v11.0;AttachDbFileName=|DataDirectory|\FSharpSNR.mdf;Integrated Security=True;MultipleActiveResultSets=True"
    type dbSchema = SqlDataConnection<ConnectionString=connectionString, LocalSchemaFile="App_Data\FSharpSNR.dbml", ForceUpdate=false>

    let save input =

        let db = dbSchema.GetDataContext()
        db.AccountEntity.InsertOnSubmit(input)
    
    let persistRegistration =
        tee save
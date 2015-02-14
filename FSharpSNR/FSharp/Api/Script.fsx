#r "System.Data.dll"
#r "FSharp.Data.TypeProviders.dll"
#r "FSharp.Core.dll"
#r "System.Data.Linq.dll"

open System
open Microsoft.FSharp.Core
open Microsoft.FSharp.Data.TypeProviders

AppDomain.CurrentDomain.SetData("DataDirectory", "C:\Users\tjaskula\Documents\GitHub\Talks\FSharpSNR\FSharp\Api\App_Data")

[<Literal>]
let connectionString = @"Data Source=(LocalDb)\v11.0;AttachDbFileName=|DataDirectory|\FSharpSNR.mdf;Integrated Security=True;MultipleActiveResultSets=True"

type dbSchema = SqlDataConnection<ConnectionString=connectionString, LocalSchemaFile="App_Data\FSharpSNR.dbml", ForceUpdate=false>
let db = dbSchema.GetDataContext()
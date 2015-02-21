#r "System.Data.dll"
#r "FSharp.Data.TypeProviders.dll"
#r "FSharp.Core.dll"
#r "System.Data.Linq.dll"
#r "System.ComponentModel.DataAnnotations.dll"
 
#load "Common.fs" "Domain.fs" "Infrastructure.fs" "Representations.fs""AppServices.fs"
 
open System
open Microsoft.FSharp.Core
open Microsoft.FSharp.Data.TypeProviders
open Api
 
//AppDomain.CurrentDomain.SetData("DataDirectory", "C:\Users\tjaskula\Documents\GitHub\Talks\FSharpSNR\FSharp\Api\App_Data")
//
//[<Literal>]
//let connectionString = @"Data Source=(LocalDb)\v11.0;AttachDbFileName=|DataDirectory|\FSharpSNR.mdf;Integrated Security=True;MultipleActiveResultSets=True"
//
//type dbSchema = SqlDataConnection<ConnectionString=connectionString, LocalSchemaFile="App_Data\FSharpSNR.dbml", ForceUpdate=false>
//let db = dbSchema.GetDataContext()
 
 
let (>>=) f1 f2 = 
        f1 >> bind f2
 
let start x =
    Validation.validateAll
    >> map Validation.normalizeEmail
    >>= RegistrationService.tryConfirmEmail
    >>= Database.findByEmailRegistration
    >>= RegistrationService.setActivationCode
    >>= Database.persistRegistration
    >>= Notification.sendActivationEmail
#r "../packages/Suave/lib/net40/Suave.dll"

open Suave                 // always open suave
open Suave.Http.Successful // for OK-result
open Suave.Web             // for config

// 1. The hello world example
startWebServer defaultConfig (OK "Hello ALT.NET Paris World!")

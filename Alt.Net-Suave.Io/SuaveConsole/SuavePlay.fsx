#r "../packages/Suave/lib/net40/Suave.dll"

open Suave
open Suave.Http.Successful
open Suave.Web

// 1. The hello world example
startWebServer defaultConfig (OK "Hello ALT.NET Paris World!")

// 2. Defining simple application
let app = OK("Hello Alt.Net World!")
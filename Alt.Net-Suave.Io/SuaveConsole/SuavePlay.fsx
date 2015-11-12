#r "../packages/Suave/lib/net40/Suave.dll"

open Suave
open Suave.Http.Successful
open Suave.Web

// 1. The hello world example
startWebServer defaultConfig (OK "Hello ALT.NET Paris World!")

let startServer app =
  let config = { defaultConfig with homeFolder = Some __SOURCE_DIRECTORY__ }
  let _, server = startWebServerAsync config app
  let cts = new System.Threading.CancellationTokenSource()
  Async.Start(server, cts.Token)
  cts

let stopServer (cts : System.Threading.CancellationTokenSource) =
  cts.Cancel()

// 2. Defining simple application
let app = OK("Hello ALT.Net Paris World!")

let cts = startServer app
stopServer cts

// 2'. Defining routing
open Suave.Http
open Suave.Types
open Suave.Http.RequestErrors
open Suave.Http.Applicatives

let app_1 =
  choose
    [ GET >>= choose
        [ path "/hello" >>= OK "Hello GET"
          path "/goodbye" >>= OK "Good bye GET" ]
      POST >>= choose
        [ path "/hello" >>= OK "Hello POST"
          path "/goodbye" >>= OK "Good bye POST" ] ]

let cts1 = startServer app_1
stopServer cts1



let app_2 : WebPart =
  choose
    [ path "/" >>= OK "See <a href=\"/add/40/2\">40 + 2</a>"
      pathScan "/add/%d/%d" (fun (a,b) -> OK(string (a + b)))
      NOT_FOUND "Found no handlers" ]

let cts2 = startServer app_2
stopServer cts2

// 3. Wheater server
#r "System.Xml.Linq.dll"
#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data

type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/weather?units=metric&q=Paris&APPID=7f62d8ca5abb6dd42bae692fd6cbb11d">

let city = "Paris,France"
let paris = Weather.Load("http://api.openweathermap.org/data/2.5/weather?&APPID=7f62d8ca5abb6dd42bae692fd6cbb11d&units=metric&q=" + city)
printfn "%A" paris.Main.Temp
printfn "http://openweathermap.org/img/w/%s.png" paris.Weather.[0].Icon

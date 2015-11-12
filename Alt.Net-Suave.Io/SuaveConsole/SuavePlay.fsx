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


// 4. News server
/// Helper function that returns nice HTML page with title & body

open System.IO

let simplePage title body =
  File.ReadAllText(Path.Combine(__SOURCE_DIRECTORY__, "web/blank.html"))
    .Replace("[TITLE]", title).Replace("[BODY]", body)

// The following uses F# Data to define types that you'll need:

type RssFeed = XmlProvider<"http://fpish.net/rss/blogs/tag/1/f~23">
type GithubSearch = JsonProvider<"samples/github-search.json">
type GithubEvents = JsonProvider<"samples/github-events.json">


let news = async {
  // Read the RSS feed
  let! rss = Http.AsyncRequestString("http://fpish.net/rss/blogs/tag/1/f~23")

  // Get recent starred F# projects from GitHub
  let! res =
    Http.AsyncRequestString
      ( "https://api.github.com/search/repositories?q=language:fsharp&sort=stars&order=desc",
        httpMethod="GET", headers=[
          HttpRequestHeaders.Accept "application/vnd.github.v3+json";
          HttpRequestHeaders.UserAgent "SuaveDemo"] )

  // Finally, to request the F# org events, you can use the
  // following URL: https://api.github.com/orgs/fsharp/events
  // (Otherwise, the request you need is exactly the same)
  return 0 }


let getFeedNews () = async {
  // TODO: Format the news from RSS feed as HTML and return it
  let html =
    [ for item in 1 .. 10 do
        yield "<li>"
        yield sprintf "<h3><a href=\"%s\">Nothing happened (#%d)</a></h3>" "#" item
        yield sprintf "<p class=\"date\">%s</p>" "Just now"
        yield sprintf "<p>Nothing happened, nothing is happening and nothing will ever happen</p>"
        yield "</li>" ]
  return String.concat "" html }

let template = Path.Combine(__SOURCE_DIRECTORY__, "web/index.html")
let html = File.ReadAllText(template)

/// The main handler for Suave server!
let app_4 ctx = async {
  let! data = [ getFeedNews() ] |> Async.Parallel
  let html =
    html.Replace("[FEED-NEWS]", data.[0])
        .Replace("[GITHUB-NEWS]", "")
        .Replace("[GITHUB-PROJECTS]", "")
  return! ctx |> Successful.OK(html) }

let cts4 = startServer app_4
stopServer cts4

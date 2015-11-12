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

open System
open System.IO
// ----------------------------------------------------------------------------
// Helper functions for formatting long text & date
// ----------------------------------------------------------------------------

open System.Text.RegularExpressions

let stripHtml (html:string) =
  Regex.Replace(html, "<.*?>", "")

let formatText length (comment:string) =
  let comment = comment.Replace("\n", " ").Replace("\r", " ")
  let short = comment.Substring(0, min length (comment.Length))
  if short.Length < comment.Length then short + "..." else short

let formatDate (date:DateTime) =
  let ts = DateTime.Now - date
  if ts.TotalDays > 1.0 then sprintf "%d days ago" (int ts.TotalDays)
  elif ts.TotalHours > 1.0 then sprintf "%d hours ago" (int ts.TotalHours)
  elif ts.TotalMinutes > 1.0 then sprintf "%d minutes ago" (int ts.TotalMinutes)
  else "just now"

// ----------------------------------------------------------------------------
// Getting blog news via RSS feed
// ----------------------------------------------------------------------------

type RssFeed = XmlProvider<"http://fpish.net/rss/blogs/tag/1/f~23">

let getFeedNews () = async {
  let! rss = Http.AsyncRequestString("http://fpish.net/rss/blogs/tag/1/f~23")
  let feed = RssFeed.Parse(rss)
  let html =
    [ for item in feed.Channel.Items do
        yield "<li>"
        yield sprintf "<h3><a href=\"%s\">%s</a></h3>" item.Link item.Title
        yield sprintf "<p class=\"date\">%s</p>" (formatDate item.PubDate)
        yield sprintf "<p>%s</p>" (formatText 500 (stripHtml (defaultArg item.Description "")))
        yield "</li>" ]
  return String.concat "" html }

// ----------------------------------------------------------------------------
// Searching for starred F# projects on GitHub
// ----------------------------------------------------------------------------

type GithubSearch = JsonProvider<"samples/github-search.json">

let getGithubProjects () = async {
  let! res =
    Http.AsyncRequestString
      ( "https://api.github.com/search/repositories?q=language:fsharp&sort=stars&order=desc",
        httpMethod="GET", headers=[
          HttpRequestHeaders.Accept "application/vnd.github.v3+json";
          HttpRequestHeaders.UserAgent "SuaveDemo"] )
  let search = GithubSearch.Parse(res)
  let html =
    [ for it in search.Items do
        yield "<li>"
        yield sprintf "<h3><a href=\"%s\">%s</a></h3>" it.HtmlUrl it.Name
        yield sprintf "<p>%s</p>" (defaultArg it.Description "")
        yield "</li>" ]
  return String.concat "" html }

// ----------------------------------------------------------------------------
// Searching for recent GitHub events in 'fsharp' organization
// ----------------------------------------------------------------------------

type GithubEvents = JsonProvider<"samples/github-events.json">

let getGithubEvents () = async {
  let! eventsJson =
    Http.AsyncRequestString
      ( "https://api.github.com/orgs/fsharp/events",
        httpMethod="GET", headers=[
          HttpRequestHeaders.Accept "application/vnd.github.v3+json";
          HttpRequestHeaders.UserAgent "SuaveDemo"] )
  let events = GithubEvents.Parse(eventsJson)
  let html =
    [ for evt in events do
        if evt.Payload.Comment.IsSome || evt.Payload.PullRequest.IsSome then
          yield "<li>"
          yield sprintf "<img src=\"%s\" />" evt.Actor.AvatarUrl
          yield sprintf "<p>%s <a href=\"http://github.com/%s\">@%s</a>"
                  (formatDate evt.CreatedAt) evt.Actor.Login evt.Actor.Login

          match evt.Payload.Comment, evt.Payload.PullRequest with
          | Some cmt, _ ->
              yield sprintf "<a href=\"%s\">commented</a>:</p>" cmt.HtmlUrl
              yield sprintf "<p class=\"body\">%s</p>" (formatText 100 cmt.Body)
          | _, Some pr ->
              let action = evt.Payload.Action.Value
              let action = if action = "closed" && pr.Merged then "merged" else action
              yield sprintf "<a href=\"%s\">%s</a>:</p>" pr.HtmlUrl action
              yield sprintf "<p class=\"body\">%s</p>" (formatText 100 pr.Title)
          | _ -> ()
          yield "</li>" ]
  return String.concat "" html }

// ----------------------------------------------------------------------------
// Minimal server to host the site
// ----------------------------------------------------------------------------

let template = Path.Combine(__SOURCE_DIRECTORY__, "web/index.html")
let html = File.ReadAllText(template)

/// The main handler for Suave server!
let app_4 ctx = async {
  let! data =
    [ getFeedNews()
      getGithubEvents()
      getGithubProjects() ]
    |> Async.Parallel
  let html =
    html.Replace("[FEED-NEWS]", data.[0])
        .Replace("[GITHUB-NEWS]", data.[1])
        .Replace("[GITHUB-PROJECTS]", data.[2])
  return! ctx |> Successful.OK(html) }

let cts4 = startServer app_4
stopServer cts4

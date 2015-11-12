/////////////
// Step 1 : Simple Hello World
////////////

#r "../packages/Suave/lib/net40/Suave.dll"

open Suave
open Suave.Http.Successful
open Suave.Web

// 1. The hello world example
// TODO : FILL IN


/////////////
// Step 2 : Server supports TPL to close the connection
////////////

// TODO : FILL IN






// 2'. Defining routing
// type SuaveTask<'a> = Async<'a option>
// type WebPart = HttpContext -> SuaveTask<HttpContext>
// hence: WebPart = HttpContext -> Async<HttpContext option>
open Suave.Http
open Suave.Types
open Suave.Http.RequestErrors
open Suave.Http.Applicatives

// val choose : (options : WebPart list) -> WebPart

// TODO : APP 1 : Fill in



let greetings q =
  let f = q |> List.tryFind (fun (k, v) -> k = "name")
  let v = match f with
          | Some (k, Some v) -> Some v
          | _ -> None
  defaultArg v "World" |> sprintf "Hello %s"

// TODO : APP 11 Fill IN

/////////////
// Step 3 : Routes dynamiques
////////////

// TODO : APP 2 Fill IN




/////////////
// Step 3 : Async
////////////

// ----------------------------------------------------------------------------
// Understanding Suave WebParts and writing asynchronous servers
// ----------------------------------------------------------------------------

// Under the cover WebPart is a function 'Context -> Async<Context>'. We
// can write it directly as a function and use asynchronous operations
// in the body to avoid blocking

let waitAndReturn time : WebPart = fun ctx -> async {
  let time = 1000 |> defaultArg time
  do! Async.Sleep(int time)
  return! ctx |> OK (sprintf "Waited %d ms" (int time)) }

// TODO : APP 3 Fill IN



let cts3 = startServer app_3
stopServer cts3


/////////////
// Step 4 : News server
////////////


/// Helper function that returns nice HTML page with title & body
#r "System.Xml.Linq.dll"
#r "../packages/FSharp.Data/lib/net40/FSharp.Data.dll"

open FSharp.Data
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

type GithubSearch = JsonProvider<"../samples/github-search.json">

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

type GithubEvents = JsonProvider<"../samples/github-events.json">

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

// TODO : APP 4 FILL IN

let cts4 = startServer app_4
stopServer cts4

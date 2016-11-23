mkdir FizzBuzz

dotnet new --lang fsharp
dotnet restore
dotnet build
dotnet run
dotnet publish

dotnet /Users/tjaskula/Documents/GitHub/Talks/FSharpNETCore/bin/Debug/netcoreapp1.0/publish/FSharpNETCore.dll

// add a new file FizzBuzz.fs, don't forget to update project.json file and recompile
module FizzBuzz

let FizzBuzz n =
    match n % 3, n % 5 with
    | 0, 0 -> "FizzBuzz"
    | 0, _ -> "Fizz"
    | _, 0 -> "Buzz"
    | _, _ -> string n

// add this to Program.fs
[1..100]
    |> Seq.map FizzBuzz
    |> Seq.iter (printfn "%s")


// adding tests
mkdir FizzBuzz.tests

dotnet new --lang fsharp

// edit project.json and add reference to FizzBuzz project
"frameworks": {
    "netcoreapp1.0": {
      "dependencies": {
        "Microsoft.NETCore.App": {
          "type": "platform",
          "version": "1.0.0"
        },
        "FizzBuzz":{
           "target": "project"
         }
      },
      "imports": [
        "portable-net45+win8",
        "dnxcore50"
      ]
    }

// add xunit to project.json
// to dependencies section
"System.Runtime.Serialization.Primitives": "4.1.1",
    "xunit": "2.1.0",
    "dotnet-test-xunit": "1.0.0-rc2-build10015"

// main section
"testRunner": "xunit",
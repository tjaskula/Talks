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
{
  "version": "1.0.0-*",
  "testRunner": "xunit",
  "buildOptions": {
    "debugType": "portable",
    "emitEntryPoint": true,
    "compilerName": "fsc",
    "compile": {
      "includeFiles": [
        "FizzBuzzTests.fs"
      ]
    }
  },
  "dependencies": {
    "Microsoft.FSharp.Core.netcore": "1.0.0-alpha-*",
     "xunit":"2.1.0",
     "dotnet-test-xunit": "2.2.0-preview2-build1029",
     "FizzBuzz": {
        "target": "project"
     }
  },
  "frameworks": {
    "netcoreapp1.0": {
      "dependencies": {
        "Microsoft.NETCore.App": {
          "type": "platform",
          "version": "1.0.0"
        }
      },
      "imports": "dnxcore50"
    }
  },
  "tools": {
    "dotnet-compile-fsc": {
      "version": "1.0.0-preview2-*",
      "imports": "dnxcore50"
    }
  }
}

// add FizzBuzzTests.fs

namespace FizzBuzz.Tests

open System
open Xunit
open FizzBuzz

module FizzBuzzTests =

    [<Fact>]
    let ``should print Fizz when divisible by 3``() = 
        Assert.Equal("Fizz", fizzBuzz 9)

    [<Fact>]
    let ``should print Buzz when divisible by 5``() = 
        Assert.Equal("Buzz", fizzBuzz 10)

    [<Fact>]
    let ``should print FizzBuzz when divisible by 3 and 5``() = 
        Assert.Equal("FizzBuzz", fizzBuzz 15)

    [<Fact>]
    let ``should print number otherwise``() = 
        Assert.Equal("11", fizzBuzz 11)



dotnet restore
dotnet build
dotnet test


// pack
go to FizzBuzz folder
dotnet pack


// some tips
dotnet --version
dotnet --info
which dotnet
dotnet -v run
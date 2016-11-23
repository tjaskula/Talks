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
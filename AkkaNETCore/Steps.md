# Intial solution creation

## Manual dotnet

- mkdir AkkaNETCore
- cd AkkaNetCore

- dotnet new console -n AkkaConsole
- dotnet new sln -n AkkaNETCore
- dotnet sln add AkkaConsole/AkkaConsole.csproj

- cd AkkaConsole/

- dotnet add package Akka --version 1.3.2

- cd ..

- dotnet build
- dotnet run -p AkkaConsole/
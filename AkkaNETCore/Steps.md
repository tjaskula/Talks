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

## Preparing simple demo

1. Copy paste ColorConsole.cs
1. Create Messages/ folder
    1. With PlayMessage.cs
    1. With StopMessage.cs
1. Create Actors/ folder
    1. MusicPlayerActor.cs
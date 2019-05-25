namespace AccountingEs

open System
open Accounting

exception WrongExpectedVersion

// this implementation is inspired by https://github.com/thinkbeforecoding/FsUno
module InMemoryEventStore =
  
    type Stream = { mutable Events:  (Event * int) list }
        with
        static member version stream = 
            stream.Events
            |> Seq.last
            |> snd


    type InMemoryEventStore = 
      { mutable streams : Map<string, Stream> 
        projection : Event -> unit }

      interface IDisposable
          with member x.Dispose() = ()

    let create() = { streams = Map.empty
                     projection = fun _ -> () }
    let subscribe projection store =
        { store with projection = projection} 

    let readStream store streamId =
        match store.streams.TryFind streamId with
        | Some(stream) -> 
            let events =
                stream.Events
                |> Seq.toList
            
            events |> List.map fst
            
        | None -> []

    let appendToStream store streamId expectedVersion newEvents =
        let eventsWithVersion =
            newEvents
            |> List.mapi (fun index event -> (event, expectedVersion + index + 1))

        match store.streams.TryFind streamId with
        | Some stream when Stream.version stream = expectedVersion -> 
            stream.Events <- stream.Events @ eventsWithVersion
        
        | None when expectedVersion = -1 -> 
            store.streams <- store.streams.Add(streamId, { Events = eventsWithVersion })        

        | _ -> raise WrongExpectedVersion 
        
        newEvents
        |> Seq.iter store.projection
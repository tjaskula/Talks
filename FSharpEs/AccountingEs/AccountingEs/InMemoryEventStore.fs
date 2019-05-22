namespace AccountingEs

open System
open Accounting

exception WrongExpectedVersion

// this implementation is taken from https://github.com/thinkbeforecoding/FsUno
module InMemoryEventStore =
  
    type Stream = { mutable Events:  (Event * int) list }
        with
        static member version stream = 
            stream.Events
            |> Seq.last
            |> snd


    type InMemoryEventStore = 
      { mutable streams : Map<string,Stream> 
        projection : Event -> unit }

      interface IDisposable
          with member x.Dispose() = ()

    let create() = { streams = Map.empty
                     projection = fun _ -> () }
    let subscribe projection store =
        { store with projection = projection} 

    let readStream store streamId version count =
        match store.streams.TryFind streamId with
        | Some(stream) -> 
            let events =
                stream.Events
                |> Seq.skipWhile (fun (_,v) -> v < version )
                |> Seq.takeWhile (fun (_,v) -> v <= version + count)
                |> Seq.toList 
            let lastEventNumber = events |> Seq.last |> snd 
            
            events |> List.map fst,
                lastEventNumber ,
                if lastEventNumber < version + count 
                then None 
                else Some (lastEventNumber+1)
            
        | None -> [], -1, None

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
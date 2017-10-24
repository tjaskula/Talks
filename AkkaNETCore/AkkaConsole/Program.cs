using System;
using Akka.Actor;
using AkkaConsole.Actors;
using AkkaConsole.Messages;

namespace AkkaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("MySystem");

            var musicPlayer = system.ActorOf<MusicPlayerActor>("musicPlayer");

            Console.ReadKey();
            musicPlayer.Tell(new PlayMessage("track 1"));

            Console.ReadKey();
            musicPlayer.Tell(new PlayMessage("track 2"));

            Console.ReadKey();
            musicPlayer.Tell(new StopMessage());

            Console.ReadKey();
            musicPlayer.Tell(new StopMessage());

            system.Terminate().Wait();

            Console.ReadKey();
        }
    }
}
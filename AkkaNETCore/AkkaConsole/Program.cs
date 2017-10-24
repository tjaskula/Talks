using System;
using System.Threading;
using Akka.Actor;
using AkkaConsole.Actors;
using AkkaConsole.Messages;

namespace AkkaConsole
{
    class Program
    {   
        static void Main(string[] args)
        {
            // First part demo
//            var system = ActorSystem.Create("MySystem");
//
//            var musicPlayer = system.ActorOf<MusicPlayerActor>("musicPlayer");
//
//            Console.ReadKey();
//            musicPlayer.Tell(new PlayMessage("track 1"));
//
//            Console.ReadKey();
//            musicPlayer.Tell(new PlayMessage("track 2"));
//
//            Console.ReadKey();
//            musicPlayer.Tell(new StopMessage());
//
//            Console.ReadKey();
//            musicPlayer.Tell(new StopMessage());
//
//            system.Terminate().Wait();
//
//            Console.ReadKey();
            
            
            // second part demo (Failure Recovery)
            ColorConsole.WriteLineGray("Creating MatchStatActorSystem");
            var matchStatActorSystem = ActorSystem.Create("MatchStatActorSystem");            

            ColorConsole.WriteLineGray("Creating actor supervisory hierarchy");
            matchStatActorSystem.ActorOf(Props.Create<StatCoordinatorActor>(), "StatCoordinator");


            do
            {
                ShortPause();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("enter a command and hit enter");
                
                var command = Console.ReadLine();

                if (command.StartsWith("show"))
                {
                    string matchId = command.Split(',')[1];

                    var message = new ShowMatchStatsMessage(matchId);
                    matchStatActorSystem.ActorSelection("/user/StatCoordinator").Tell(message);
                }

                if (command.StartsWith("terminate"))
                {
                    string matchId = command.Split(',')[1];                    

                    var message = new TerminateMatchMessage(matchId);
                    matchStatActorSystem.ActorSelection("/user/StatCoordinator").Tell(message);
                }

                if (command == "exit")
                {
                    matchStatActorSystem.Terminate().Wait();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

            } while (true);
        }
        
        private static void ShortPause()
        {
            Thread.Sleep(450);
        }
    }
}
using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaConsole.Messages;

namespace AkkaConsole.Actors
{
    public class StatCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _matches;

        public StatCoordinatorActor()
        {
            _matches = new Dictionary<string, IActorRef>();

            Receive<ShowMatchStatsMessage>(
                message =>
                {
                    CreateChildMatchIfNoExists(message.MatchId);
                    
                    IActorRef childActorRef = _matches[message.MatchId];

                    childActorRef.Tell(message);
                });
            
            Receive<TerminateMatchMessage>(
                message =>
                {
                    if (_matches.ContainsKey(message.MatchId))
                    {
                        IActorRef childActorRef = _matches[message.MatchId];
                        Context.Stop(childActorRef);
                        _matches.Remove(message.MatchId);
                    }
                });
        }
        
        private void CreateChildMatchIfNoExists(string matchId)
        {
            if (!_matches.ContainsKey(matchId))
            {
                IActorRef newChildActorRef = 
                    Context.ActorOf(Props.Create(() => new MatchActor(matchId)), "Match_" + matchId);

                _matches.Add(matchId, newChildActorRef);

                ColorConsole.WriteLineCyan(
                    $"StatCoordinatorActor created new child MatchActor for {matchId} (Total Matches: {_matches.Count})");
            }
        }
        
        
        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("StatCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("StatCoordinatorActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan($"StatCoordinatorActor PreRestart because: {reason}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan($"StatCoordinatorActor PostRestart because: {reason}");

            base.PostRestart(reason);
        } 
        #endregion
    }
}
using System;
using Akka.Actor;
using AkkaConsole.Exceptions;
using AkkaConsole.Messages;

namespace AkkaConsole.Actors
{
    public class MatchActor : ReceiveActor
    {
        private readonly string _matchId;
        private readonly IActorRef _childRef;
        
        public MatchActor(string matchId)
        {
            _matchId = matchId;
            Context.ActorOf(Props.Create(() => new StatFetchActor(matchId)), "StatFetchCounter");
            Receive<ShowMatchStatsMessage>(message => HandleStartMessage(message.MatchId));
        }
        
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                exception =>
                {
                    if (exception is ExternalApiTiemoutException)
                    {
                        return Directive.Restart;
                    }
                    if (exception is FatalApiException)
                    {
                        return Directive.Resume;
                    }

                    return Directive.Restart;
                }
            );
        }
        
        private void HandleStartMessage(string matchId)
        {
            ColorConsole.WriteLineYellow($"MatchActor {matchId} is fetching stats...");

            Context.ActorSelection($"/user/StatCoordinator/Match_{matchId}/StatFetchCounter").Tell(new FetchStatsMessage(matchId));
        }

        
        #region Lifecycle hooks

        protected override void PreStart()
        {
            ColorConsole.WriteWhite($"MatchActor {_matchId} PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteWhite($"MatchActor {_matchId} PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteWhite($"MatchActor {_matchId} PreRestart because: {reason.Message}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteWhite($"MatchActor {_matchId} PostRestart because: {reason.Message} ");

            base.PostRestart(reason);
        }
        #endregion
    }
}
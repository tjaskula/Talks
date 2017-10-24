using System;
using Akka.Actor;
using AkkaConsole.Exceptions;
using AkkaConsole.Messages;

namespace AkkaConsole.Actors
{
    public class StatFetchActor : ReceiveActor
    {
        private int _statFetchCount;
        private readonly string _matchId;

        public StatFetchActor(string matchId)
        {
            ColorConsole.WriteMagenta("StatFetchActor constructor executing");
            _matchId = matchId;
            Receive<FetchStatsMessage>(message => HandleFetchMessage(message));
        }
        
        private void HandleFetchMessage(FetchStatsMessage message)
        {
            // This simulates the call to the external API.
            _statFetchCount++;
            

            //  Simulated bugs
            if (_statFetchCount > 3)
            {
                throw new ExternalApiTiemoutException();
            }

            if (message.MatchId == "Federer-Nadal")
            {
                throw new FatalApiException();
            }

            ColorConsole.WriteMagenta(
                $"StatFetchActor '{message.MatchId}' has been fetched {_statFetchCount} times");
        }
        
        #region Lifecycle hooks

        protected override void PreStart()
        {
            ColorConsole.WriteMagenta($"StatFetchActor {_matchId} PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteMagenta($"StatFetchActor {_matchId} PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteMagenta($"StatFetchActor {_matchId} PreRestart because: {reason.Message}");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteMagenta($"StatFetchActor {_matchId} PostRestart because: {reason.Message} ");

            base.PostRestart(reason);
        }
        #endregion
    }
}
namespace AkkaConsole.Messages
{
    public class ShowMatchStatsMessage
    {
        public string MatchId { get; }

        public ShowMatchStatsMessage(string matchId)
        {
            MatchId = matchId;
        }
    }
}
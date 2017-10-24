namespace AkkaConsole.Messages
{
    public class FetchStatsMessage
    {
        public string MatchId { get; }
        
        public FetchStatsMessage(string matchId)
        {
            MatchId = matchId;
        }
    }
}
namespace AkkaConsole.Messages
{
    public class TerminateMatchMessage
    {
        public string MatchId { get; }
        
        public TerminateMatchMessage(string matchId)
        {
            MatchId = matchId;
        }
    }
}
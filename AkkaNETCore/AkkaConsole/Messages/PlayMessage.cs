namespace AkkaConsole.Messages
{
    public class PlayMessage
    {
        public string TrackName { get; }

        public PlayMessage(string trackName)
        {
            TrackName = trackName;
        }
    }
}
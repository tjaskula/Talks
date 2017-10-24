using System;
using Akka.Actor;
using AkkaConsole.Messages;

namespace AkkaConsole.Actors
{
    public class MusicPlayerActor : ReceiveActor
    {
        private string _currentlyPlaying;

        public MusicPlayerActor()
        {
            Console.WriteLine("Initial behavior to stopped.");
            Stopped();
        }

        private void Stopped()
        {
            Receive<StopMessage>(m => ColorConsole.WriteLineRed("Error: Cannot stop if already stopped."));
            Receive<PlayMessage>(m => StartPlayingMusic(m.TrackName));
        }

        private void Playing()
        {
            Receive<StopMessage>(m => StopPlayingMusic());
            Receive<PlayMessage>(m => ColorConsole.WriteLineRed("Error: Cannot play another track while playing the current one."));
        }

        private void StartPlayingMusic(string trackName)
        {
            _currentlyPlaying = trackName;

            ColorConsole.WriteLineYellow($"Currently playing '{_currentlyPlaying}'");

            Become(Playing);
        }

        private void StopPlayingMusic()
        {

            ColorConsole.WriteLineYellow($"Stopped playing '{_currentlyPlaying}'");

            _currentlyPlaying = "";
            
            Become(Stopped);
        }
    }
}
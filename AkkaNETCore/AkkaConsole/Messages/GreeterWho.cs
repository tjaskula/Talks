namespace AkkaConsole.Messages
{
    public class GreetWho
    {
        public string Who { get; }

        public GreetWho(string who)
        {
            Who = who;
        }
    }
}
using ObjectOriented.ApplicationSerivice;
using ObjectOriented.IO;
using ObjectOriented.Parser;

namespace Console
{
    public class Bootstraper
    {
        private readonly Resolver _resolver = new Resolver();

        public Resolver Initialize()
        {
            var wordCounter = new WordCounterService(new WordCounter(), new FileStoreReader());
            _resolver.Register(wordCounter);

            return _resolver;
        }
    }
}
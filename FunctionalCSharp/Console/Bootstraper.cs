using ObjectOriented.ApplicationSerivice;
using ObjectOriented.Domain;
using ObjectOriented.IO;
using ObjectOriented.Parser;

namespace Console
{
    public class Bootstraper
    {
        private readonly Resolver _resolver = new Resolver();

        public Resolver Initialize()
        {
            var parsers = new ParserCombinator();
            parsers.AddNext(new StartStriper()).AddNext(new EndStriper());
            var wordCounter = new WordCounterService(new WordCounter(), parsers, new FileStoreReader());
            _resolver.Register(wordCounter);

            return _resolver;
        }
    }
}
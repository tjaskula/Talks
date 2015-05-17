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
            var parser = new ParserCombinator(new StartStriper(), new EndStriper(), new PageParser());
            var wordCounter = new WordCounterService(new WordCounter(), parser, new FileStoreReader());
            _resolver.Register(wordCounter);

            return _resolver;
        }
    }
}
using ObjectOriented.AppService;
using ObjectOriented.IO;

namespace Console
{
    public class Bootstraper
    {
        private readonly Resolver _resolver = new Resolver();

        public Resolver Initialize()
        {
            var wordCounter = new WordCounter(new FileStoreReader());
            _resolver.Register(wordCounter);

            return _resolver;
        }
    }
}
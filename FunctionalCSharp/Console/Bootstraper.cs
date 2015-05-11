using ObjectOriented.ApplicationSerivice;
using ObjectOriented.IO;

namespace Console
{
    public class Bootstraper
    {
        private readonly Resolver _resolver = new Resolver();

        public Resolver Initialize()
        {
            var wordCounter = new WordCounterService(new FileStoreReader());
            _resolver.Register(wordCounter);

            return _resolver;
        }
    }
}
using ObjectOriented.AppService;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "../../Data/monteCristo.txt";

            var bootstrapper = new Bootstraper();
            var resolver = bootstrapper.Initialize();

            var wordCounter = resolver.Resolve<WordCounter>();
            var words = wordCounter.CountInFile(path);
        }
    }
}
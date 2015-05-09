using ObjectOriented.AppService;
using ObjectOriented.IO;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "../../Data/monteCristo.txt";

            var wordCounter = new WordCounter(new FileStoreReader());
            var words = wordCounter.CountInFile(path);
        }
    }
}
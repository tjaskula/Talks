using ObjectOriented.AppService;
using ObjectOriented.IO;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "../../Data/monteCristo.txt";

            var wordCounter = new WordCounter(new FileStoreReader());
        }
    }
}
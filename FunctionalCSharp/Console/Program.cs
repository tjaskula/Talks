using ObjectOriented.ApplicationSerivice;

namespace Console
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            const string path = "../../Data/monteCristo.txt";

            var bootstrapper = new Bootstraper();
            var resolver = bootstrapper.Initialize();

            var wordCounter = resolver.Resolve<WordCounterService>();
            var number = wordCounter.CountInFile(path);

            Console.WriteLine("Number of words : {0}", number);
            Console.ReadKey();
        }
    }
}
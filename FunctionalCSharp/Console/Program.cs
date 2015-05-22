using Functional;
using ObjectOriented.ApplicationSerivice;
using ObjectOriented.IO;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "../../Data/monteCristo.txt";

            /*
             * Object Oriented version
            */
            var bootstrapper = new Bootstraper();
            var resolver = bootstrapper.Initialize();

            var wordCounterService = resolver.Resolve<WordCounterService>();
            var number = wordCounterService.CountInFile(path);

            System.Console.WriteLine("Number of words : {0}", number);
            System.Console.ReadKey();

            /*
             * Functional version
             */
            var content = new FileStoreReader().Read(path);
            var wordCounter = new Domain.WordCounter();

            // composing functions
            var startStriperFunc = Parsers.GetStartStriperFunc();
            var endStriperFunc = Parsers.GetEndStriperFunc();
            var pageParserFunc = Parsers.GetPageParserFunc();

            int funcParsedNumber = 0;

            var funcParsed = new Parsers.Success<string>(content).Bind(startStriperFunc).Bind(endStriperFunc).Bind(pageParserFunc);
            if (funcParsed.IsSuccess)
                funcParsedNumber = wordCounter.Count(funcParsed.FromParseResult());

            System.Console.WriteLine("Number of words (func composition) : {0}", funcParsedNumber);
            System.Console.ReadKey();

            int funcParsedNumberLinq = 0;

            var funcParsedLinq = from a in content.ToParseResult()
                                 from b in startStriperFunc(a)
                                 from c in endStriperFunc(b)
                                 from d in pageParserFunc(c)
                                 select d;

            if (funcParsedLinq.IsSuccess)
                funcParsedNumberLinq = wordCounter.Count(funcParsedLinq.FromParseResult());

            System.Console.WriteLine("Number of words (func composition Linq) : {0}", funcParsedNumberLinq);
            System.Console.ReadKey();

            // composing parsers
            var startSriper = Parsers.GetStartStriper();
            var endStriper = Parsers.GetEndStriper();
            var pageStriper = Parsers.GetPageParser();

            int numberComposite = 0;

            var composite = startSriper.Compose(endStriper.Compose(pageStriper));
            var compParseResult = composite(content);

            if (compParseResult.IsSuccess)
                numberComposite = wordCounter.Count(compParseResult.FromParseResult());

            System.Console.WriteLine("Number of words (composing parsers) : {0}", numberComposite);
            System.Console.ReadKey();

            int numberCompositeLinq = 0;

            var parsers = from a in startSriper(content)
                          from b in endStriper(a)
                          from c in pageStriper(b)
                          select c;

            if (parsers.IsSuccess)
                numberCompositeLinq = wordCounter.Count(parsers.FromParseResult());

            System.Console.WriteLine("Number of words (func composition Linq) : {0}", numberCompositeLinq);
            System.Console.ReadKey();
        }
    }
}
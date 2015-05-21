using System.Collections.Generic;
using Functional;
using ObjectOriented.ApplicationSerivice;
using ObjectOriented.IO;
using ObjectOriented.Parser;
using System.Linq;
using ObjectOriented.Domain;

namespace Console
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            const string path = "../../Data/monteCristo.txt";

            //var bootstrapper = new Bootstraper();
            //var resolver = bootstrapper.Initialize();

            //var wordCounter = resolver.Resolve<WordCounterService>();
            //var number = wordCounter.CountInFile(path);

            //Console.WriteLine("Number of words : {0}", number);
            //Console.ReadKey();


            var content = new FileStoreReader().Read(path);

            Func<string, Parsers.ParseResult<string>> startSriper = input =>
                                {
                                    if (string.IsNullOrWhiteSpace(input))
                                        return new Parsers.Error<string>("Cannot process an empty input");

                                    const string startPattern = "*** START";

                                    int startLineIndx = input.IndexOf(startPattern, StringComparison.OrdinalIgnoreCase);
                                    int endOfLineIndx = input.IndexOf("***", startLineIndx + startPattern.Length, StringComparison.OrdinalIgnoreCase);
                                    string remaining = input.Remove(0, endOfLineIndx + "***".Length);

                                    return new Parsers.Success<string>(remaining);
                                };

            Func<string, Parsers.ParseResult<string>> endSriper = input =>
                                {
                                    if (string.IsNullOrWhiteSpace(input))
                                        return new Parsers.Error<string>("Cannot process an empty input");

                                    const string endPattern = "*** END";
                                    int endLineIndx = input.IndexOf(endPattern, StringComparison.OrdinalIgnoreCase);
                                    string remaining = input.Remove(endLineIndx);

                                    return new Parsers.Success<string>(remaining);
                                };


            Func<string, Parsers.ParseResult<string>> endSriper2 = input =>
                                {
                                    if (string.IsNullOrWhiteSpace(input))
                                        return new Parsers.Error<string>("Cannot process an empty input");

                                    const string endPattern = "*** END";
                                    int endLineIndx = input.IndexOf(endPattern, StringComparison.OrdinalIgnoreCase);
                                    string remaining = input.Remove(endLineIndx);

                                    return new Parsers.Success<string>(remaining);
                                };


            Func<string, Parsers.ParseResult<IEnumerable<BookElement>>> pageStriper = input =>
                                {
                                    if (string.IsNullOrWhiteSpace(input))
                                        return new Parsers.Error<IEnumerable<BookElement>>("Cannot process an empty input");

                                    var words = input.Split(' ');

                                    List<Page> pages = new List<Page>();
                                    for (int i = 0; i < words.Length; i += 1000)
                                    {
                                        var pagedWords = words.Skip(i).Take(i + 1000);
                                        var page = new Page(string.Join(" ", pagedWords));
                                        pages.Add(page);
                                    }

                                    return new Parsers.Success<IEnumerable<BookElement>>(pages);
                                };

            //var pipline = new Parsers.Success<string>(content).Bind(startSriper).Bind(endSriper);
            //var pipline = new Parsers.Success<string>(content).Bind(startSriper).Bind(endSriper).Bind(pageStriper);

            var result = from a in content.ToParseResult()
                         from b in startSriper(a)
                         from c in endSriper(b)
                         select c;


            var p1 = startSriper.ToParser();
            var p2 = endSriper.ToParser();
            //var p3 = endSriper2.ToParser();
            var p3 = pageStriper.ToParser();

            //var composite = p1.Compose(p2);

            //var result2 = composite(content);

            var parsers = from a in p1(content)
                          from b in p2(a)
                          from c in p3(b)
                          select c;
        }
    }
}
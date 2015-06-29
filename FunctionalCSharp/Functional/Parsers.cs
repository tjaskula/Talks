using System;
using System.Collections.Generic;
using System.Linq;

namespace Functional
{
    public static partial class Parsers
    {
        public static Parser<string, string> GetStartStriper()
        {
            return GetStartStriperFunc().ToParser();
        } 

        public static Func<string, ParseResult<string>> GetStartStriperFunc()
        {
            return input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return new Error<string>("Cannot process an empty input");

                const string startPattern = "*** START";

                int startLineIndx = input.IndexOf(startPattern, StringComparison.OrdinalIgnoreCase);
                int endOfLineIndx = input.IndexOf("***", startLineIndx + startPattern.Length, StringComparison.OrdinalIgnoreCase);
                string remaining = input.Remove(0, endOfLineIndx + "***".Length);

                return new Success<string>(remaining);
            };
        }

        public static Parser<string, string> GetEndStriper()
        {
            return GetEndStriperFunc().ToParser();
        }

        public static Func<string, ParseResult<string>> GetEndStriperFunc()
        {
            return input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return new Error<string>("Cannot process an empty input");

                const string endPattern = "*** END";
                int endLineIndx = input.IndexOf(endPattern, StringComparison.OrdinalIgnoreCase);
                string remaining = input.Remove(endLineIndx);

                return new Success<string>(remaining);
            };
        }

        public static Parser<string, IEnumerable<Domain.BookElement>> GetPageParser()
        {
            return GetPageParserFunc().ToParser();
        }

        public static Func<string, ParseResult<IEnumerable<Domain.BookElement>>> GetPageParserFunc()
        {
            return input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return new Error<IEnumerable<Domain.BookElement>>("Cannot process an empty input");

                var words = input.Split(' ');

                List<Domain.Page> pages = new List<Domain.Page>();
                for (int i = 0; i < words.Length; i += 1000)
                {
                    var pagedWords = words.Skip(i).Take(i + 1000);
                    var page = new Domain.Page(string.Join(" ", pagedWords));
                    pages.Add(page);
                }

                return new Success<IEnumerable<Domain.BookElement>>(pages);
            };
        }
    }
}
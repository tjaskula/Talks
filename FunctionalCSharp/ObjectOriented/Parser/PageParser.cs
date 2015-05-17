using System;
using System.Collections.Generic;
using System.Linq;
using ObjectOriented.Domain;

namespace ObjectOriented.Parser
{
    public class PageParser : IParser<string, IEnumerable<BookElement>>
    {
        public ParserResult<IEnumerable<BookElement>> Parse(ParserResult<string> input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            var unwrapped = input.Parsed;
            var words = unwrapped.Split(' ');

            List<Page> pages = new List<Page>();
            for (int i = 0; i < words.Length; i += 1000)
            {
                var pagedWords = words.Skip(i).Take(i + 1000);
                var page = new Page(string.Join(" ", pagedWords));
                pages.Add(page);
            }

            return new ParserResult<IEnumerable<BookElement>>(pages, string.Empty);
        }
    }
}
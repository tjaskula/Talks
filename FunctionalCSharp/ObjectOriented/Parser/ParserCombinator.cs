using System;
using System.Collections.Generic;
using ObjectOriented.Domain;

namespace ObjectOriented.Parser
{
    /// <summary>
    /// Compose different parsers together
    /// </summary>
    public class ParserCombinator
    {
        private readonly IParser<string, string> _start;
        private readonly IParser<string, string> _end;
        private readonly IParser<string, IEnumerable<BookElement>> _page;

        public ParserCombinator(IParser<string, string> start, 
                                IParser<string, string> end, 
                                IParser<string, IEnumerable<BookElement>> page)
        {
            if (start == null) throw new ArgumentNullException("start");
            if (end == null) throw new ArgumentNullException("end");
            if (page == null) throw new ArgumentNullException("page");

            _start = start;
            _end = end;
            _page = page;
        }

        public IEnumerable<BookElement> Parse(string input)
        {
            var parseResult = _page.Parse(_end.Parse(_start.Parse(new ParserResult<string>(parsed : input))));
            // you can check the output of parsing so it can be logged or something like that
            // if (!parseResult.IsSuccess)
            return parseResult.Parsed;
        }
    }
}
using System;
using System.Collections.Generic;

namespace ObjectOriented.Parser
{
    public class ParserCombinator : IParser<string>
    {
        private readonly IList<IParser<string>> _parsers = new List<IParser<string>>(); 
        
        public string Parse(string input)
        {
            string result = input;
            foreach (var parser in _parsers)
                result = parser.Parse(result);
            return result;
        }

        public ParserCombinator AddNext(IParser<string> parser)
        {
            if (parser == null)
                throw new ArgumentNullException("parser");

            _parsers.Add(parser);

            return this;
        }
    }
}
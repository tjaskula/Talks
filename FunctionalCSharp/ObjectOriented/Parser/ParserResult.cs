using System;

namespace ObjectOriented.Parser
{
    public class ParserResult<T>
    {
        public ParserResult(T parsed, string remainingToParse)
        {
            if (parsed == null)
                throw new ArgumentNullException("parsed");

            Parsed = parsed;
            RemainingToParse = remainingToParse;
        }

        public T Parsed { get; private set; }
        public string RemainingToParse { get; private set; } 
    }
}
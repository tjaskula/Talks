using System;

namespace ObjectOriented.Parser
{
    public class EndStriper : IParser<string, string>
    {
        public ParserResult<string> Parse(ParserResult<string> input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            const string endPattern = "*** END";
            string unwrapped = input.Parsed;
            int endLineIndx = unwrapped.IndexOf(endPattern, StringComparison.OrdinalIgnoreCase);
            string remaining = unwrapped.Remove(endLineIndx);
            return new ParserResult<string>(remaining, remaining);
        }
    }
}
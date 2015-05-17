using System;

namespace ObjectOriented.Parser
{
    public class EndStriper : IParser<string, string>
    {
        public ParserResult<string> Parse(ParserResult<string> input)
        {
            if (!input.IsSuccess)
                return input;

            if (string.IsNullOrWhiteSpace(input.Parsed))
                return new ParserResult<string>(errorMessage: "Cannot parse empty input");

            const string endPattern = "*** END";
            string unwrapped = input.Parsed;
            int endLineIndx = unwrapped.IndexOf(endPattern, StringComparison.OrdinalIgnoreCase);
            string remaining = unwrapped.Remove(endLineIndx);
            return new ParserResult<string>(parsed : remaining);
        }
    }
}
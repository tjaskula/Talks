using System;

namespace ObjectOriented.Parser
{
    public class StartStriper : IParser<string, string>
    {
        public ParserResult<string> Parse(ParserResult<string> input)
        {
            if (!input.IsSuccess)
                return input;

            if (string.IsNullOrWhiteSpace(input.Parsed))
                return new ParserResult<string>(errorMessage: "Cannot parse empty input");

            const string startPattern = "*** START";
            
            string unwrapped = input.Parsed;
            int startLineIndx = unwrapped.IndexOf(startPattern, StringComparison.OrdinalIgnoreCase);
            int endOfLineIndx = unwrapped.IndexOf("***", startLineIndx + startPattern.Length, StringComparison.OrdinalIgnoreCase);
            string remaining = unwrapped.Remove(0, endOfLineIndx + "***".Length);
            
            return new ParserResult<string>(parsed: remaining);
        }
    }
}
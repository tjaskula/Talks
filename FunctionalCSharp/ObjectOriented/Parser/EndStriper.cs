using System;

namespace ObjectOriented.Parser
{
    public class EndStriper : IParser<string>
    {
        public string Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            const string endPattern = "*** END";
            int endLineIndx = input.IndexOf(endPattern, StringComparison.OrdinalIgnoreCase);
            return input.Remove(endLineIndx);
        }
    }
}
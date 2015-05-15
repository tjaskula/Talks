using System;

namespace ObjectOriented.Parser
{
    public class StartStriper : IParser<string>
    {
        public string Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            const string startPattern = "*** START";
            int startLineIndx = input.IndexOf(startPattern, StringComparison.OrdinalIgnoreCase);
            int endOfLineIndx = input.IndexOf("***", startLineIndx + startPattern.Length, StringComparison.OrdinalIgnoreCase);
            return input.Remove(0, endOfLineIndx + "***".Length);
        }
    }
}
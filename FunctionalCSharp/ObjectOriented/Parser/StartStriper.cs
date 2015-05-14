using System;

namespace ObjectOriented.Parser
{
    public class StartStriper : IParser<string, string>
    {
        public string Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("input");

            int startLineIndx = input.IndexOf("*** START", StringComparison.InvariantCulture);
            int endOfLineIndx = input.IndexOf("***", startLineIndx + 10, StringComparison.InvariantCulture);
            return input.Remove(0, endOfLineIndx + 3);
        }
    }
}
using System.Text.RegularExpressions;

namespace ObjectOriented.Parser
{
    public class WordCounter
    {
        public int Count(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            return Regex.Matches(text, @"\S+").Count;
        }
    }
}
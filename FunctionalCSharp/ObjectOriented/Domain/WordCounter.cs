using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ObjectOriented.Domain
{
    public class WordCounter
    {
        public int Count(IEnumerable<BookElement> pages)
        {
            if (pages == null)
                return 0;

            return pages.Sum(p => Regex.Matches(((Page)p).Text, @"\S+").Count);
        }
    }
}
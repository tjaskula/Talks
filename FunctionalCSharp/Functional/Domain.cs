using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Functional
{
    public static class Domain
    {
        public enum BookElementType
        {
            Page
        }

        public abstract class BookElement
        {
            private readonly BookElementType _tag;

            protected BookElement(BookElementType tag)
            {
                _tag = tag;
            }

            public BookElementType Tag { get { return _tag; } }
        }

        public class Page : BookElement
        {
            private readonly string _text;

            public Page(string text)
                : base(BookElementType.Page)
            {
                if (string.IsNullOrWhiteSpace(text))
                    throw new ArgumentNullException("text");

                _text = text;
            }

            public string Text { get { return _text; } }
        }

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
}
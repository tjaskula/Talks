using System;

namespace ObjectOriented.Domain
{
    public class Page : BookElement
    {
        private readonly string _text;

        public Page(string text) : base(BookElementType.Page)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException("text");

            _text = text;
        }

        public string Text { get { return _text; } }
    }
}
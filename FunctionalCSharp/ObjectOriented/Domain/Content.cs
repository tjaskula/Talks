using System;
using System.Collections.Generic;

namespace ObjectOriented.Domain
{
    public class Content : BookElement
    {
        private readonly List<Chapter> _chapters = new List<Chapter>(); 

        public Content(Chapter firstChapter) : base(BookElementType.Content)
        {
            if (firstChapter == null)
                throw new ArgumentNullException("firstChapter");

            _chapters.Add(firstChapter);
        }

        public void AddChapter(Chapter chapter)
        {
            if (chapter == null)
                throw new ArgumentNullException("chapter");

            _chapters.Add(chapter);
        }

        public IEnumerable<Chapter> Chapters { get { return _chapters; } } 
    }
}
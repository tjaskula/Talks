using System;
using System.Collections.Generic;

namespace ObjectOriented.Domain
{
    public class Chapter : BookElement
    {
        private readonly List<Page> _pages = new List<Page>(); 

        public Chapter(Page firstPage) : base(BookElementType.Chapter)
        {
            if (firstPage == null)
                throw new ArgumentNullException("firstPage");

            _pages.Add(firstPage);
        }

        public void AddPage(Page page)
        {
            if (page == null)
                throw new ArgumentNullException("page");

            _pages.Add(page);
        }

        public IEnumerable<Page> Pages { get { return _pages; } } 
    }
}
namespace ObjectOriented.Domain
{
    public abstract class BookElement
    {
        private readonly BookElementType _tag;

        protected BookElement(BookElementType tag)
        {
            _tag = tag;
        }

        public BookElementType Tag { get { return _tag; } }
    }
}
using System;

namespace ObjectOriented.Parser
{
    public class ParserResult<T>
    {
        public ParserResult(T parsed)
        {
            if (parsed == null)
                throw new ArgumentNullException("parsed");

            Parsed = parsed;
            IsSuccess = true;
        }

        public ParserResult(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentNullException("errorMessage");

            ErrorMessage = errorMessage;
            IsSuccess = false;
        }

        public T Parsed { get; private set; }
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
    }
}
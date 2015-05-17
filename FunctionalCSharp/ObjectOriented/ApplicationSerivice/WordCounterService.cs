using System;
using ObjectOriented.Domain;
using ObjectOriented.Parser;

namespace ObjectOriented.ApplicationSerivice
{
    public class WordCounterService
    {
        private readonly WordCounter _wordCounter;
        private readonly ParserCombinator _textParser;
        private readonly IStoreReader _storeReader;

        public WordCounterService(WordCounter wordCounter, ParserCombinator textParser, IStoreReader storeReader)
        {
            if (wordCounter == null)
                throw new ArgumentNullException("wordCounter");

            if (textParser == null)
                throw new ArgumentNullException("textParser");

            if (storeReader == null)
                throw new ArgumentNullException("storeReader");

            _wordCounter = wordCounter;
            _textParser = textParser;
            _storeReader = storeReader;
        }

        public int CountInFile(string path)
        {
            var content = _storeReader.Read(path);
            var pages = _textParser.Parse(content);
            return _wordCounter.Count(pages);
        }
    }
}
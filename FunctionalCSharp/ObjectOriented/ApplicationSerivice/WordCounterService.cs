using System;
using ObjectOriented.Parser;

namespace ObjectOriented.ApplicationSerivice
{
    public class WordCounterService
    {
        private readonly WordCounter _wordCounter;
        private readonly IStoreReader _storeReader;

        public WordCounterService(WordCounter wordCounter, IStoreReader storeReader)
        {
            if (storeReader == null) 
                throw new ArgumentNullException("storeReader");

            if (wordCounter == null)
                throw new ArgumentNullException("wordCounter");

            _wordCounter = wordCounter;
            _storeReader = storeReader;
        }

        public int CountInFile(string path)
        {
            return _wordCounter.Count(_storeReader.Read(path));
        }
    }
}
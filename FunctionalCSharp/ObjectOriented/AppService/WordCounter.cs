using System;

namespace ObjectOriented.AppService
{
    public class WordCounter
    {
        private readonly IStoreReader _storeReader;

        public WordCounter(IStoreReader storeReader)
        {
            if (storeReader == null) 
                throw new ArgumentNullException("storeReader");

            _storeReader = storeReader;
        }

        public int CountInFile(string path)
        {
            return _storeReader.Read(path).Length;
        }
    }
}
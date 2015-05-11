using System;
using ObjectOriented.AppService;

namespace ObjectOriented.ApplicationSerivice
{
    public class WordCounterService
    {
        private readonly IStoreReader _storeReader;

        public WordCounterService(IStoreReader storeReader)
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
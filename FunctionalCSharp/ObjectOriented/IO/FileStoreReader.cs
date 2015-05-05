using System;
using System.IO;
using ObjectOriented.AppService;

namespace ObjectOriented.IO
{
    public class FileStoreReader : IStoreReader<string>
    {
        private readonly string _filePath;

        public FileStoreReader(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException("filePath");

            if (!File.Exists(filePath))
                throw new InvalidOperationException(string.Format("The file path '{0}' doesn't exist"));

            _filePath = filePath;
        }

        public string Read()
        {
            return File.ReadAllText(_filePath);
        }
    }
}
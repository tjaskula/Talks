using System;
using System.IO;
using ObjectOriented.AppService;

namespace ObjectOriented.IO
{
    public class FileStoreReader : IStoreReader
    {
        public string Read(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path");

            if (!File.Exists(path))
                throw new InvalidOperationException(string.Format("The file path '{0}' doesn't exist", path));

            return File.ReadAllText(path);
        }
    }
}
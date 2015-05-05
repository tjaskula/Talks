using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectOriented.IO;

namespace Console
{
    class Program
    {
        static void Main(string[] args) 
        {
            var storeReader = new FileStoreReader("../../Data/monteCristo.txt");
            var text = storeReader.Read();
            }
    }
}
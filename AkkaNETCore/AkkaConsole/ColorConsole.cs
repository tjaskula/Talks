using System;

namespace AkkaConsole
{
    public static class ColorConsole
    {
        public static void WriteLineGreen(string message)
        {
            var beforeColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeColor;
        }

        public static void WriteLineYellow(string message)
        {
            var beforeColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeColor;
        }

        public static void WriteLineRed(string message)
        {
            var beforeColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeColor;
        }

        public static void WriteLineCyan(string message)
        {
            var beforeColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(message);

            Console.ForegroundColor = beforeColor;
        }
        
        public static void WriteLineGray(string message, params object[] args)
        {
            Write(ConsoleColor.Gray, string.Format(message, args));
        }
        
        public static void WriteMagenta(string message, params object[] args)
        {
            Write(ConsoleColor.Magenta, string.Format(message, args));
        }

        public static void WriteWhite(string message, params object[] args)
        {
            Write(ConsoleColor.White, string.Format(message, args));
        }
        
        static object locker = new object();
        private static void Write(ConsoleColor color, string message)
        {
            // Locking for demo purposes so we get the correct color output.
            // This represents shared state accessed by different actors so
            // should be avoided in practice as it breaks the abstraction
            // of the Actor Model.
            lock (locker)
            {
                Console.ForegroundColor = color;

                Console.WriteLine(message);
            }
        }
    }
}
using System;
using Domain.Commands;

namespace Presentation.CommandHandlers
{
    public class CommonCommandHandlers
    {
        public void Log<T>(T command, Action<T> next) where T : ICommand
        {
            Console.WriteLine("log something before");
            next(command);
            Console.WriteLine("log something after");
        }

        public void Audit<T>(T command, Action<T> next) where T : ICommand
        {
            Console.WriteLine("audit something before");
            next(command);
            Console.WriteLine("audit something after");
        }
    }
}
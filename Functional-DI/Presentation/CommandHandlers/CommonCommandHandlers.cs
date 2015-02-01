using System;
using Domain.Commands;

namespace Presentation.CommandHandlers
{
    public class CommonCommandHandlers
    {
        public void Log<T>(T command, Action<T> next) where T : ICommand
        {
            // log something here
            next(command);
            // log after here
        }
    }
}
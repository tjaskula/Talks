using System;
using System.Collections.Generic;

namespace Presentation.Helpers
{
    public class Dispatcher<TCommand>
    {
        private readonly Dictionary<Type, Action<TCommand>> dictionary = new Dictionary<Type, Action<TCommand>>();

        public void Subscribe<T>(Action<T> commandHandler) where T : TCommand
        {
           this.dictionary.Add(typeof(T), x => commandHandler((T)x));
        }

        public void Dispatch(TCommand command)
        {
            Action<TCommand> handler;
            if (!this.dictionary.TryGetValue(command.GetType(), out handler))
            {
                throw new Exception("cannot map " + command.GetType());
            }
            handler(command);
        }
    }
}
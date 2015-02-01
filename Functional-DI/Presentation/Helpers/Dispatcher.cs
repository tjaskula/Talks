using System;
using System.Collections.Generic;

namespace Presentation.Helpers
{
    public interface IDispatcher<in TCommand>
    {
        void Dispatch(TCommand command);
    }

    public interface ISubscriber<in TCommand>
    {
        void Subscribe<T>(Action<T> commandHandler) where T : TCommand;
    }

    public class Dispatcher<TCommand> : IDispatcher<TCommand>, ISubscriber<TCommand>
    {
        private readonly Dictionary<Type, Action<TCommand>> _dictionary = new Dictionary<Type, Action<TCommand>>();

        public void Subscribe<T>(Action<T> commandHandler) where T : TCommand
        {
           _dictionary.Add(typeof(T), x => commandHandler((T)x));
        }

        public void Dispatch(TCommand command)
        {
            Action<TCommand> handler;
            if (!_dictionary.TryGetValue(command.GetType(), out handler))
            {
                throw new Exception("cannot map " + command.GetType());
            }
            handler(command);
        }
    }
}
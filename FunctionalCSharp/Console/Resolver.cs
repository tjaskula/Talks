using System;
using System.Collections.Generic;

namespace Console
{
    public class Resolver
    {
        private readonly Dictionary<Type, object> _registrations = new Dictionary<Type, object>();
 
        public void Register<T>(T instance)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            _registrations[typeof (T)] = instance;
        }

        public T Resolve<T>() where T : class
        {
            object instance;
            if (_registrations.TryGetValue(typeof (T), out instance))
                return (T) instance;

            return null;
        }
    }
}
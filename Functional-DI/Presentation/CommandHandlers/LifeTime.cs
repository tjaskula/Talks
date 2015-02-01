using System;
using System.Collections.Generic;
using System.Threading;

namespace Presentation.CommandHandlers
{
    public class LifeTime : IDisposable
    {
        private readonly Dictionary<Type, ThreadLocal<object>> _dependencies = new Dictionary<Type, ThreadLocal<object>>(); 

        public TResult PerThread<TResult>(Func<TResult> dependencyFactory) where TResult : class
        {
            ThreadLocal<object> threadLocal;
            if (!_dependencies.TryGetValue(typeof (TResult), out threadLocal))
            {
                threadLocal = new ThreadLocal<object>(dependencyFactory);
                _dependencies.Add(typeof(TResult), threadLocal);
            }

            return (TResult)threadLocal.Value;
        }

        public void PerWebRequest<TResult>(Func<TResult> dependencyFactory)
        {

        }

        public void Dispose()
        {
            foreach (var dep in _dependencies)
            {
                dep.Value.Dispose();
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TPLDataFlowTests
{
    public class CompositeSubscription : ISubscription, IEnumerable<ISubscription>, ISubscriptionResolver
    {
        private readonly HashSet<ISubscription> _subscriptions = new HashSet<ISubscription>();
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        public CompositeSubscription() { }

        public CompositeSubscription(IEnumerable<ISubscription> subscriptions)
        {
            AddRange(subscriptions);
        }

        public bool IsEmpty => _subscriptions.Count == 0;

        public void Push(object message)
        {
            foreach (var s in Snapshot)
                s.Push(message);
        }

        public event EventHandler Disposed;

        bool ISubscription.Handles(Type messageType)
        {
            return Snapshot.All(s => s.Handles(messageType));
        }

        public IEnumerator<ISubscription> GetEnumerator()
        {
            return Snapshot.GetEnumerator();
        }

        IEnumerable<ISubscription> ISubscriptionResolver.GetSubscriptionsFor(object message)
        {
            return Snapshot.Where(s => s.Handles(message.GetType())).ToArray();
        }

        /// <inheritdoc/>
        public bool Add(ISubscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException("subscription", "Attempt to add a Null Reference to Composite subscription.");
            try
            {
                _rwLock.EnterWriteLock();
                JustAdd(subscription);
                return true;
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void AddRange(IEnumerable<ISubscription> subscriptions)
        {
            foreach (var s in subscriptions)
                JustAdd(s);
        }

        private void JustAdd(ISubscription subscription)
        {
            _subscriptions.Add(subscription);
        }

        private IReadOnlyCollection<ISubscription> Snapshot
        {
            get
            {
                try
                {
                    _rwLock.EnterReadLock();
                    var disposableSubscriptions = _subscriptions.ToArray();
                    return disposableSubscriptions;
                }
                finally
                {
                    _rwLock.ExitReadLock();
                }
            }
        }
    }
}
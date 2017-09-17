using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TPLDataFlowTests
{
    public abstract class AbstractServices<TTarget> : IDisposable, IEnumerable<object> where TTarget : AbstractServices<TTarget>
    {
        private Dictionary<Type, IAttachedObject> _attachedObjects = new Dictionary<Type, IAttachedObject>();

        public virtual T Get<T>()
        {
            IAttachedObject obj;
            _attachedObjects.TryGetValue(typeof(T), out obj);
            return obj != null ? (T)obj.TheObject : default(T);
        }

        public virtual void Add<T>(T @object)
        {
            Add(@object, true);
        }

        public virtual void Add<T>(T @object, bool disposable)
        {
            if (_attachedObjects.ContainsKey(typeof(T)))
                throw new ArgumentException($"An object of Type {typeof(T).Name} is already attached to this context");
            _attachedObjects.Add(typeof(T), new AttachedObject<T>(disposable, @object));
        }

        protected void ForceAdd<T>(T @object)
        {
            _attachedObjects[typeof(T)] = (AttachedObject<T>)@object;
        }

        public virtual TTarget Replace<T>(T @object)
        {
            if (@object != null)
            {
                ForceAdd(@object);
            }
            return (TTarget)this;
        }

        public virtual void Remove<T>()
        {
            if (!_attachedObjects.ContainsKey(typeof(T))) return;
            _attachedObjects.Remove(typeof(T));
        }

        public virtual TTarget CloneContext()
        {
            var obj = (TTarget)MemberwiseClone();
            obj._attachedObjects = new Dictionary<Type, IAttachedObject>(_attachedObjects);
            return obj;
        }

        public string WhatDoIHave
        {
            get
            {
                var strings =
                (from entry in _attachedObjects
                    select string.Format("Access Type: {0}, Stored Object: {1}", entry.Key.FullName,
                        entry.Value.TheObject)).ToArray();
                return string.Join("\n", strings);
            }
        }

        ///<summary>
        /// If an added object is an extension, Unattach will be called, if the object implements Dispose,
        /// Dispose will be called, if both then both.
        ///</summary>
        public void Dispose()
        {
            var disposedObjects = new HashSet<object>();
            foreach (IAttachedObject o in _attachedObjects.Values)
            {
                var disp = o.TheObject as IDisposable;
                if (disp == null || !o.CanBeDisposed || disposedObjects.Contains(disp)) continue;
                disp.Dispose();
                disposedObjects.Add(disp);
            }
            _attachedObjects.Clear();
        }

        ///<summary>
        /// IAttachedObject interface.
        ///</summary>
        private interface IAttachedObject
        {
            ///<summary>
            /// Can be disposed.
            ///</summary>
            bool CanBeDisposed { get; }

            ///<summary>
            /// The object.
            ///</summary>
            object TheObject { get; }
        }

        ///<summary>
        /// Attached object.
        ///</summary>
        ///<typeparam name="T"></typeparam>
        private class AttachedObject<T> : IAttachedObject
        {
            ///<summary>
            /// The object.
            ///</summary>
            private readonly T theObject;

            public AttachedObject(bool canBeDisposed, T theObject)
            {
                CanBeDisposed = canBeDisposed;
                this.theObject = theObject;
            }

            ///<summary>
            /// Can be disposed.
            ///</summary>
            public bool CanBeDisposed { get; private set; }

            object IAttachedObject.TheObject
            {
                get { return theObject; }
            }

            ///<summary>
            /// The object.
            ///</summary>
            public static implicit operator T(AttachedObject<T> attachedObject)
            {
                return attachedObject.theObject;
            }

            ///<summary>
            /// Attached object.
            ///</summary>
            public static implicit operator AttachedObject<T>(T theObject)
            {
                return new AttachedObject<T>(true, theObject);
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            return _attachedObjects.Values.Select(a => a.TheObject).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

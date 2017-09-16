using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TPLDataFlowTests
{
    /// <summary>
    /// Context class
    /// </summary>
    public class StandardServices : AbstractServices<StandardServices>
    {
    }

    public abstract class AbstractServices<TTarget> : IServices<TTarget>, IDisposable, IEnumerable<object> where TTarget : AbstractServices<TTarget>
    {

        /// <summary>
        /// The list of objects attached to this context. These may be context extensions
        /// or other objects
        /// </summary>
        private Dictionary<Type, IAttachedObject> _attachedObjects =
            new Dictionary<Type, IAttachedObject>();

        /// <summary>
        /// Adds a OCntext extension to this context instance
        /// </summary>
        /// <typeparam name="T">The type of the extension</typeparam>
        /// <param name="extension">The extension which must implement the <see cref="IServicesExtension{T}"/> interface</param>
        public virtual void AddExtension<T>(T extension) where T : IServicesExtension<TTarget>
        {
            if (_attachedObjects.ContainsKey(typeof(T)))
                throw new ArgumentException(string.Format("An object of Type {0} is already attached to this context",
                    typeof(T).Name));
            _attachedObjects.Add(typeof(T), (AttachedObject<T>) extension);
            extension.Attach((TTarget) this);
        }

        /// <summary>
        /// Remove an extension
        /// </summary>
        /// <typeparam name="T">The type of the extension to remove</typeparam>
        public virtual void RemoveExtension<T>() where T : IServicesExtension<TTarget>
        {
            if (!_attachedObjects.ContainsKey(typeof(T))) return;
            Remove((IServicesExtension<TTarget>) _attachedObjects[typeof(T)].TheObject);
        }

        /// <summary>
        /// Remove a specific extension
        /// </summary>
        /// <param name="extension">The extension instance to remove</param>
        public virtual void Remove(IServicesExtension<TTarget> extension)
        {
            var ext = _attachedObjects.Values.Where(e => e.TheObject == extension).FirstOrDefault();
            if (ext == null) return;
            ((IServicesExtension<TTarget>) ext.TheObject).Remove((TTarget) this);
            _attachedObjects.Remove(ext.TheObject.GetType());
        }

        /// <summary>
        /// Get the Object from the context of type T
        /// </summary>
        /// <typeparam name="T">the type of the object to retrieve</typeparam>
        /// <returns>The instance of said type or teh default value of T</returns>
        public virtual T Get<T>()
        {
            IAttachedObject obj;
            _attachedObjects.TryGetValue(typeof(T), out obj);
            return obj != null ? (T) obj.TheObject : default(T);
        }

        /// <summary>
        /// Add any object of type T
        /// </summary>
        /// <typeparam name="T">The type of teh object to be added</typeparam>
        /// <param name="object">The instance to be added</param>
        public virtual void Add<T>(T @object)
        {
            Add(@object, true);
        }

        ///<summary>
        /// Add.
        ///</summary>
        public virtual void Add<T>(T @object, bool disposable)
        {
            if (_attachedObjects.ContainsKey(typeof(T)))
                throw new ArgumentException($"An object of Type {typeof(T).Name} is already attached to this context");
            _attachedObjects.Add(typeof(T), new AttachedObject<T>(disposable, @object));
        }

        ///<summary>
        /// Force add.
        ///</summary>
        ///<param name="object"></param>
        ///<typeparam name="T"></typeparam>
        protected void ForceAdd<T>(T @object)
        {
            _attachedObjects[typeof(T)] = (AttachedObject<T>) @object;
            if ((@object != null) && @object.GetType().ImplementsInterface<IServicesExtension<TTarget>>())
            {
                ((IServicesExtension<TTarget>) @object).Attach((TTarget) this);
            }
        }

        /// <summary>
        /// Replace a certain object of type T with a new one of the same type
        /// </summary>
        /// <typeparam name="T">The type of the object to be added</typeparam>
        /// <param name="object">The instance</param>
        /// <returns>This instance</returns>
        public virtual TTarget Replace<T>(T @object)
        {
            if (@object != null)
            {
                ForceAdd(@object);
            }
            return (TTarget) this;
        }

        /// <summary>
        /// Remove an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to be removed</typeparam>
        public virtual void Remove<T>()
        {
            if (!_attachedObjects.ContainsKey(typeof(T))) return;
            _attachedObjects.Remove(typeof(T));
        }

        /// <summary>
        /// Get a clone of this context. Its initial state will be the same as the cloned context,
        /// however you can modify the copy in any way you want.
        /// </summary>
        /// <returns>A new instance in the same state as the original context</returns>
        public virtual TTarget CloneContext()
        {
            var obj = (TTarget) MemberwiseClone();
            obj._attachedObjects = new Dictionary<Type, IAttachedObject>(_attachedObjects);
            return obj;
        }

        /// <summary>
        /// <see cref="IServices{TTarget}.WhatDoIHave()"/>
        /// </summary>
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
                var extension = o.TheObject as IServicesExtension<TTarget>;
                if (extension != null)
                    extension.Remove((TTarget) this);
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
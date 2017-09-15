namespace TPLDataFlowTests
{
    public interface IServices
    {
        void Add<T>(T @object);
        void Remove<T>();
        T Get<T>();
        string WhatDoIHave { get; }
    }

    public interface IServices<TTarget> : IServices
    {
        void AddExtension<T>(T extension) where T : IServicesExtension<TTarget>;
        void RemoveExtension<T>() where T : IServicesExtension<TTarget>;
        TTarget Replace<T>(T @object);
        TTarget CloneContext();
    }

    public interface IServicesExtension<T>
    {
        void Attach(T target);
        void Remove(T target);
    }
}
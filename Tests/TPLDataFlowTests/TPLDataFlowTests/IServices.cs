namespace TPLDataFlowTests
{
    public interface IServices
    {
        void Add<T>(T @object);
        void Remove<T>();
        T Get<T>();
        string WhatDoIHave { get; }
    }

    public interface IServices<out TTarget> : IServices
    {
        TTarget Replace<T>(T @object);
        TTarget CloneContext();
    }

    public interface IServicesExtension<T>
    {
        void Attach(T target);
        void Remove(T target);
    }
}
namespace TPLDataFlowTests
{
    public interface IServices
    {
        void Add<T>(T @object);

        void Remove<T>();

        T Get<T>();

        string WhatDoIHave { get; }
    }
}
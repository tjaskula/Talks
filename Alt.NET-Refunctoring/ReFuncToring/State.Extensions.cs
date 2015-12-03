using System;

namespace ReFuncToring
{
    public static partial class StateMan
    {
        public abstract class Result<T>
        {
            public bool IsSuccess { get; protected set; }
        }

        public class Success<T> : Result<T>
        {
            public Success(T state)
            {
                State = state;
                IsSuccess = true;
            }

            public T State { get; private set; }
        }

        public class Error<T> : Result<T>
        {
            public Error(string message)
            {
                Message = message;
                IsSuccess = false;
            }

            public string Message { get; private set; }
        }

        public static string FromError<T>(this Result<T> state)
        {
            var error = state as Error<T>;

            if (error == null)
                throw new InvalidOperationException("There is no error.");

            return error.Message;
        }

        public delegate Result<R> StatePiper<T, R>(T input);

        public static Result<T> ToState<T>(this T value)
        {
            return new Success<T>(value);
        }

        public static T FromState<T>(this Result<T> state)
        {
            var newState = state as Success<T>;

            if (newState == null)
                throw new InvalidOperationException("Cannot extract state from error");

            return newState.State;
        }

        public static StatePiper<T, R> ToStatePiper<T, R>(this Func<T, Result<R>> stateFunc)
        {
            return input => stateFunc(input);
        }

        public static StatePiper<TSource, TResult> Bind<TSource, TResultTemp, TResult>(
                            this StatePiper<TSource, TResultTemp> s, Func<TResultTemp, Result<TResult>> func)
        {
            return input =>
            {
                var r1 = s(input);
                var ar1 = r1 as Success<TResultTemp>;

                if (ar1 == null)
                {
                    return new Error<TResult>(((Error<TResultTemp>)r1).Message);
                }

                return func(ar1.State);
            };
        }

        public static StatePiper<TSource, TResult> SelectMany<TSource, TResultTemp, TCollection, TResult>(
                        this StatePiper<TSource, TResultTemp> source,
                        Func<TResultTemp, StatePiper<TResultTemp, TCollection>> collectionSelector,
                        Func<TResultTemp, TCollection, TResult> resultSelector)
        {
            return source.Bind(a => collectionSelector(a).Bind(b => resultSelector(a, b).ToState())(a));
        }

        public static Result<R> Bind<A, R>(this Result<A> state, Func<A, Result<R>> function)
        {
            var newState = state as Success<A>;

            return newState == null
                ? new Error<R>(((Error<A>)state).Message)
                : function(newState.State);
        }

        public static Result<C> SelectMany<A, B, C>(this Result<A> a, Func<A, Result<B>> func, Func<A, B, C> select)
        {
            return a.Bind(aval =>
                    func(aval).Bind(bval =>
                    select(aval, bval).ToState()));
        }
    }
}
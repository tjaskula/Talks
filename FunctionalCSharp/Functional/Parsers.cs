using System;

namespace Functional
{
    public static class Parsers
    {
        public abstract class ParseResult<T>
        {
        }

        public class Success<T> : ParseResult<T>
        {
            public Success(T parsed)
            {
                Parsed = parsed;
            }

            public T Parsed { get; private set; }
        }

        public class Error<T> : ParseResult<T>
        {
            public Error(string message)
            {
                Message = message;
            }

            public string Message { get; private set; }
        }

        public static ParseResult<R> Bind<A, R>(this ParseResult<A> parsed, Func<A, ParseResult<R>> function)
        {
            var parseda = parsed as Success<A>;

            return parseda == null
                ? new Error<R>(((Error<A>)parsed).Message)
                : function(parseda.Parsed);
        }

        public static ParseResult<T> ToParseResult<T>(this T value)
        {
            return new Success<T>(value);
        }

        public static ParseResult<C> SelectMany<A, B, C>(this ParseResult<A> a, Func<A, ParseResult<B>> func, Func<A, B, C> select)
        {
            return a.Bind(aval =>
                    func(aval).Bind(bval =>
                    select(aval, bval).ToParseResult()));
        }

        public delegate ParseResult<R> Parser<T, R>(T input);

        public static Parser<T, R> ToParser<T, R>(this Func<T, ParseResult<R>> parsingFunc)
        {
            return input => parsingFunc(input);
        }

        public static Parser<T, V> Compose<T, R, V>(this Parser<T, R> p1, Parser<R, V> p2)
        {
            return input =>
                    {
                        var r1 = p1(input);
                        return r1.Bind(r => p2(r));
                    };
        }
    }
}
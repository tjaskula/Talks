using System;

namespace ReFuncToring
{
    public static class ResultExt
    {
        public static Result<TSource, TResult> Select<TSource, TResultTemp, TResult>(this Result<TSource, TResultTemp> self, Func<TResultTemp, TResult> selector)
        {
            if (self.IsSuccess) return Result.Success<TSource, TResult>(selector(self.Success));
            return Result.Error<TSource, TResult>(self.Error);
        }

        public static Result<TSource, TResult> SelectMany<TSource, TResultTemp, TCollection, TResult>(
            this Result<TSource, TResultTemp> self, 
            Func<TResultTemp, Result<TSource, TCollection>> collectionSelector, 
            Func<TResultTemp, TCollection, TResult> projector)
        {
            var r1 = self.Select(s => collectionSelector(s).Select(col => projector(s, col)));

            if (self.IsError)
                return Result.Error<TSource, TResult>(self.Error);

            var res = collectionSelector(self.Success);
            if (res.IsError)
                return Result.Error<TSource, TResult>(self.Error);

            var r2 = Result.Success<TSource, TResult>(projector(self.Success, res.Success));
            return r2;
        }
    }
}
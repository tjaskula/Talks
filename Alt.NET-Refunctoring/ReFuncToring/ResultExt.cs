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
            var result = self.Select(s => collectionSelector(s).Select(col => projector(s, col)));
            if (result.IsSuccess) return result.Success;
            return Result.Error<TSource, TResult>(result.Error);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace NC.Common.Extensions
{
    public static class CommonExtensions
    {
        public static TResult[] MapToArray<TSource, TResult>(this IEnumerable<TSource> items, Func<TSource, TResult> toResult)
        {
            return items == null ? EmptyArray<TResult>.Instance : items.Select(toResult).ToArray();
        }
    }
}

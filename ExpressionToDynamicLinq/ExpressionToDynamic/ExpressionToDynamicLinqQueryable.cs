using System.Collections;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class ExpressionToDynamicLinqQueryable
    {
        public static string ToExpressionString(this Expression expression)
        {
            return new ExpressionStringToDynamicLinq(expression).ToExpressionString();
        }
        public static PredicateQueryable ToExpressionPredicate(this Expression expression)
        {
            return new ExpressionPredicateToDynamicLinq(expression).ToExpressionPredicate();
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, PredicateQueryable predicate)
        {
            return Where(source.AsQueryable(), predicate);
        }
        public static IEnumerable<TSource> Where<TSource>(this IQueryable<TSource> source, PredicateQueryable predicate)
        {
            var _params = predicate.Parameters.Select(f => f.Value).ToArray();
#if !(NET35 || SILVERLIGHT || NETFX_CORE || WINDOWS_APP || DOTNET5_1 || UAP10_0 || NETSTANDARD)
            return source.Where(predicate.Predicate, _params);
#endif
#if NETSTANDARD2_0
            return System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(source, predicate.Predicate, _params);
#endif
        }
        public static IEnumerable Select(this IEnumerable source, PredicateQueryable selector)
        {
            return Select(source.AsQueryable(), selector);
        }
        public static IQueryable Select(this IQueryable source, PredicateQueryable selector)
        {
#if !(NET35 || SILVERLIGHT || NETFX_CORE || WINDOWS_APP || DOTNET5_1 || UAP10_0 || NETSTANDARD)
               return source.Select(selector.Predicate, selector.Parameters);
#endif
#if NETSTANDARD2_0
            var _params = selector.Parameters.Select(f => f.Value).ToArray();
            return System.Linq.Dynamic.Core.DynamicQueryableExtensions.Select(source, selector.Predicate, _params);
#endif
        }

    }
}
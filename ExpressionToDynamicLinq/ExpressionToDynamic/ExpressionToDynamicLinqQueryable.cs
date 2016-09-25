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
            return new ExpressionToDynamicLinq(expression).ToExpressionString();
        }
        public static PredicateQueryable ToExpressionPredicate(this Expression expression)
        {
            return new ExpressionToDynamicLinq2(expression).ToExpressionPredicate();
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, PredicateQueryable predicate)
        {
            var _params = predicate.Parameters.Select(f => f.Value).ToArray(); 
            return source.Where(predicate.Predicate, _params);
        }
        public static IEnumerable Select(this IEnumerable source, PredicateQueryable selector)
        {
            var _params = selector.Parameters.Select(f => f.Value).ToArray(); 
            return source.Select(selector.Predicate, _params);
        }
    }
}
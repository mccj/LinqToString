using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq.Dynamic
{
    public static class ExpressionToDynamicLinqQueryable
    {
        //[System.Obsolete("使用 ToStringExpression")]
        //public static string ToExpressionString(this Expression expression)
        //{
        //    return ToStringExpression(expression);
        //}
        //[System.Obsolete("使用 ToPredicateExpression")]
        //public static PredicateQueryable ToExpressionPredicate(this Expression expression)
        //{
        //    return ToPredicateExpression(expression);
        //}
        public static string ToStringExpression(this Expression expression)
        {
            return new ExpressionStringToDynamicLinq(expression).ToExpressionString();
        }
        public static PredicateQueryable ToPredicateExpression(this Expression expression)
        {
            return new ExpressionPredicateToDynamicLinq(expression).ToExpressionPredicate();
        }
        //public static LambdaExpression ToLambdaExpression(this Expression expression)
        //{
        //    //Check.NotNull(source, nameof(source));
        //    //Check.NotNull(config, nameof(config));
        //    //Check.NotEmpty(predicate, nameof(predicate));
        //    //var config = Core.ParsingConfig.Default;
        //    //bool createParameterCtor =false ;//SupportsLinqToObjects(config, source);
        //    //LambdaExpression lambda = Core.DynamicExpressionParser.ParseLambda(config, createParameterCtor, source.ElementType, null, predicate, args);

        //    //return new ExpressionPredicateToDynamicLinq(expression).ToExpressionPredicate();
        //    return null;
        //}

        #region Where
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, PredicateQueryable predicate)
        {
            return Where(source.AsQueryable(), predicate);
        }
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, PredicateQueryable predicate)
        {
            var _params = predicate.Parameters.Select(f => f.Value).ToArray();
#if Kahanu_System_Linq_Dynamic && !(NET35 || SILVERLIGHT || NETFX_CORE || WINDOWS_APP || DOTNET5_1 || UAP10_0 || NETSTANDARD)
            return source.Where(predicate.Predicate, _params);
#endif
#if !Kahanu_System_Linq_Dynamic || NETSTANDARD2_0
            return System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(source, predicate.Predicate, _params);
#endif
        }
        #endregion Where
        #region Select
        public static IEnumerable Select(this IEnumerable source, PredicateQueryable selector)
        {
            return Select(source.AsQueryable(), selector);
        }
        public static IQueryable Select(this IQueryable source, PredicateQueryable selector)
        {
            var _params = selector.Parameters.Select(f => f.Value).ToArray();
#if Kahanu_System_Linq_Dynamic && !(NET35 || SILVERLIGHT || NETFX_CORE || WINDOWS_APP || DOTNET5_1 || UAP10_0 || NETSTANDARD)
               return source.Select(selector.Predicate, _params);
#endif
#if !Kahanu_System_Linq_Dynamic || NETSTANDARD2_0
            return System.Linq.Dynamic.Core.DynamicQueryableExtensions.Select(source, selector.Predicate, _params);
#endif
        }
        #endregion Select
    }
}
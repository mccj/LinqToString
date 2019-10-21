using System.Collections;
using System.Collections.Generic;
using System.Linq.Dynamic;
//using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace System.Linq
{
    public static class DynamicQueryableExtension
    {
#if NETCOREAPP2_0 || !Kahanu_System_Linq_Dynamic
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, string predicate, string defaultValue = null)
        {
            var s = source.AsQueryable();
            return Where(s, predicate, defaultValue);
        }
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, string defaultValue = null)
        {
            if (!string.IsNullOrWhiteSpace(predicate))
                return System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(source, predicate);
            else if (!string.IsNullOrWhiteSpace(defaultValue))
                return System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(source, defaultValue);
            else
                return source;
        }
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, string predicate, params object[] args)
        {
            return System.Linq.Dynamic.Core.DynamicQueryableExtensions.Where(source.AsQueryable(), predicate, args);
        }
        public static IEnumerable Select<T>(this IEnumerable<T> source, string predicate, params object[] args)
        {
            return System.Linq.Dynamic.Core.DynamicQueryableExtensions.Select(source.AsQueryable(), predicate, args);
        }
#endif
    }
}
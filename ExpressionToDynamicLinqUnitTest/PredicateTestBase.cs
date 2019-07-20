using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
{
    public class PredicateTestBase : PredicateTestBase<Model1>
    {

        //public void TestMode()
        //{

        //}
    }
    public class PredicateTestBase<TModel>
    {
        public (PredicateQueryable TestPredicate, string TestString) Test(Expression expression,  TModel[] models = null)
        {
            var t1 = TestToPredicate(expression, models);
            var t2 = TestToString(expression, models);
            return (TestPredicate: t1, TestString: t2);
        }
        public string TestToString(Expression expression, TModel[] models = null)
        {
            var s1 = expression.ToStringExpression();

            if (models == null)
                models = new TModel[] { };
            var m1 = models.Where(s1).ToArray();

            return s1;
        }
        public PredicateQueryable TestToPredicate(Expression expression, TModel[] models = null)
        {
            var s1 = expression.ToPredicateExpression();

            if (models == null)
                models = new TModel[] { };
            var m1 = models.Where(s1).ToArray();

            return s1;
        }

    }
}

using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    public class PredicateTestBase : PredicateTestBase<Model1>
    {

        //public void TestMode()
        //{

        //}
    }
    public class PredicateTestBase<TModel>
    {
        public PredicateQueryable Test(Expression expression, string predicate, TModel[] models = null)
        {
            var s1 = expression.ToPredicateExpression();
            //Assert.AreEqual(s1.Predicate, predicate);

            if (models == null)
                models = new TModel[] { };
            var m1 = models.Where(s1).ToArray();

            return s1;
        }

    }
}

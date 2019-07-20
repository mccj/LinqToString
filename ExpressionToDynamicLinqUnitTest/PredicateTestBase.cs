using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
{
    public class PredicateTestBase : PredicateTestBase<Model1>
    {
        private static Model1[] model1s = null;
        public override Model1[] getModes()
        {
            if (model1s == null)
                model1s = new Model1[] { new Model1 {
                    Name="mccj",
                    B1=2.2M,
                     B5=new Model2{  Name="mccj", Age=6,B1=2.2M},
                      Age=6
                } };

            return model1s;
        }
    }
    public class PredicateTestBase<TModel>
    {
        public virtual TModel[] getModes()
        {
            return null;
        }
        public (PredicateQueryable TestPredicate, string TestString) Test(Expression expression, TModel[] models = null)
        {
            var t1 = TestToPredicate(expression, models);
            var t2 = TestToString(expression, models);
            return (TestPredicate: t1, TestString: t2);
        }
        public string TestToString(Expression expression, TModel[] models = null)
        {
            var s1 = expression.ToStringExpression();

            if (models == null)
                models = getModes();
            var m1 = models.Where(s1).ToArray();

            var ssdd = expression as Expression<Func<TModel, bool>>;
            var dddd = ssdd.Compile();
            var m2 = models.Where(dddd).ToArray();

            Assert.IsTrue(m1.Length > 0);
            Assert.IsTrue(m1.Length == m2.Length);
            for (int i = 0; i < m1.Length; i++)
            {
                Assert.AreEqual(m1.ElementAt(i), m2.ElementAt(i));
            }
            return s1;
        }
        public PredicateQueryable TestToPredicate(Expression expression, TModel[] models = null)
        {
            var s1 = expression.ToPredicateExpression();

            if (models == null)
                models = getModes();
            var m1 = models.Where(s1).ToArray();

            var ssdd = expression as Expression<Func<TModel, bool>>;
            var dddd = ssdd.Compile();
            var m2 = models.Where(dddd).ToArray();

            Assert.IsTrue(m1.Length > 0);
            Assert.IsTrue(m1.Length == m2.Length);
            for (int i = 0; i < m1.Length; i++)
            {
                Assert.AreEqual(m1.ElementAt(i), m2.ElementAt(i));
            }

            return s1;
        }

    }
}

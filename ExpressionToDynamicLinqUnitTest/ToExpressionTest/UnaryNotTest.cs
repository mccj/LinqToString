using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
{
    [TestClass]
    public class UnaryNotTest : PredicateTestBase
    {
        [TestMethod]
        public void UnaryNotBool()
        {
            var value = true;
            Expression<Func<Model1, bool>> expression1 = f => (!(!value != f.B4));
            Expression<Func<Model1, bool>> expression2 = f => (!(!true != f.B4));
            Expression<Func<Model1, bool>> expression3 = f => (!(!true != f.B5.B4));
            Expression<Func<Model1, bool>> expression4 = f => (!(!f.B4 != f.B5.B4));

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(!((!True) != (it).B4))");
            //Assert.AreEqual(s2.Predicate, "(!(False != (it).B4))");
            //Assert.AreEqual(s3.Predicate, "(!(False != (it).B5.B4))");
            //Assert.AreEqual(s4.Predicate, "(!((!(it).B4) != (it).B5.B4))");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
    }
}

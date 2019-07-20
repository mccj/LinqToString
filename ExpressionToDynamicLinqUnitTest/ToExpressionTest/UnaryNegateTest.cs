using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
{
    [TestClass]
    public class UnaryNegateTest : PredicateTestBase
    {
        [TestMethod]
        public void UnaryNegateInt()
        {
            var value = 1;
            Expression<Func<Model1, bool>> expression1 = f => (-value <= -f.Age);
            Expression<Func<Model1, bool>> expression2 = f => (-1 <=- f.Age);
            Expression<Func<Model1, bool>> expression3 = f => (-(1+2) <= -f.B5.Age);
            Expression<Func<Model1, bool>> expression4 = f => (-f.Age <=- f.B5.Age);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "((-1) <= (-(it).Age))");
            //Assert.AreEqual(s2.Predicate, "(-1 <= (-(it).Age))");
            //Assert.AreEqual(s3.Predicate, "(-3 <= (-(it).B5.Age))");
            //Assert.AreEqual(s4.Predicate, "((-(it).Age) <= (-(it).B5.Age))");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void UnaryNegateDecimal()
        {
            var value = 1.11M;
            Expression<Func<Model1, bool>> expression1 = f => (-value <= -f.B1);
            Expression<Func<Model1, bool>> expression2 = f => (-1.11M <= -f.B1);
            Expression<Func<Model1, bool>> expression3 = f => (-(1.11M+2) <= -f.B5.B1);
            Expression<Func<Model1, bool>> expression4 = f => (-f.B1 <= -f.B5.B1);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "((-1.11) <= (-(it).B1))");
            //Assert.AreEqual(s2.Predicate, "(-1.11 <= (-(it).B1))");
            //Assert.AreEqual(s3.Predicate, "(-3.11 <= (-(it).B5.B1))");
            //Assert.AreEqual(s4.Predicate, "((-(it).B1) <= (-(it).B5.B1))");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
    }
}

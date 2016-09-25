using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    [TestClass]
    public class UnaryNegateTest
    {
        [TestMethod]
        public void UnaryNegateInt()
        {
            var value = 1;
            Expression<Func<Model1, bool>> expression1 = f => (-value <= -f.Age);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (-1 <=- f.Age);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (-(1+2) <= -f.B5.Age);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (-f.Age <=- f.B5.Age);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "((-1) <= (-Age))");
            Assert.AreEqual(s2, "(-1 <= (-Age))");
            Assert.AreEqual(s3, "(-3 <= (-B5.Age))");
            Assert.AreEqual(s4, "((-Age) <= (-B5.Age))");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void UnaryNegateDecimal()
        {
            var value = 1.11M;
            Expression<Func<Model1, bool>> expression1 = f => (-value <= -f.B1);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (-1.11M <= -f.B1);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (-(1.11M+2) <= -f.B5.B1);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (-f.B1 <= -f.B5.B1);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "((-1.11) <= (-B1))");
            Assert.AreEqual(s2, "(-1.11 <= (-B1))");
            Assert.AreEqual(s3, "(-3.11 <= (-B5.B1))");
            Assert.AreEqual(s4, "((-B1) <= (-B5.B1))");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
    }
}

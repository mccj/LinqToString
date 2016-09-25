using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    [TestClass]
    public class UnaryNotTest
    {
        [TestMethod]
        public void UnaryNotBool()
        {
            var value = true;
            Expression<Func<Model1, bool>> expression1 = f => (!(!value != f.B4));
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (!(!true != f.B4));
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (!(!true != f.B5.B4));
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (!(!f.B4 != f.B5.B4));
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "(!((!True) != B4))");
            Assert.AreEqual(s2, "(!(False != B4))");
            Assert.AreEqual(s3, "(!(False != B5.B4))");
            Assert.AreEqual(s4, "(!((!B4) != B5.B4))");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
    }
}

using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class PrimaryNewTest
    {
        [TestMethod]
        public void PrimaryNewString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr[0] == new { aaa = "sss" }.aaa);
            var s1 = expression1.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }[0] == new { aaa = f.Name }.aaa);
            var s2 = expression2.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }[0] == new { aaa = f.Name }.aaa);
            var s3 = expression3.ToExpressionPredicate();

            Assert.AreEqual(s1.Predicate, "(\"a1\" == \"sss\")");
            Assert.AreEqual(s2.Predicate, "(\"a1\" == new((it).Name as aaa).aaa)");
            Assert.AreEqual(s3.Predicate, "(\"a1\" == new((it).Name as aaa).aaa)");
        }
    }
}

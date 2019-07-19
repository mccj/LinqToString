using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    [TestClass]
    public class PrimaryNewTest
    {
        [TestMethod]
        public void PrimaryNewString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr[0] == new { aaa = "sss" }.aaa);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }[0] == new { aaa = f.Name }.aaa);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }[0] == new { aaa = f.Name }.aaa);
            var s3 = expression3.ToExpressionString();

            Assert.AreEqual(s1, "(\"a1\" == \"sss\")");
            Assert.AreEqual(s2, "(\"a1\" == new((it).Name as aaa).aaa)");
            Assert.AreEqual(s3, "(\"a1\" == new((it).Name as aaa).aaa)");
        }
    }
}

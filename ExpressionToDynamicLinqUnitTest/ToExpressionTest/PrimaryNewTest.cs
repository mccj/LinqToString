using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
{
    [TestClass]
    public class PrimaryNewTest : PredicateTestBase
    {
        [TestMethod]
        public void PrimaryNewString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr[0] == new { aaa = "sss" }.aaa);

            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }[0] == new { aaa = f.Name }.aaa);
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }[0] == new { aaa = f.Name }.aaa);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(\"a1\" == \"sss\")");
            //Assert.AreEqual(s2.Predicate, "(\"a1\" == new((it).Name as aaa).aaa)");
            //Assert.AreEqual(s3.Predicate, "(\"a1\" == new((it).Name as aaa).aaa)");
        }
    }
}

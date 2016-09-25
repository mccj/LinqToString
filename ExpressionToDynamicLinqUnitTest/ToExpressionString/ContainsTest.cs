using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    [TestClass]
    public class OperatorsContainsTest
    {
        [TestMethod]
        public void ContainsTestString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Contains(f.Name));
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }.Contains(f.Name));
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }.Contains(f.B5.Name));
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.B8.Contains(f.B5.Name));
            var s4 = expression4.ToExpressionString();

            Assert.AreEqual(s1, "(Name = \"a1\" || Name = \"a2\" || Name = \"a3\")");
            Assert.AreEqual(s2, "(Name = \"a1\" || Name = \"a2\" || Name = \"a3\")");
            Assert.AreEqual(s3, "(B5.Name = \"a1\" || B5.Name = \"a2\" || B5.Name = \"a3\")");
            Assert.AreEqual(s4, "B8.Contains(B5.Name)");
        }
        [TestMethod]
        public void ContainsTestEnum()
        {
            var arrStr = new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Contains(f.State));
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Contains(f.State));
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Contains(f.B5.State));
            var s3 = expression3.ToExpressionString();

            Assert.AreEqual(s1, "(State = \"State1\" || State = \"State2\" || State = \"State3\")");
            Assert.AreEqual(s2, "(State = \"State1\" || State = \"State2\" || State = \"State3\")");
            Assert.AreEqual(s3, "(B5.State = \"State1\" || B5.State = \"State2\" || B5.State = \"State3\")");
        }
    }
}

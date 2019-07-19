using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    [TestClass]
    public class OperatorsAnyTest
    {
        [TestMethod]
        public void OperatorsAnyTestString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Any(ff => f.Name == ff));
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }.Any(ff => f.Name == ff));
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }.Any(ff => f.B5.Name == ff));
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.B8.Any(ff => f.B5.Name == ff));
            var s4 = expression4.ToExpressionString();
            Expression<Func<Model1, bool>> expression5 = f => (f.B8.Any());
            var s5 = expression5.ToExpressionString();
            Expression<Func<Model1, bool>> expression6 = f => (f.B6.Any(ff => f.B5.Name == ff.Name));
            var s6 = expression6.ToExpressionString();

            Assert.AreEqual(s1, "(((it).Name == \"a1\") || ((it).Name == \"a2\") || ((it).Name == \"a3\"))");
            Assert.AreEqual(s2, "(((it).Name == \"a1\") || ((it).Name == \"a2\") || ((it).Name == \"a3\"))");
            Assert.AreEqual(s3, "(((it).B5.Name == \"a1\") || ((it).B5.Name == \"a2\") || ((it).B5.Name == \"a3\"))");
            Assert.AreEqual(s4, "(it).B8.Any(((outerIt).B5.Name == (it)))");
            Assert.AreEqual(s5, "(it).B8.Any()");
            Assert.AreEqual(s6, "(it).B6.Any(((outerIt).B5.Name == (it).Name))");

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B6 = new Model2[] { }, B8 = new[] { "" } } };
            model.Where(s1).ToArray();
            model.Where(s2).ToArray();
            model.Where(s3).ToArray();
            model.Where(s4).ToArray();
            model.Where(s5).ToArray();
            model.Where(s6).ToArray();
        }
        [TestMethod]
        public void OperatorsAnyTestEnum()
        {
            var arrStr = new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Any(ff => f.State == ff));
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Any((ff => f.State == ff)));
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Any((ff => f.B5.State == ff)));
            var s3 = expression3.ToExpressionString();

            Assert.AreEqual(s1, "(((it).State == \"State1\") || ((it).State == \"State2\") || ((it).State == \"State3\"))");
            Assert.AreEqual(s2, "(((it).State == \"State1\") || ((it).State == \"State2\") || ((it).State == \"State3\"))");
            Assert.AreEqual(s3, "(((it).B5.State == \"State1\") || ((it).B5.State == \"State2\") || ((it).B5.State == \"State3\"))");

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B8 = new[] { "" } } };
            model.Where(s1).ToArray();
            model.Where(s2).ToArray();
            model.Where(s3).ToArray();
        }
    }
}

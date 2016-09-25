using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    [TestClass]
    public class PrimaryArrayIndexTest
    {
        [TestMethod]
        public void PrimaryArrayIndexTestString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr[0] == (f.Name));
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }[0] == (f.Name));
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }[0] == (f.B5.Name));
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.B6[0].Age == 1 + f.Age);
            var s4 = expression4.ToExpressionString();
            Expression<Func<Model1, bool>> expression5 = f => (f.B6[0].B4);
            var s5 = expression5.ToExpressionString();
            Expression<Func<Model1, bool>> expression6 = f => (f.B7["ssss"].B4);
            var s6 = expression6.ToExpressionString();

            Assert.AreEqual(s1, "(\"a1\" == Name)");
            Assert.AreEqual(s2, "(\"a1\" == Name)");
            Assert.AreEqual(s3, "(\"a1\" == B5.Name)");
            Assert.AreEqual(s4, "(B6[0].Age == (1 + Age))");
            Assert.AreEqual(s5, "B6[0].B4");
            Assert.AreEqual(s6, "B7[\"ssss\"].B4");


            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B6 = new Model2[] { new Model2 { } }, B7 = new System.Collections.Generic.Dictionary<string, Model2>() { { "ssss", new Model2 { } } } } };
            model.Where(s1).ToArray();
            model.Where(s2).ToArray();
            model.Where(s3).ToArray();
            model.Where(s4).ToArray();
            model.Where(s5).ToArray();
            model.Where(s6).ToArray();
        }
    }
}

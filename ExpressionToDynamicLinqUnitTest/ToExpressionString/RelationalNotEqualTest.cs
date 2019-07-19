using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    [TestClass]
    public class RelationalNotEqualTest
    {
        [TestMethod]
        public void NotEqualString()
        {
            var value = "a1";
            Expression<Func<Model1, bool>> expression1 = f => (value != f.Name);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => ("a1" != f.Name);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => ("a1" != f.B5.Name);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.Name != f.B5.Name);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "(\"a1\" != (it).Name)");
            Assert.AreEqual(s2, "(\"a1\" != (it).Name)");
            Assert.AreEqual(s3, "(\"a1\" != (it).B5.Name)");
            Assert.AreEqual(s4, "((it).Name != (it).B5.Name)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void NotEqualEnum()
        {
            var value = StateEnum.State2;
            Expression<Func<Model1, bool>> expression1 = f => (value != f.State);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (f.State != StateEnum.State2);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (StateEnum.State2 != f.B5.State);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.State != f.B5.State);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "(\"State2\" != (it).State)");
            Assert.AreEqual(s2, "((it).State != \"State2\")");
            Assert.AreEqual(s3, "(\"State2\" != (it).B5.State)");
            Assert.AreEqual(s4, "((it).State != (it).B5.State)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void NotEqualBool()
        {
            var value = true;
            Expression<Func<Model1, bool>> expression1 = f => (value != f.B4);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (true != f.B4);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (true != f.B5.B4);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.B4 != f.B5.B4);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "(True != (it).B4)");
            Assert.AreEqual(s2, "(True != (it).B4)");
            Assert.AreEqual(s3, "(True != (it).B5.B4)");
            Assert.AreEqual(s4, "((it).B4 != (it).B5.B4)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void NotEqualInt()
        {
            var value = 1;
            Expression<Func<Model1, bool>> expression1 = f => (value != f.Age);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (1 != f.Age);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (1 != f.B5.Age);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.Age != f.B5.Age);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "(1 != (it).Age)");
            Assert.AreEqual(s2, "(1 != (it).Age)");
            Assert.AreEqual(s3, "(1 != (it).B5.Age)");
            Assert.AreEqual(s4, "((it).Age != (it).B5.Age)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void NotEqualDecimal()
        {
            var value = 1.11M;
            Expression<Func<Model1, bool>> expression1 = f => (value != f.B1);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (1.11M != f.B1);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (1.11M != f.B5.B1);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.B1 != f.B5.B1);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "(1.11 != (it).B1)");
            Assert.AreEqual(s2, "(1.11 != (it).B1)");
            Assert.AreEqual(s3, "(1.11 != (it).B5.B1)");
            Assert.AreEqual(s4, "((it).B1 != (it).B5.B1)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
    }
}

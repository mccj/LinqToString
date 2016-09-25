using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class MultiplicativeModuloTest
    {
        [TestMethod]
        public void MultiplicativeModuloInt()
        {
            var value = 1;
            Expression<Func<Model1, bool>> expression1 = f => (value % f.Age == 5);
            var s1 = expression1.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression2 = f => (1 % f.Age == 5);
            var s2 = expression2.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression3 = f => (1 % f.B5.Age == 5);
            var s3 = expression3.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression4 = f => (f.Age % f.B5.Age == 5);
            var s4 = expression4.ToExpressionPredicate();
            Assert.AreEqual(s1.Predicate, "((1 % it.Age) == 5)");
            Assert.AreEqual(s2.Predicate, "((1 % it.Age) == 5)");
            Assert.AreEqual(s3.Predicate, "((1 % it.B5.Age) == 5)");
            Assert.AreEqual(s4.Predicate, "((it.Age % it.B5.Age) == 5)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void MultiplicativeModuloDecimal()
        {
            var value = 1.11M;
            Expression<Func<Model1, bool>> expression1 = f => (value % f.B1 == 5M);
            var s1 = expression1.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression2 = f => (1.11M % f.B1 == 5M);
            var s2 = expression2.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression3 = f => (1.11M % f.B5.B1 == 5M);
            var s3 = expression3.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression4 = f => (f.B1 % f.B5.B1 == 5M);
            var s4 = expression4.ToExpressionPredicate();
            Assert.AreEqual(s1.Predicate, "((1.11 % it.B1) == 5)");
            Assert.AreEqual(s2.Predicate, "((1.11 % it.B1) == 5)");
            Assert.AreEqual(s3.Predicate, "((1.11 % it.B5.B1) == 5)");
            Assert.AreEqual(s4.Predicate, "((it.B1 % it.B5.B1) == 5)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
    }
}

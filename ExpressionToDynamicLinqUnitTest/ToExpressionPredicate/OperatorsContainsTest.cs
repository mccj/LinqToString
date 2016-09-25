using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class OperatorsContainsTest
    {
        [TestMethod]
        public void OperatorsContainsTestString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Contains(f.Name));
            var s1 = expression1.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }.Contains(f.Name));
            var s2 = expression2.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }.Contains(f.B5.Name));
            var s3 = expression3.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression4 = f => (f.B8.Contains(f.B5.Name));
            var s4 = expression4.ToExpressionPredicate();

            Assert.AreEqual(s1.Predicate, "@0.Contains(outerIt.Name)");
            Assert.AreEqual(s2.Predicate, "@0.Contains(outerIt.Name)");
            Assert.AreEqual(s3.Predicate, "@0.Contains(outerIt.B5.Name)");
            Assert.AreEqual(s4.Predicate, "it.B8.Contains(outerIt.B5.Name)");

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B8 = new[] { "" } } };
            model.Where(s1).ToArray();
            model.Where(s2).ToArray();
            model.Where(s3).ToArray();
            model.Where(s4).ToArray();
        }
        [TestMethod]
        public void OperatorsContainsTestEnum()
        {
            var arrStr = new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Contains(f.State));
            var s1 = expression1.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Contains(f.State));
            var s2 = expression2.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Contains(f.B5.State));
            var s3 = expression3.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression4 = f => (f.B6.Contains(f.B5));
            var s4 = expression4.ToExpressionPredicate();
            Assert.AreEqual(s1.Predicate, "@0.Contains(outerIt.State)");
            Assert.AreEqual(s2.Predicate, "@0.Contains(outerIt.State)");
            Assert.AreEqual(s3.Predicate, "@0.Contains(outerIt.B5.State)");
            Assert.AreEqual(s4.Predicate, "it.B6.Contains(outerIt.B5)");

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B6 = new[] { new Model2 { } } } };
            model.Where(s1).ToArray();
            model.Where(s2).ToArray();
            model.Where(s3).ToArray();
            model.Where(s4).ToArray();
        }
    }
}

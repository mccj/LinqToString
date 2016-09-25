using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class OperatorsAnyTest
    {
        [TestMethod]
        public void OperatorsAnyTestString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Any(ff => f.Name == ff));
            var s1 = expression1.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }.Any(ff => f.Name == ff));
            var s2 = expression2.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }.Any(ff => f.B5.Name == ff));
            var s3 = expression3.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression4 = f => (f.B8.Any(ff => f.B5.Name == ff));
            var s4 = expression4.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression5 = f => (f.B8.Any());
            var s5 = expression5.ToExpressionPredicate();

            Assert.AreEqual(s1.Predicate, "@0.Any((outerIt.Name == it))");
            Assert.AreEqual(s2.Predicate, "@0.Any((outerIt.Name == it))");
            Assert.AreEqual(s3.Predicate, "@0.Any((outerIt.B5.Name == it))");
            Assert.AreEqual(s4.Predicate, "it.B8.Any((outerIt.B5.Name == it))");
            Assert.AreEqual(s5.Predicate, "it.B8.Any()");

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B8 = new[] { "" } } };
            model.Where(s1).ToArray();
            model.Where(s2).ToArray();
            model.Where(s3).ToArray();
            model.Where(s4).ToArray();
            model.Where(s5).ToArray();
        }
        [TestMethod]
        public void OperatorsAnyTestEnum()
        {
            var arrStr = new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Any(ff => f.State == ff));
            var s1 = expression1.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression2 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Any((ff => f.State == ff)));
            var s2 = expression2.ToExpressionPredicate();
            Expression<Func<Model1, bool>> expression3 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Any((ff => f.B5.State == ff)));
            var s3 = expression3.ToExpressionPredicate();

            Assert.AreEqual(s1.Predicate, "@0.Any((outerIt.State == it))");
            Assert.AreEqual(s2.Predicate, "@0.Any((outerIt.State == it))");
            Assert.AreEqual(s3.Predicate, "@0.Any((outerIt.B5.State == it))");

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B8 = new[] { "" } } };
            model.Where(s1).ToArray();
            model.Where(s2).ToArray();
            model.Where(s3).ToArray();
        }
    }
}

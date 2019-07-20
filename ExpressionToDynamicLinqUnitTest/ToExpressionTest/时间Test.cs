using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
{
    [TestClass]
    public class 时间Test : PredicateTestBase
    {
        [TestMethod]
        public void 时间DateTime()
        {
            var value = new System.DateTime(2012, 1, 1);
            var value2 = new System.TimeSpan(1, 0, 0);
            var value3 = -9;
            Expression<Func<Model1, bool>> expression1 = f => (value.Add(new System.TimeSpan(1, 0, 0)) > System.DateTime.Now.Add(value2));
            Expression<Func<Model1, bool>> expression2 = f => (value.AddDays(value3) > System.DateTime.Now.AddDays(-value3));

            var ss1 = Test(expression1);
            var ss2 = Test(expression2);
    
            //Assert.AreEqual(s1, "(DateTime(634609728000000000).Add(TimeSpan(1,0,0)) > DateTime(636104765495424190).Add(TimeSpan(36000000000)))");
            //Assert.AreEqual(s2, "(DateTime(634609728000000000).AddDays(-9) > DateTime(636104765676911376).AddDays(9))");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
        }
        [TestMethod]
        public void 时间DateTimeOffset()
        {
            var value = new System.DateTimeOffset(new System.DateTime(2012, 1, 1));
            var value2 = new System.TimeSpan(1, 0, 0);
            var value3 = -9;
            Expression<Func<Model1, bool>> expression1 = f => (value.Add(new System.TimeSpan(1, 0, 0)) > System.DateTimeOffset.Now.Add(value2));
            Expression<Func<Model1, bool>> expression2 = f => (value.AddDays(value3) > System.DateTimeOffset.Now.AddDays(-value3));

            var ss1 = Test(expression1);
            var ss2 = Test(expression2);

            //Assert.AreEqual(s1, "(DateTimeOffset(634609728000000000,TimeSpan(288000000000)).Add(TimeSpan(1,0,0)) > DateTimeOffset(636104766093049722,TimeSpan(288000000000)).Add(TimeSpan(36000000000)))");
            //Assert.AreEqual(s2, "(DateTimeOffset(634609728000000000,TimeSpan(288000000000)).AddDays(-9) > DateTimeOffset(636104766093069731,TimeSpan(288000000000)).AddDays(9))");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
        }
    }
}

using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class DynamicLinqTest
    {
        [TestMethod]
        public void Dynamic静态方法Test()
        {
            var dddd = new Model1[] { new Model1 { B2 = System.DateTime.Now } };
            var a1 = dddd.Where(f => f.B2 > Convert.ToDateTime("2012-01-01")).ToArray();
            var a2 = dddd.Where("it.B2>Convert.ToDateTime(\"2012 - 01 - 01\")").ToArray();
            Expression<Func<Model1, bool>> expression1 = f => (f.B2 > Convert.ToDateTime("2012-01-01"));
            var s1 = expression1.ToExpressionPredicate();
            var a3 = dddd.Select(s1);
            Assert.AreEqual(s1.Predicate, "(it.B2 > Convert.ToDateTime(\"2012-01-01\"))");
        }
        [TestMethod]
        public void DynamicNewTest()
        {
            var dddd = new Model1[] { new Model1 { B2 = System.DateTime.Now, Name = "eeee" } };
            var a1 = dddd.SelectMany(f => new string[] { "1111" }).ToArray();
            var a2 = dddd.Select("new(6 as Age,it.Name as tttt,8 as eeeee)");
            Expression<Func<Model1, object>> expression1 = f => (new { sss = "hhhhhh", Name = f.Name });
            var s1 = expression1.ToExpressionPredicate();
            var a3 = dddd.Select(s1);
            Assert.AreEqual(s1.Predicate, "new(\"hhhhhh\" as sss,it.Name as Name)");
        }
        [TestMethod]
        public void DynamicNewTest1()
        {
            //var s=  new string[0];
            var dddd = new Model1[] { new Model1 { B2 = System.DateTime.Now, Name = "eeee", B6 = new Model2[] { new Model2 { Age = 1 } } } };
            var a1 = dddd.Where(f => f.B6[0].Age == 1);
            var a2 = dddd.Where("(B6[0].Age == (1 + Age))");
            var a3 = dddd.Where("(B7[\"sss\"].Age == (1 + Age))");
            //Expression<Func<Model1, object>> expression1 = f => (new { sss = "hhhhhh", Name = f.Name });
            //var s1 = expression1.ToExpressionPredicate();
            //var a3 = dddd.Select(s1);
            //Assert.AreEqual(s1, "new(\"hhhhhh\" as sss,Name as Name)");
        }
        [TestMethod]
        public void DynamicNewTest2()
        {
            //var s=  new string[0];
            var dddd = new Model1[] { new Model1 { B2 = System.DateTime.Now, Name = "eeee", B6 = new Model2[] { new Model2 { Age = 1 } } } };
            var a1 = dddd.Where(f => f.B8.Contains("sssss"));
            var a2 = dddd.Where("(B8.Contains(\"sssss\"))");
            //Expression<Func<Model1, object>> expression1 = f => (new { sss = "hhhhhh", Name = f.Name });
            //var s1 = expression1.ToExpressionPredicate();
            //var a3 = dddd.Select(s1);
            //Assert.AreEqual(s1, "new(\"hhhhhh\" as sss,Name as Name)");
        }
        [TestMethod]
        public void DynamicNewTest3()
        {
            //var s=  new string[0];
            var dddd = new Model1[] { new Model1 { B2 = System.DateTime.Now, Name = "111", B6 = new Model2[] { new Model2 { Age = 1 } } } };
            //var a1 = dddd.Where(f => f.B8.Contains("sssss"));
            var a1 = dddd.Where("(\"gggg\"==(it.Name))");
            var a2 = dddd.Where("(@0.Contains(\"111\"))", new object[] { new[] { "111", "222", "333" } });
            var a3 = dddd.Where("(@0.Contains(outerIt.Name))", new object[] { new[] { "111", "222", "333" } });
            var a4 = dddd.Where("(B8.Contains(outerIt.Name))", new object[] { new[] { "111", "222", "333" } });

            //var sssss = Newtonsoft.Json.JsonConvert.SerializeObject(a4);


            //var a5 = dddd.Where(f => f.B6.Any(d=>d.Name==f.Name));
            //Expression<Func<Model1, object>> expression1 = f => (new { sss = "hhhhhh", Name = f.Name });
            //var s1 = expression1.ToExpressionPredicate();
            //var a3 = dddd.Select(s1);
            //Assert.AreEqual(s1, "new(\"hhhhhh\" as sss,Name as Name)");
        }
    }
}

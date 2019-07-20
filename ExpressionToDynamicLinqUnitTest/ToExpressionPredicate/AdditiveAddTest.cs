using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class AdditiveAddTest : PredicateTestBase
    {
        [TestMethod]
        public void AdditiveAddString()
        {
            var value = "a1";
            Expression<Func<Model1, bool>> expression1 = f => (value + f.Name == "");
            Expression<Func<Model1, bool>> expression2 = f => ("a1" + f.Name == "");
            Expression<Func<Model1, bool>> expression3 = f => ("a1" + f.B5.Name == "");
            Expression<Func<Model1, bool>> expression4 = f => (f.Name + f.B5.Name == "");

            var s1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var s2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var s3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var s4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "((\"a1\" + (it).Name) == \"\")");
            //Assert.AreEqual(s2.Predicate, "((\"a1\" + (it).Name) == \"\")");
            //Assert.AreEqual(s3.Predicate, "((\"a1\" + (it).B5.Name) == \"\")");
            //Assert.AreEqual(s4.Predicate, "(((it).Name + (it).B5.Name) == \"\")");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void AdditiveAddInt()
        {
            var value = 1;
            Expression<Func<Model1, bool>> expression1 = f => (value + f.Age == 5);
            Expression<Func<Model1, bool>> expression2 = f => (1 + f.Age == 5);
            Expression<Func<Model1, bool>> expression3 = f => (1 + f.B5.Age == 5);
            Expression<Func<Model1, bool>> expression4 = f => (f.Age + f.B5.Age == 5);

            var s1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var s2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var s3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var s4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "((1 + (it).Age) == 5)");
            //Assert.AreEqual(s2.Predicate, "((1 + (it).Age) == 5)");
            //Assert.AreEqual(s3.Predicate, "((1 + (it).B5.Age) == 5)");
            //Assert.AreEqual(s4.Predicate, "(((it).Age + (it).B5.Age) == 5)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void AdditiveAddDecimal()
        {
            var value = 1.11M;
            Expression<Func<Model1, bool>> expression1 = f => (value + f.B1 == 5M);
            Expression<Func<Model1, bool>> expression2 = f => (1.11M + f.B1 == 5M);
            Expression<Func<Model1, bool>> expression3 = f => (1.11M + f.B5.B1 == 5M);
            Expression<Func<Model1, bool>> expression4 = f => (f.B1 + f.B5.B1 == 5M);

            var s1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var s2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var s3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var s4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "((1.11 + (it).B1) == 5)");
            //Assert.AreEqual(s2.Predicate, "((1.11 + (it).B1) == 5)");
            //Assert.AreEqual(s3.Predicate, "((1.11 + (it).B5.B1) == 5)");
            //Assert.AreEqual(s4.Predicate, "(((it).B1 + (it).B5.B1) == 5)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }

        [TestMethod]
        public void AdditiveAddDateTime()
        {
            var value = new System.DateTime(2012, 1, 1);
            var value2 = new TimeSpan(1, 0, 0);

            Expression<Func<Model1, bool>> expression1 = f => (value + value2 <= f.B2);
            Expression<Func<Model1, bool>> expression2 = f => (new System.DateTime(2012, 1, 1) + new TimeSpan(1, 0, 0) <= f.B2);
            Expression<Func<Model1, bool>> expression2_1 = f => (System.DateTime.Now + new TimeSpan(1, 0, 0) <= f.B2);
            Expression<Func<Model1, bool>> expression3 = f => (new System.DateTime(2012, 1, 1) + new TimeSpan(1, 0, 0) <= f.B5.B2);
            Expression<Func<Model1, bool>> expression4 = f => (f.B2 + new TimeSpan(1, 0, 0) <= f.B5.B2);

            var s1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var s2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var s3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var s4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "((DateTime(634609728000000000) + TimeSpan(36000000000)) <= (it).B2)");
            //Assert.AreEqual(s2.Predicate, "((DateTime(2012,1,1) + TimeSpan(1,0,0)) <= (it).B2)");
            ////Assert.AreEqual(s2_1.Predicate, "(1.11 >= B2)");
            //Assert.AreEqual(s3.Predicate, "((DateTime(2012,1,1) + TimeSpan(1,0,0)) <= (it).B5.B2)");
            //Assert.AreEqual(s4.Predicate, "(((it).B2 + TimeSpan(1,0,0)) <= (it).B5.B2)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        //[TestMethod]
        //public void AdditiveAddDateTimeOffset()
        //{
        //    var value = new System.DateTimeOffset(new System.DateTime(2012, 1, 1));
        //    var value2 = new TimeSpan(1, 0, 0);
        //    Expression<Func<Model1, bool>> expression1 = f => (value + value2 <= f.B3);
        //    var s1 = expression1.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression2 = f => (new System.DateTimeOffset(new System.DateTime(2012, 1, 1)) + new TimeSpan(1, 0, 0) <= f.B3);
        //    var s2 = expression2.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression2_1 = f => (System.DateTimeOffset.Now + new TimeSpan(1, 0, 0) <= f.B3);
        //    var s2_1 = expression2_1.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression3 = f => (new System.DateTimeOffset(new System.DateTime(2012, 1, 1)) + new TimeSpan(1, 0, 0) <= f.B5.B3);
        //    var s3 = expression3.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression4 = f => (f.B2 + new TimeSpan(1, 0, 0) <= f.B5.B2);
        //    var s4 = expression4.ToExpressionPredicate();
        //    Assert.AreEqual(s1, "(DateTimeOffset(DateTime(634609728000000000)) <= B3)");
        //    Assert.AreEqual(s2, "(DateTimeOffset(DateTime(2012,1,1)) <= B3)");
        //    //Assert.AreEqual(s2_1, "(1.11 >= B2)");
        //    Assert.AreEqual(s3, "(DateTimeOffset(DateTime(2012,1,1)) <= B5.B3)");
        //    Assert.AreEqual(s4, "(B2 <= B5.B2)");

        //    var models = new Model1[] { };
        //    var m1 = models.Where(s1).ToArray();
        //    var m2 = models.Where(s2).ToArray();
        //    var m3 = models.Where(s3).ToArray();
        //    var m4 = models.Where(s4).ToArray();
        //}

    }
}

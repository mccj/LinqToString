using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class RelationalGreaterThanOrEqualTest : PredicateTestBase
    {
        //[TestMethod]
        //public void GreaterThanOrEqualString()
        //{
        //    var value = "a1";
        //    Expression<Func<Model1, bool>> expression1 = f => (value >= f.Name);
        //    var s1 = expression1.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression2 = f => ("a1" >= f.Name);
        //    var s2 = expression2.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression3 = f => ("a1" >= f.B5.Name);
        //    var s3 = expression3.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression4 = f => (f.Name >= f.B5.Name);
        //    var s4 = expression4.ToExpressionPredicate();
        //    Assert.AreEqual(s1, "(\"a1\" >= Name)");
        //    Assert.AreEqual(s2, "(\"a1\" >= Name)");
        //    Assert.AreEqual(s3, "(\"a1\" >= B5.Name)");
        //    Assert.AreEqual(s4, "(Name >= B5.Name)");

        //    var models = new Model1[] { };
        //    var m1 = models.Where(s1).ToArray();
        //    var m2 = models.Where(s2).ToArray();
        //    var m3 = models.Where(s3).ToArray();
        //    var m4 = models.Where(s4).ToArray();
        //}
        [TestMethod]
        public void GreaterThanOrEqualEnum()
        {
            var value = StateEnum.State2;
            Expression<Func<Model1, bool>> expression1 = f => (value >= f.State);
            Expression<Func<Model1, bool>> expression2 = f => ( f.State >= StateEnum.State2);
            Expression<Func<Model1, bool>> expression3 = f => (StateEnum.State2 >= f.B5.State);
            Expression<Func<Model1, bool>> expression4 = f => (f.State >= f.B5.State);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(1 >= (it).State)");
            //Assert.AreEqual(s2.Predicate, "(1 < (it).State)");
            //Assert.AreEqual(s3.Predicate, "(1 >= (it).B5.State)");
            //Assert.AreEqual(s4.Predicate, "((it).State >= (it).B5.State)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        //[TestMethod]
        //public void GreaterThanOrEqualBool()
        //{
        //    var value = true;
        //    Expression<Func<Model1, bool>> expression1 = f => (value >= f.B4);
        //    var s1 = expression1.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression2 = f => (true >= f.B4);
        //    var s2 = expression2.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression3 = f => (true >= f.B5.B4);
        //    var s3 = expression3.ToExpressionPredicate();
        //    Expression<Func<Model1, bool>> expression4 = f => (f.B4 >= f.B5.B4);
        //    var s4 = expression4.ToExpressionPredicate();
        //    Assert.AreEqual(s1, "(True >= B4)");
        //    Assert.AreEqual(s2, "(True >= B4)");
        //    Assert.AreEqual(s3, "(True >= B5.B4)");
        //    Assert.AreEqual(s4, "(B4 >= B5.B4)");

        //    var models = new Model1[] { };
        //    var m1 = models.Where(s1).ToArray();
        //    var m2 = models.Where(s2).ToArray();
        //    var m3 = models.Where(s3).ToArray();
        //    var m4 = models.Where(s4).ToArray();
        //}
        [TestMethod]
        public void GreaterThanOrEqualInt()
        {
            var value = 1;
            Expression<Func<Model1, bool>> expression1 = f => (value >= f.Age);
            Expression<Func<Model1, bool>> expression2 = f => (1 >= f.Age);
            Expression<Func<Model1, bool>> expression3 = f => (1 >= f.B5.Age);
            Expression<Func<Model1, bool>> expression4 = f => (f.Age >= f.B5.Age);


            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(1 >= (it).Age)");
            //Assert.AreEqual(s2.Predicate, "(1 >= (it).Age)");
            //Assert.AreEqual(s3.Predicate, "(1 >= (it).B5.Age)");
            //Assert.AreEqual(s4.Predicate, "((it).Age >= (it).B5.Age)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void GreaterThanOrEqualDecimal()
        {
            var value = 1.11M;
            Expression<Func<Model1, bool>> expression1 = f => (value >= f.B1);
            Expression<Func<Model1, bool>> expression2 = f => (1.11M >= f.B1);
            Expression<Func<Model1, bool>> expression3 = f => (1.11M >= f.B5.B1);
            Expression<Func<Model1, bool>> expression4 = f => (f.B1 >= f.B5.B1);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");
     

            //Assert.AreEqual(s1.Predicate, "(1.11 >= (it).B1)");
            //Assert.AreEqual(s2.Predicate, "(1.11 >= (it).B1)");
            //Assert.AreEqual(s3.Predicate, "(1.11 >= (it).B5.B1)");
            //Assert.AreEqual(s4.Predicate, "((it).B1 >= (it).B5.B1)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void GreaterThanOrEqualDateTime()
        {
            var value = new System.DateTime(2012, 1, 1);
            Expression<Func<Model1, bool>> expression1 = f => (value >= f.B2);
            Expression<Func<Model1, bool>> expression2 = f => (new System.DateTime(2012, 1, 1) >= f.B2);
            Expression<Func<Model1, bool>> expression2_1 = f => (System.DateTime.Now >= f.B2);
            Expression<Func<Model1, bool>> expression3 = f => (new System.DateTime(2012, 1, 1) >= f.B5.B2);
            Expression<Func<Model1, bool>> expression4 = f => (f.B2 >= f.B5.B2);


            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss2_1 = Test(expression2_1, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(DateTime(634609728000000000) >= (it).B2)");
            //Assert.AreEqual(s2.Predicate, "(DateTime(2012,1,1) >= (it).B2)");
            ////Assert.AreEqual(s2_1.Predicate, "(1.11 >= B2)");
            //Assert.AreEqual(s3.Predicate, "(DateTime(2012,1,1) >= (it).B5.B2)");
            //Assert.AreEqual(s4.Predicate, "((it).B2 >= (it).B5.B2)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void GreaterThanOrEqualDateTimeOffset()
        {
            var value = new System.DateTimeOffset(new System.DateTime(2012, 1, 1));
            Expression<Func<Model1, bool>> expression1 = f => (value >= f.B3);
            Expression<Func<Model1, bool>> expression2 = f => (new System.DateTimeOffset(new System.DateTime(2012, 1, 1)) >= f.B3);
            Expression<Func<Model1, bool>> expression2_1 = f => (System.DateTimeOffset.Now >= f.B3);
            Expression<Func<Model1, bool>> expression3 = f => (new System.DateTimeOffset(new System.DateTime(2012, 1, 1)) >= f.B5.B3);
            Expression<Func<Model1, bool>> expression4 = f => (f.B2 >= f.B5.B2);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss2_1 = Test(expression2_1, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");


            //Assert.AreEqual(s1.Predicate, "(DateTimeOffset(634609728000000000,TimeSpan(288000000000)) >= (it).B3)");
            //Assert.AreEqual(s2.Predicate, "(DateTimeOffset(DateTime(2012,1,1)) >= (it).B3)");
            ////Assert.AreEqual(s2_1.Predicate, "(1.11 >= B2)");
            //Assert.AreEqual(s3.Predicate, "(DateTimeOffset(DateTime(2012,1,1)) >= (it).B5.B3)");
            //Assert.AreEqual(s4.Predicate, "((it).B2 >= (it).B5.B2)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
    }
}

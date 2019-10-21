using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
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

            var ss1 = Test(expression1);
            var ss2 = Test(expression2);
            var ss3 = Test(expression3);
            var ss4 = Test(expression4);

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


            var ss1 = Test(expression1);
            var ss2 = Test(expression2);
            var ss3 = Test(expression3);
            var ss4 = Test(expression4);

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

            var ss1 = Test(expression1);
            var ss2 = Test(expression2);
            var ss3 = Test(expression3);
            var ss4 = Test(expression4);
     

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


            var ss1 = Test(expression1);
            var ss2 = Test(expression2);
            var ss2_1 = Test(expression2_1);
            var ss3 = Test(expression3);
            var ss4 = Test(expression4);

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

            var ss1 = Test(expression1);
            var ss2 = Test(expression2);
            var ss2_1 = Test(expression2_1);
            var ss3 = Test(expression3);
            var ss4 = Test(expression4);


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

        [TestMethod]
        public void GreaterThanOrEqualDateTimeAndDateTimeOffset()
        {
            var value1 = new System.DateTimeOffset(new System.DateTime(2012, 1, 1));
            var value2 = new System.DateTime(2012, 1, 1);
            Expression<Func<Model1, bool>> expression1 = f => (value1 <= f.B2);
            Expression<Func<Model1, bool>> expression2 = f => (value2 <= f.B3);
            Expression<Func<Model1, bool>> expression3 = f => (new System.DateTimeOffset(new System.DateTime(2012, 1, 1)) <= f.B2);
            Expression<Func<Model1, bool>> expression4 = f => (new System.DateTime(2012, 1, 1) <= f.B3);
            Expression<Func<Model1, bool>> expression5 = f => (System.DateTimeOffset.Now <= f.B2);
            Expression<Func<Model1, bool>> expression6 = f => (System.DateTime.Now <= f.B3);
            Expression<Func<Model1, bool>> expression7 = f => (new System.DateTimeOffset(new System.DateTime(2012, 1, 1)) <= f.B5.B2);
            Expression<Func<Model1, bool>> expression8 = f => (new System.DateTime(2012, 1, 1) <= f.B5.B3);
            Expression<Func<Model1, bool>> expression9 = f => (f.B2 <= f.B5.B3);
            Expression<Func<Model1, bool>> expression10 = f => (f.B3 <= f.B5.B2);

            var ss1 = Test(expression1);
            var ss2 = Test(expression2);
            var ss3 = Test(expression3);
            var ss4 = Test(expression4);
            var ss5 = Test(expression5);
            var ss6 = Test(expression6);
            var ss7 = Test(expression7);
            var ss8 = Test(expression8);
            //var ss9 = Test(expression9);
            //var ss10 = Test(expression10);


            //Assert.AreEqual(s1, "(DateTime(634609728000000000) <= B2)");
            //Assert.AreEqual(s2, "(DateTimeOffset(634609728000000000,TimeSpan(288000000000)) <= B3)");
            //Assert.AreEqual(s3, "(DateTimeOffset(DateTime(2012,1,1)) <= B2)");
            //Assert.AreEqual(s4, "(DateTimeOffset(DateTime(2012,1,1)) <= B3)");
            //Assert.AreEqual(s5, "(DateTime(2012,1,1) <= B2)");
            //Assert.AreEqual(s6, "(DateTimeOffset(DateTime(2012,1,1)) <= B3)");
            //Assert.AreEqual(s7, "(DateTime(2012,1,1) <= B5.B2)");
            //Assert.AreEqual(s8, "(DateTimeOffset(DateTime(2012,1,1)) <= B5.B3)");
            ////Assert.AreEqual(s9, "(B2 <= B5.B2)");
            ////Assert.AreEqual(s10, "(B2 <= B5.B2)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
            //var m5 = models.Where(s5).ToArray();
            //var m6 = models.Where(s6).ToArray();
            //var m7 = models.Where(s7).ToArray();
            //var m8 = models.Where(s8).ToArray();
            ////var m9 = models.Where(s9).ToArray();
            ////var m10 = models.Where(s10).ToArray();
        }
    }
}

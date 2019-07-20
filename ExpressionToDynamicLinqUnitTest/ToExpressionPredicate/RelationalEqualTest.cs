using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class RelationalEqualTest : PredicateTestBase
    {
        [TestMethod]
        public void EqualString()
        {
            var value = "a1";
            Expression<Func<Model1, bool>> expression1 = f => (value == f.Name);
            Expression<Func<Model1, bool>> expression2 = f => ("a1" == f.Name);
            Expression<Func<Model1, bool>> expression3 = f => ("a1" == f.B5.Name);
            Expression<Func<Model1, bool>> expression4 = f => (f.Name == f.B5.Name);
            Expression<Func<Model1, bool>> expression5 = f => (null== f.B5.Name);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");
            var ss5 = Test(expression5, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(\"a1\" == (it).Name)");
            //Assert.AreEqual(s2.Predicate, "(\"a1\" == (it).Name)");
            //Assert.AreEqual(s3.Predicate, "(\"a1\" == (it).B5.Name)");
            //Assert.AreEqual(s4.Predicate, "((it).Name == (it).B5.Name)");
            //Assert.AreEqual(s5.Predicate, "(null == (it).B5.Name)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void EqualEnum()
        {
            var value = StateEnum.State2;
            Expression<Func<Model1, bool>> expression1 = f => (value == f.State);
            Expression<Func<Model1, bool>> expression2 = f => (f.State== StateEnum.State2);
            Expression<Func<Model1, bool>> expression3 = f => (StateEnum.State2 == f.B5.State);
            Expression<Func<Model1, bool>> expression4 = f => (f.State == f.B5.State);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(\"State2\" == (it).State)");
            //Assert.AreEqual(s2.Predicate, "((it).State == \"State2\")");
            //Assert.AreEqual(s3.Predicate, "(\"State2\" == (it).B5.State)");
            //Assert.AreEqual(s4.Predicate, "((it).State == (it).B5.State)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void EqualBool()
        {
            var value = true;
            Expression<Func<Model1, bool>> expression1 = f => (value == f.B4);
            Expression<Func<Model1, bool>> expression2 = f => (true == f.B4);
            Expression<Func<Model1, bool>> expression3 = f => (true == f.B5.B4);
            Expression<Func<Model1, bool>> expression4 = f => (f.B4 == f.B5.B4);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(True == (it).B4)");
            //Assert.AreEqual(s2.Predicate, "(True == (it).B4)");
            //Assert.AreEqual(s3.Predicate, "(True == (it).B5.B4)");
            //Assert.AreEqual(s4.Predicate, "((it).B4 == (it).B5.B4)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void EqualInt()
        {
            var value = 1;
            Expression<Func<Model1, bool>> expression1 = f => (value == f.Age);
            Expression<Func<Model1, bool>> expression2 = f => (1 == f.Age);
            Expression<Func<Model1, bool>> expression3 = f => (1 == f.B5.Age);
            Expression<Func<Model1, bool>> expression4 = f => (f.Age == f.B5.Age);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");
         

            //Assert.AreEqual(s1.Predicate, "(1 == (it).Age)");
            //Assert.AreEqual(s2.Predicate, "(1 == (it).Age)");
            //Assert.AreEqual(s3.Predicate, "(1 == (it).B5.Age)");
            //Assert.AreEqual(s4.Predicate, "((it).Age == (it).B5.Age)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void EqualDecimal()
        {
            var value = 1.11M;
            Expression<Func<Model1, bool>> expression1 = f => (value == f.B1);
            Expression<Func<Model1, bool>> expression2 = f => (1.11M == f.B1);
            Expression<Func<Model1, bool>> expression3 = f => (1.11M == f.B5.B1);
            Expression<Func<Model1, bool>> expression4 = f => (f.B1 == f.B5.B1);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(1.11 == (it).B1)");
            //Assert.AreEqual(s2.Predicate, "(1.11 == (it).B1)");
            //Assert.AreEqual(s3.Predicate, "(1.11 == (it).B5.B1)");
            //Assert.AreEqual(s4.Predicate, "((it).B1 == (it).B5.B1)");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();
            //var m3 = models.Where(s3).ToArray();
            //var m4 = models.Where(s4).ToArray();
        }
    }
}

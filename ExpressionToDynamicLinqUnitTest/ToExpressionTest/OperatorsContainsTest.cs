using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
{
    [TestClass]
    public class OperatorsContainsTest : PredicateTestBase
    {
        [TestMethod]
        public void OperatorsContainsString()
        {
            var arr = "a1a2a3";
            Expression<Func<Model1, bool>> expression1 = f => (arr.Contains(f.Name));

            Expression<Func<Model1, bool>> expression2 = f => ("a1a2a3".Contains(f.Name));
            Expression<Func<Model1, bool>> expression3 = f => ("a1a2a3".Contains(f.B5.Name));
            Expression<Func<Model1, bool>> expression4 = f => ("a1a2a3".Contains(f.B5.Name));
            Expression<Func<Model1, bool>> expression5 = f => (f.B6.Any(ff => ff.Name == f.B5.Name));

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");
            var ss5 = Test(expression5, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "\"a1a2a3\".Contains((it).Name)");
            //Assert.AreEqual(s2.Predicate, "\"a1a2a3\".Contains((it).Name)");
            //Assert.AreEqual(s3.Predicate, "\"a1a2a3\".Contains((it).B5.Name)");
            //Assert.AreEqual(s4.Predicate, "\"a1a2a3\".Contains((it).B5.Name)");

            //var model = new Model1[] { new Model1 { B5 = new Model2 { Name="rrr" }, Name="111"} };
            //model.Where(s1).ToArray();
            //model.Where(s2).ToArray();
            //model.Where(s3).ToArray();
            //model.Where(s4).ToArray();
        }

        [TestMethod]
        public void OperatorsContainsArrayString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Contains(f.Name));

            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }.Contains(f.Name));
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }.Contains(f.B5.Name));
            Expression<Func<Model1, bool>> expression4 = f => (f.B8.Contains(f.B5.Name));

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "@0.Contains((outerIt).Name)");
            //Assert.AreEqual(s2.Predicate, "@0.Contains((outerIt).Name)");
            //Assert.AreEqual(s3.Predicate, "@0.Contains((outerIt).B5.Name)");
            //Assert.AreEqual(s4.Predicate, "(it).B8.Contains((outerIt).B5.Name)");

            //s1.Predicate = "@0.Contains(root.Name)";
            //s2.Predicate = "@0.Contains(root.Name)";
            //s3.Predicate = "@0.Contains(root.B5.Name)";
            //s4.Predicate = "it.B8.Contains(root.B5.Name)";

            //var model = new Model1[] { new Model1 { B5 = new Model2 { }, B8 = new[] { "" } } };
            //model.Where(s1).ToArray();
            //model.Where(s2).ToArray();
            //model.Where(s3).ToArray();
            //model.Where(s4).ToArray();
        }
        [TestMethod]
        public void OperatorsContainsArrayEnum()
        {
            var arrStr = new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Contains(f.State));

            Expression<Func<Model1, bool>> expression2 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Contains(f.State));
            Expression<Func<Model1, bool>> expression3 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Contains(f.B5.State));
            Expression<Func<Model1, bool>> expression4 = f => (f.B6.Contains(f.B5));

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "@0.Contains((outerIt).State)");
            //Assert.AreEqual(s2.Predicate, "@0.Contains((outerIt).State)");
            //Assert.AreEqual(s3.Predicate, "@0.Contains((outerIt).B5.State)");
            //Assert.AreEqual(s4.Predicate, "(it).B6.Contains((outerIt).B5)");


            //s1.Predicate = "@0.Contains(root.State)";
            //s2.Predicate = "@0.Contains(root.State)";
            //s3.Predicate = "@0.Contains(root.B5.State)";
            //s4.Predicate = "it.B6.Contains(root.B5)";

            //var model = new Model1[] { new Model1 { B5 = new Model2 { }, B6 = new[] { new Model2 { } } } };
            //model.Where(s1).ToArray();
            //model.Where(s2).ToArray();
            //model.Where(s3).ToArray();
            //model.Where(s4).ToArray();
        }
    }
}

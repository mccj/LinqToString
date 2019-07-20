using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class OperatorsAnyTest : PredicateTestBase
    {
        [TestMethod]
        public void OperatorsAnyString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Any(ff => f.Name == ff));
            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }.Any(ff => f.Name == ff));
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }.Any(ff => f.B5.Name == ff));
            Expression<Func<Model1, bool>> expression4 = f => (f.B8.Any(ff => f.B5.Name == ff));
            Expression<Func<Model1, bool>> expression5 = f => (f.B8.Any());
            Expression<Func<Model1, bool>> expression6 = f => (f.B6.Any(ff => f.B5.Name == ff.Name));

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B6 = new Model2[] { }, B8 = new[] { "" } } };

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")",model);
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")", model);
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")", model);
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")", model);
            var ss5 = Test(expression5, "((\"a1\" + (it).Name) == \"\")", model);
            var ss6 = Test(expression6, "((\"a1\" + (it).Name) == \"\")", model);


            //Assert.AreEqual(s1.Predicate, "@0.Any(((outerIt).Name == (it)))");
            //Assert.AreEqual(s2.Predicate, "@0.Any(((outerIt).Name == (it)))");
            //Assert.AreEqual(s3.Predicate, "@0.Any(((outerIt).B5.Name == (it)))");
            //Assert.AreEqual(s4.Predicate, "(it).B8.Any(((outerIt).B5.Name == (it)))");
            //Assert.AreEqual(s5.Predicate, "(it).B8.Any()");
            //Assert.AreEqual(s6.Predicate, "(it).B6.Any(((outerIt).B5.Name == (it).Name))");

            //s1.Predicate = "@0.Any((outerIt => root.Name == outerIt))";
            //s2.Predicate = "@0.Any((outerIt => root.Name == outerIt))";
            //s3.Predicate = "@0.Any((outerIt => root.B5.Name == outerIt))";
            //s4.Predicate = "root.B8.Any((outerIt => root.B5.Name == outerIt))";
            //s5.Predicate = "root.B8.Any()";
            //s6.Predicate = "root.B6.Any(( root.B5.Name == parent.Name))";


            //model.Where(s1).ToArray();
            //model.Where(s2).ToArray();
            //model.Where(s3).ToArray();
            //model.Where(s4).ToArray();
            //model.Where(s5).ToArray();
            //model.Where(s6).ToArray();
        }

        [TestMethod]
        public void OperatorsAnyTestInt()
        {
            var arrStr = new[] { 1, 2, 3 };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Any(ff => f.Age == ff));
            Expression<Func<Model1, bool>> expression2 = f => (new[] { 1, 2, 3 }.Any(ff => f.Age == ff));
            Expression<Func<Model1, bool>> expression3 = f => (new[] { 1, 2, 3 }.Any(ff => f.B5.Age == ff));
            Expression<Func<Model1, bool>> expression4 = f => (f.B9.Any(ff => f.B5.Age == ff));
            Expression<Func<Model1, bool>> expression5 = f => (f.B9.Any());

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B9 = new[] { 0 } } };
            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")", model);
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")", model);
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")", model);
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")", model);
            var ss5 = Test(expression5, "((\"a1\" + (it).Name) == \"\")", model);

            //Assert.AreEqual(s1.Predicate, "@0.Any((outerIt.Name == it))");
            //Assert.AreEqual(s2.Predicate, "@0.Any((outerIt.Name == it))");
            //Assert.AreEqual(s3.Predicate, "@0.Any((outerIt.B5.Name == it))");
            //Assert.AreEqual(s4.Predicate, "it.B8.Any((outerIt.B5.Name == it))");
            //Assert.AreEqual(s5.Predicate, "it.B8.Any()");

            //s1.Predicate = "@0.Any((outerIt => root.Age == outerIt))";
            //s2.Predicate = "@0.Any((outerIt => root.Age == outerIt))";
            //s3.Predicate = "@0.Any((outerIt => root.B5.Age == outerIt))";
            //s4.Predicate = "root.B9.Any((outerIt => root.B5.Age == outerIt))";
            //s5.Predicate = "root.B9.Any()";

            //var model = new Model1[] { new Model1 { B5 = new Model2 { }, B9 = new[] { 0 } } };
            //model.Where(s1).ToArray();
            //model.Where(s2).ToArray();
            //model.Where(s3).ToArray();
            //model.Where(s4).ToArray();
            //model.Where(s5).ToArray();
        }

        [TestMethod]
        public void OperatorsAnyEnum()
        {
            var arrStr = new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr.Any(ff => f.State == ff));
            Expression<Func<Model1, bool>> expression2 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Any((ff => f.State == ff)));
            Expression<Func<Model1, bool>> expression3 = f => (new[] { StateEnum.State1, StateEnum.State2, StateEnum.State3 }.Any((ff => f.B5.State == ff)));

            var model = new Model1[] { new Model1 { B5 = new Model2 { }, B8 = new[] { "" } } };
            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")", model);
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")", model);
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")", model);


            //Assert.AreEqual(s1.Predicate, "@0.Any(((outerIt).State == (it)))");
            //Assert.AreEqual(s2.Predicate, "@0.Any(((outerIt).State == (it)))");
            //Assert.AreEqual(s3.Predicate, "@0.Any(((outerIt).B5.State == (it)))");

            //s1.Predicate = "@0.Any(root.State == it)";
            //s2.Predicate = "@0.Any(root.State == it)";
            //s3.Predicate = "@0.Any(root.B5.State == it)";

            //var model = new Model1[] { new Model1 { B5 = new Model2 { }, B8 = new[] { "" } } };
            //model.Where(s1).ToArray();
            //model.Where(s2).ToArray();
            //model.Where(s3).ToArray();
        }
    }
}

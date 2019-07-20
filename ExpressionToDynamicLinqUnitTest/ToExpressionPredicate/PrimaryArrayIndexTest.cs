using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionPredicate
{
    [TestClass]
    public class PrimaryArrayIndexTest : PredicateTestBase
    {
        [TestMethod]
        public void PrimaryArrayIndexTestString()
        {
            var arrStr = new[] { "a1", "a2", "a3" };
            Expression<Func<Model1, bool>> expression1 = f => (arrStr[0] == (f.Name));

            Expression<Func<Model1, bool>> expression2 = f => (new[] { "a1", "a2", "a3" }[0] == (f.Name));
            Expression<Func<Model1, bool>> expression3 = f => (new[] { "a1", "a2", "a3" }[0] == (f.B5.Name));
            Expression<Func<Model1, bool>> expression4 = f => (f.B6[0].Age == 1 + f.Age);
            Expression<Func<Model1, bool>> expression5 = f => (f.B6[0].B4);
            Expression<Func<Model1, bool>> expression6 = f => (f.B7["ssss"].B4);
            Expression<Func<Model1, bool>> expression7 = f => (f.B6[f.Age].B4);
            Expression<Func<Model1, bool>> expression8 = f => (f.B7[f.Name].B4);

            var ss1 = Test(expression1, "((\"a1\" + (it).Name) == \"\")");
            var ss2 = Test(expression2, "((\"a1\" + (it).Name) == \"\")");
            var ss3 = Test(expression3, "((\"a1\" + (it).Name) == \"\")");
            var ss4 = Test(expression4, "((\"a1\" + (it).Name) == \"\")");
            var ss5 = Test(expression5, "((\"a1\" + (it).Name) == \"\")");
            var ss6 = Test(expression6, "((\"a1\" + (it).Name) == \"\")");
            var ss7 = Test(expression7, "((\"a1\" + (it).Name) == \"\")");
            var ss8 = Test(expression8, "((\"a1\" + (it).Name) == \"\")");

            //Assert.AreEqual(s1.Predicate, "(\"a1\" == (it).Name)");
            //Assert.AreEqual(s2.Predicate, "(\"a1\" == (it).Name)");
            //Assert.AreEqual(s3.Predicate, "(\"a1\" == (it).B5.Name)");
            //Assert.AreEqual(s4.Predicate, "((it).B6[0].Age == (1 + (it).Age))");
            //Assert.AreEqual(s5.Predicate, "(it).B6[0].B4");
            //Assert.AreEqual(s6.Predicate, "(it).B7[\"ssss\"].B4");
            //Assert.AreEqual(s7.Predicate, "(it).B6[(it).Age].B4");
            //Assert.AreEqual(s8.Predicate, "(it).B7[(it).Name].B4");

            //var model = new Model1[] { new Model1 {Name="ssss", B5 = new Model2 { }, B6 = new Model2[] { new Model2 { } }, B7 = new System.Collections.Generic.Dictionary<string, Model2>() { { "ssss", new Model2 { } } } } };
            //model.Where(s1).ToArray();
            //model.Where(s2).ToArray();
            //model.Where(s3).ToArray();
            //model.Where(s4).ToArray();
            //model.Where(s5).ToArray();
            //model.Where(s6).ToArray();
            //model.Where(s7).ToArray();
            //model.Where(s8).ToArray();
        }
    }
}

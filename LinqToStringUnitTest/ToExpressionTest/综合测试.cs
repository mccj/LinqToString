﻿using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ToExpressionTest
{
    [TestClass]
    public class 综合Test : PredicateTestBase
    {
        [TestMethod]
        public void 综合测试()
        {
            var value = "a1";
            var valueTrue = true;
            var valueFalse = false;

            Expression<Func<Model1, bool>> expression1 = f => ((value + f.Name == "") && value == "ssss" && f.Name == "ssss");
            Expression<Func<Model1, bool>> expression2 = f => (valueTrue && (((valueFalse || valueFalse))));

            var ss1 = Test(expression1);
            var ss2 = Test(expression2);

            //Assert.AreEqual(s1.Predicate, "((((\"a1\" + (it).Name) == \"\") && (\"a1\" == \"ssss\")) && ((it).Name == \"ssss\"))");
            //Assert.AreEqual(s2.Predicate, "(True && (False || False))");

            //var models = new Model1[] { };
            //var m1 = models.Where(s1).ToArray();
            //var m2 = models.Where(s2).ToArray();

        }
    }
}

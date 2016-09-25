﻿using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    [TestClass]
    public class MultiplicativeMultiplyTest
    {
        [TestMethod]
        public void MultiplicativeMultiplyInt()
        {
            var value = 1;
            Expression<Func<Model1, bool>> expression1 = f => (value * f.Age==5);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (1 * f.Age == 5);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (1 * f.B5.Age == 5);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.Age * f.B5.Age == 5);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "((1 * Age) == 5)");
            Assert.AreEqual(s2, "((1 * Age) == 5)");
            Assert.AreEqual(s3, "((1 * B5.Age) == 5)");
            Assert.AreEqual(s4, "((Age * B5.Age) == 5)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
        [TestMethod]
        public void MultiplicativeMultiplyDecimal()
        {
            var value = 1.11M;
            Expression<Func<Model1, bool>> expression1 = f => (value * f.B1 == 5M);
            var s1 = expression1.ToExpressionString();
            Expression<Func<Model1, bool>> expression2 = f => (1.11M * f.B1 == 5M);
            var s2 = expression2.ToExpressionString();
            Expression<Func<Model1, bool>> expression3 = f => (1.11M * f.B5.B1 == 5M);
            var s3 = expression3.ToExpressionString();
            Expression<Func<Model1, bool>> expression4 = f => (f.B1 * f.B5.B1 == 5M);
            var s4 = expression4.ToExpressionString();
            Assert.AreEqual(s1, "((1.11 * B1) == 5)");
            Assert.AreEqual(s2, "((1.11 * B1) == 5)");
            Assert.AreEqual(s3, "((1.11 * B5.B1) == 5)");
            Assert.AreEqual(s4, "((B1 * B5.B1) == 5)");

            var models = new Model1[] { };
            var m1 = models.Where(s1).ToArray();
            var m2 = models.Where(s2).ToArray();
            var m3 = models.Where(s3).ToArray();
            var m4 = models.Where(s4).ToArray();
        }
    }
}
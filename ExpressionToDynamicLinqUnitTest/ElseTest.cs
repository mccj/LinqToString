//using System;
//using System.Linq;
//using System.Linq.Dynamic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Linq.Expressions;

//namespace ExpressionToDynamicLinqUnitTest.ExpressionString
//{
//    [TestClass]
//    public class ElseTest
//    {
//        [TestMethod]
//        public void TestMethod1()
//        {
//            try
//            {
//                var sss = new System.Linq.Dynamic.Core.CustomTypeProviders.DefaultDynamicLinqCustomTypeProvider();
//                var sddd = sss.GetCustomTypes();


//                var assembly = typeof(System.Linq.Dynamic.Core.Parser.ExpressionParser).Assembly;
//                var type = assembly.GetType("System.Linq.Dynamic.Core.Parser.KeywordsHelper");
//                //var type = Type.GetType("System.Linq.Dynamic.Core.Parser.KeywordsHelper,System.Linq.Dynamic.Core");
//                var config = System.Linq.Dynamic.Core.ParsingConfig.Default;
//                var keywordsHelper = System.Activator.CreateInstance(type, config);
//                var _keywordsField = type.GetField("_keywords", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//                var _keywords = _keywordsField.GetValue(keywordsHelper);


//            }
//            catch (Exception ex)
//            {

//                throw;
//            }
//        }
//    }
//}

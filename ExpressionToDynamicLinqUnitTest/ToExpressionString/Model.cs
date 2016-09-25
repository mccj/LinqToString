using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionToDynamicLinqUnitTest.ExpressionString
{
    public enum StateEnum { State1, State2, State3 }
    public abstract class ModelBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal B1 { get; set; }
        public System.DateTime B2 { get; set; }
        public System.DateTimeOffset B3 { get; set; }
        public bool B4 { get; set; }
        public StateEnum State { get; set; }
    }
    public class Model1 : ModelBase
    {
        public Model2 B5 { get; set; }
        public Model2[] B6 { get; set; }
        public System.Collections.Generic.Dictionary<string, Model2> B7 { get; set; }
        public string[] B8 { get; set; }

    }
    public class Model2 : ModelBase
    {

    }
}

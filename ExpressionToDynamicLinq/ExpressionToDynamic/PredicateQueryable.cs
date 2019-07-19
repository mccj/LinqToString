namespace System.Linq.Dynamic
{
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("\\{Predicate:{Predicate}, Parameters:{Parameters}\\}")]
    public class PredicateQueryable
    {
        public string Predicate { get; set; }
        public Parameter[] Parameters { get; set; }
    }
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("\\{Index:{Index}, Type:{Type}, Value:{Value}\\}")]
    public class Parameter
    {
        public int Index { get; set; }
        public Type Type { get; set; }
        public dynamic Value { get; set; }

    }
}
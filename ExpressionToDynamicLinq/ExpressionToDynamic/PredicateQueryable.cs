namespace System.Linq.Dynamic
{
    [Serializable]
    public class PredicateQueryable
    {
        public string Predicate { get; set; }
        public Parameter[] Parameters { get; set; }
    }
    [Serializable]
    public class Parameter
    {
        public int Index { get; set; }
        public Type Type { get; set; }
        public dynamic Value { get; set; }

    }
}
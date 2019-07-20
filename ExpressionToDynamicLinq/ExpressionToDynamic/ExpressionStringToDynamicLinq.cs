using System.Linq.Expressions;

namespace System.Linq.Dynamic
{
    internal class ExpressionStringToDynamicLinq : ExpressionToDynamicLinqBase
    {
        private Expression _expression;
        public ExpressionStringToDynamicLinq(Expression expression)
        {
            this._expression = expression;
        }
        public string ToExpressionString()
        {
            return GetExpressionString(_expression);
        }
        //protected override string GetExpressionParameterValue(ParameterExpression parameter)
        //{
        //    //return string.Empty;
        //    return parameter == it ? "(it)" : "(outerIt)";
        //}
        protected override string GetExpressionCallValue(MethodCallExpression call)
        {
            if (predefinedTypes.Contains(call.Method.ReflectedType))
            {
                var arguments = call.Arguments.Select(f => GetExpressionValue(f)).ToArray();
                if (call.Object != null)
                    return string.Format("{0}.{1}({2})", GetExpressionValue(call.Object), call.Method.Name, string.Join(",", arguments));
                else
                    return string.Format("{0}.{1}({2})", call.Method.ReflectedType.Name, call.Method.Name, string.Join(",", arguments));
            }
            else
            {
                var isParameter = IsParameterExpression(call);
                if (isParameter == false)
                {
                    return LambdaExpressionInvokeValue(call);
                }
                else if (call.Arguments.Count > 0 && IsParameterExpression(call.Arguments[0]) == true)
                {
                    if (call.Arguments.Count == 1)
                    {
                        if (call.Object != null)
                            return string.Format("{0}[{1}]", GetExpressionValue(call.Object), GetExpressionValue(call.Arguments[0]));
                        else
                            return string.Format("{1}.{0}()", call.Method.Name, GetExpressionValue(call.Arguments[0]));
                    }
                    else if (call.Arguments.Count == 2)
                    {
                        var argument1 = GetExpressionValue(call.Arguments[0]);
                        outerIt = it;
                        it = null;
                        var argument2 = GetExpressionValue(call.Arguments[1]);
                        it = outerIt;
                        outerIt = null;
                        return string.Format("{1}.{0}({2})", call.Method.Name, argument1, argument2);
                    }
                    else
                    {
                        throw new Exception("GetExpressionCallValue");
                    }
                }
                else
                {
                    //Queryable
                    //DynamicClass
                    //EqualityComparer<>
                    //ILogicalSignatures
                    //IEqualitySignatures
                    //IRelationalSignatures
                    //IAddSignatures
                    //ISubtractSignatures
                    //IArithmeticSignatures
                    //INegationSignatures
                    //INotSignatures
                    //Nullable<>
                    //IEnumerable<>
                    //IEnumerableSignatures
                    //Enumerable
                    if (call.Arguments.Count == 1)
                    {
                        if (call.Object != null)
                            return string.Format("{0}[{1}]", GetExpressionValue(call.Object), GetExpressionValue(call.Arguments[0]));
                        else
                            return string.Format("{1}.{0}()", call.Method.Name, GetExpressionValue(call.Arguments[0]));
                    }
                    else if (call.Arguments.Count == 2)
                    {
                        switch (call.Method.Name)
                        {
                            case nameof(Enumerable.Contains):
                                {
                                    //((it).Name == \"a1\" || (it).Name == \"a2\" || (it).Name == \"a3\")
                                    var a0 = LambdaExpressionInvoke(call.Arguments[0]);
                                    var a1 = GetExpressionValue(call.Arguments[1]);
                                    var s111 = (a0 as Collections.IEnumerable).OfType<object>().Select(f => a1 + " == " + ConstantToValue(f)).ToArray();
                                    return "(" + string.Join(" || ", s111) + ")";
                                }
                            case nameof(Enumerable.Any):
                                {
                                    ////(((it).Name == "a1") || ((it).Name == "a2") || ((it).Name == "a3"))
                                    var a0 = LambdaExpressionInvoke(call.Arguments[0]);
                                    var a1 = GetExpressionValue(call.Arguments[1]);
                                    var s111 = (a0 as Collections.IEnumerable).OfType<object>().Select(f =>
                                    {
                                        return Text.RegularExpressions.Regex.Replace(a1, "\\(it\\)", evaluator => ConstantToValue(f)).Replace("(outerIt)", "(it)");
                                    }
                                    ).ToArray();
                                    return "(" + string.Join(" || ", s111) + ")";
                                }
                            case nameof(Enumerable.All):
                                {

                                }
                                break;
                            case nameof(Enumerable.Count):
                                {

                                }
                                break;
                            case nameof(Enumerable.Min):
                                {

                                }
                                break;
                            case nameof(Enumerable.Sum):
                                {

                                }
                                break;
                            default:
                                break;
                        }
                        throw new Exception("GetExpressionCallValue");
                    }
                    else
                    {
                        throw new Exception("GetExpressionCallValue");
                    }
                }
            }
            ////var callObject = GetExpressionValue(call.Object);
            ////System.Linq.Enumerable
            //if (predefinedTypes.Contains(call.Method.ReflectedType))
            //{
            //    var arguments = call.Arguments.Select(f => GetExpressionValue(f)).ToArray();
            //    return string.Format("{0}.{1}({2})", call.Method.ReflectedType.Name, call.Method.Name, string.Join(",", arguments));
            //}
            //else
            //{
            //    var isParameter = IsParameterExpression(call);
            //    if (isParameter == false)
            //    {
            //        return LambdaExpressionInvoke(call);
            //    }
            //    else
            //    {
            //        //Queryable
            //        //DynamicClass
            //        //EqualityComparer<>
            //        //ILogicalSignatures
            //        //IEqualitySignatures
            //        //IRelationalSignatures
            //        //IAddSignatures
            //        //ISubtractSignatures
            //        //IArithmeticSignatures
            //        //INegationSignatures
            //        //INotSignatures
            //        //Nullable<>
            //        //IEnumerable<>
            //        //IEnumerableSignatures
            //        //Enumerable
            //        if (call.Method.ReflectedType == typeof(Enumerable))
            //        {
            //            switch (call.Method.Name)
            //            {
            //                case nameof(Enumerable.Contains):
            //                    {
            //                        //typeof(Collections.IEnumerable).IsAssignableFrom(  call.Arguments[0].Type)
            //                        if (call.Arguments.Count == 2 && typeof(Collections.IEnumerable).IsAssignableFrom(call.Arguments[0].Type))
            //                        {
            //                            if (IsParameterExpression(call.Arguments[0]))
            //                            {
            //                                var arguments = call.Arguments.Select(f => GetExpressionValue(f)).ToArray();
            //                                return string.Format("{0}.Contains({1})", arguments);
            //                            }
            //                            else
            //                            {
            //                                var a0 = ssss(call.Arguments[0]);
            //                                var a1 = GetExpressionValue(call.Arguments[1]);
            //                                var s111 = (a0 as Collections.IEnumerable).OfType<object>().Select(f => a1 + " = " + ConstantToValue(f)).ToArray();
            //                                return "(" + string.Join(" || ", s111) + ")";
            //                            }
            //                        }
            //                        else
            //                        {
            //                            //return string.Format("{0}.{1}({2})", string.Join("", calls[0] as object[]), call.Method.Name, calls[1]);
            //                            //return string.Format("{0}.Contains({1})",);
            //                        }
            //                        break;
            //                    }
            //                case nameof(Enumerable.Any):
            //                    {

            //                    }
            //                    break;
            //                case nameof(Enumerable.All):
            //                    {

            //                    }
            //                    break;
            //                case nameof(Enumerable.Count):
            //                    {

            //                    }
            //                    break;
            //                case nameof(Enumerable.Min):
            //                    {

            //                    }
            //                    break;
            //                case nameof(Enumerable.Sum):
            //                    {

            //                    }
            //                    break;
            //                default:
            //                    //calls = call.Arguments.Select(f => GetExpressionValue(f, false)).ToArray();
            //                    //var callObject = GetExpressionValue(call.Object, false);
            //                    //return ConstantToValue(call.Method.Invoke(callObject, calls), isConstantValue);
            //                    break;
            //            }
            //        }
            //        if (call.Method.ReflectedType.IsGenericType && call.Method.ReflectedType.GetGenericTypeDefinition() == typeof(System.Collections.Generic.Dictionary<,>))
            //        {
            //            switch (call.Method.Name)
            //            {
            //                case nameof(Enumerable.Sum):
            //                    {

            //                    }
            //                    break;
            //            }
            //        }
            //        else
            //        {

            //        }
            //        //typeof(Enumerable)
            //        var sss = GetExpressionValue(call.Object);
            //        // return GetExpressionValue(call.Operand);
            //    }
            //}
            //throw new Exception("GetExpressionCallValue");
        }
    }
}
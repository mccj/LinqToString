using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Reflection;

namespace System.Linq.Dynamic
{
    public class ExpressionToDynamicLinq2 : ExpressionToDynamicLinqBase
    {
        private Expression _expression;
        public ExpressionToDynamicLinq2(Expression expression)
        {
            this._expression = expression;
        }
        public PredicateQueryable ToExpressionPredicate()
        {
            var predicate = GetExpressionValue(_expression);
            return new PredicateQueryable
            {
                Predicate = predicate,
                Parameters = _parameter.ToArray()
            };
        }
        protected override string GetExpressionParameterValue(ParameterExpression parameter)
        {
            return parameter == it ? "it" : "outerIt";
        }
        protected override string GetExpressionCallValue(MethodCallExpression call)
        {
            if (predefinedTypes.Contains(call.Method.ReflectedType))
            {
                var arguments = call.Arguments.Select(f => GetExpressionValue(f)).ToArray();
                return string.Format("{0}.{1}({2})", call.Method.ReflectedType.Name, call.Method.Name, string.Join(",", arguments));
            }
            else
            {
                var isParameter = IsParameterExpression(call);
                if (isParameter == false)
                {
                    return LambdaExpressionInvoke(call);
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
            }
        }
        protected override string ConstantToValue(object value)
        {
            if (value == null)
                return "null";
            else if (value is string)
                return $"\"{value}\"";
            else if (value is Enum)
                return $"\"{value}\"";
            else if (value is DateTime)
            {
                var date = (System.DateTime)value;
                return string.Format("DateTime({0})", date.Ticks);
            }
            else if (value is DateTimeOffset)
            {
                var date = (System.DateTimeOffset)value;
                return string.Format("DateTimeOffset({0},TimeSpan({1}))", date.Ticks, date.Offset.Ticks);
            }
            else if (value is TimeSpan)
            {
                var date = (System.TimeSpan)value;
                return string.Format("TimeSpan({0})", date.Ticks);
            }
            //else if (value is decimal)
            //{
            //    return string.Format("{0}M", value);
            //}
            //else if (value is double)
            //{
            //    return string.Format("{0}D", value);
            //}
            //else if (value is float)
            //{
            //    return string.Format("{0}F", value);
            //}
            else if (predefinedTypes.Contains(value.GetType()))
            {
                return Convert.ToString(value);
            }
            else
            {
                var v = _parameter.Where(f => f.Value == value).SingleOrDefault();
                if (v != null)
                {
                    return string.Format("@{0}", v.Index);
                }
                else
                {
                    var p = new Parameter { Value = value, Type = value.GetType(), Index = _parameter.Count == 0 ? 0 : _parameter.Max(f => f.Index) + 1 };
                    _parameter.Add(p);
                    return string.Format("@{0}", p.Index);
                }
            }
            //return Convert.ToString(value);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Reflection;

namespace System.Linq.Dynamic
{
    public abstract class ExpressionToDynamicLinqBase
    {
        protected static readonly Type[] predefinedTypes = {
            typeof(Object),
            typeof(Boolean),
            typeof(Char),
            typeof(String),
            typeof(SByte),
            typeof(Byte),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(Math),
            typeof(Convert),
            typeof(System.Data.Objects.EntityFunctions)
        };


        protected List<Parameter> _parameter = new List<Parameter>();
        protected ParameterExpression it = null;
        protected ParameterExpression outerIt = null;
        private List<ParameterExpression> _expressionParameter = new List<ParameterExpression>();
        private System.Collections.Concurrent.ConcurrentDictionary<Expression, bool> _expressionParameterCache = new System.Collections.Concurrent.ConcurrentDictionary<Expression, bool>();

        protected bool IsParameterExpression(Expression exp)
        {
            if (exp == null) return false;
            return _expressionParameterCache.GetOrAdd(exp, expression =>
            {
                if (expression is BinaryExpression)
                {
                    var call = expression as BinaryExpression;
                    return IsParameterExpression(call.Conversion) || IsParameterExpression(call.Left) || IsParameterExpression(call.Right);
                }
                else if (expression is BlockExpression)
                {
                    var call = expression as BlockExpression;
                    return call.Expressions.Any(f => IsParameterExpression(f)) || IsParameterExpression(call.Result) || call.Variables.Any(f => IsParameterExpression(f));
                }
                else if (expression is ConditionalExpression)
                {
                    var call = expression as ConditionalExpression;
                    return IsParameterExpression(call.IfFalse) || IsParameterExpression(call.IfTrue) || IsParameterExpression(call.Test);
                }
                else if (expression is ConstantExpression)
                {
                    //var call = expression as ConstantExpression;
                    return false;
                }
                else if (expression is DebugInfoExpression)
                {
                    //var call = expression as DebugInfoExpression;
                    return false;
                }
                else if (expression is DefaultExpression)
                {
                    //var call = expression as DefaultExpression;
                    return false;
                }
                //else if (expression is DynamicExpression)
                //{
                //    var call = expression as DynamicExpression;
                //    return call.Arguments.Any(f => IsParameterExpression(f));
                //}
                else if (expression is GotoExpression)
                {
                    var call = expression as GotoExpression;
                    return IsParameterExpression(call.Value);
                }
                else if (expression is IndexExpression)
                {
                    var call = expression as IndexExpression;
                    return call.Arguments.Any(f => IsParameterExpression(f)) || IsParameterExpression(call.Object);
                }
                else if (expression is InvocationExpression)
                {
                    var call = expression as InvocationExpression;
                    return call.Arguments.Any(f => IsParameterExpression(f)) || IsParameterExpression(call.Expression);
                }
                else if (expression is LabelExpression)
                {
                    var call = expression as LabelExpression;
                    return IsParameterExpression(call.DefaultValue);
                }
                else if (expression is LambdaExpression)
                {
                    var call = expression as LambdaExpression;
                    return IsParameterExpression(call.Body) || call.Parameters.Any(f => IsParameterExpression(f));
                }
                else if (expression is ListInitExpression)
                {
                    var call = expression as ListInitExpression;
                    return IsParameterExpression(call.NewExpression);
                }
                else if (expression is LoopExpression)
                {
                    var call = expression as LoopExpression;
                    return IsParameterExpression(call.Body);
                }
                else if (expression is MemberExpression)
                {
                    var call = expression as MemberExpression;
                    return IsParameterExpression(call.Expression);
                }
                else if (expression is MemberInitExpression)
                {
                    var call = expression as MemberInitExpression;
                    return IsParameterExpression(call.NewExpression);
                }
                else if (expression is MethodCallExpression)
                {
                    var call = expression as MethodCallExpression;
                    return call.Arguments.Any(f => IsParameterExpression(f)) || IsParameterExpression(call.Object);
                }
                else if (expression is NewArrayExpression)
                {
                    var call = expression as NewArrayExpression;
                    return call.Expressions.Any(f => IsParameterExpression(f));
                }
                else if (expression is NewExpression)
                {
                    var call = expression as NewExpression;
                    return call.Arguments.Any(f => IsParameterExpression(f));
                }
                else if (expression is ParameterExpression)
                {
                    var call = expression as ParameterExpression;
                    return call == null ? false : _expressionParameter.Contains(call);
                    //return false;
                }
                else if (expression is RuntimeVariablesExpression)
                {
                    var call = expression as RuntimeVariablesExpression;
                    return call.Variables.Any(f => IsParameterExpression(f));
                }
                else if (expression is SwitchExpression)
                {
                    var call = expression as SwitchExpression;
                    return IsParameterExpression(call.DefaultBody) || IsParameterExpression(call.SwitchValue);
                }
                else if (expression is TryExpression)
                {
                    var call = expression as TryExpression;
                    return IsParameterExpression(call.Body) || IsParameterExpression(call.Fault) || IsParameterExpression(call.Finally);
                }
                else if (expression is TypeBinaryExpression)
                {
                    var call = expression as TypeBinaryExpression;
                    return IsParameterExpression(call.Expression);
                }
                else if (expression is UnaryExpression)
                {
                    var call = expression as UnaryExpression;
                    return IsParameterExpression(call.Operand);
                }

                throw new Exception("IsParameterExpression   " + expression.NodeType);
            });
        }
        protected string GetExpressionValue(Expression expression)
        {
            if (expression == null) return string.Empty;
            switch (expression.NodeType)
            {
                #region Lambda
                case ExpressionType.Lambda:
                    {
                        var lambda = expression as LambdaExpression;
                        if (lambda.Name != null)
                        {

                        }
                        _expressionParameter.AddRange(lambda.Parameters);
                        it = lambda.Parameters.FirstOrDefault();
                        var str = GetExpressionValue(lambda.Body);
                        it = null;
                        foreach (var item in lambda.Parameters)
                        {
                            _expressionParameter.Remove(item);
                        }
                        return str;
                    }
                #endregion Lambda
                #region 比较操作 Relational
                case ExpressionType.Equal:
                    {
                        var equal = expression as BinaryExpression;
                        return string.Format("({0} == {1})", GetExpressionValue(equal.Left), GetExpressionValue(equal.Right));
                    }
                case ExpressionType.NotEqual:
                    {
                        var notEqual = expression as BinaryExpression;
                        return string.Format("({0} != {1})", GetExpressionValue(notEqual.Left), GetExpressionValue(notEqual.Right));
                    }
                case ExpressionType.GreaterThan:
                    {
                        var greaterThan = expression as BinaryExpression;
                        return string.Format("({0} > {1})", GetExpressionValue(greaterThan.Left), GetExpressionValue(greaterThan.Right));
                    }
                case ExpressionType.LessThan:
                    {
                        var lessThan = expression as BinaryExpression;
                        return string.Format("({0} < {1})", GetExpressionValue(lessThan.Left), GetExpressionValue(lessThan.Right));
                    }
                case ExpressionType.GreaterThanOrEqual:
                    {
                        var greaterThan = expression as BinaryExpression;
                        return string.Format("({0} >= {1})", GetExpressionValue(greaterThan.Left), GetExpressionValue(greaterThan.Right));
                    }
                case ExpressionType.LessThanOrEqual:
                    {
                        var lessThanOrEqual = expression as BinaryExpression;
                        return string.Format("({0} <= {1})", GetExpressionValue(lessThanOrEqual.Left), GetExpressionValue(lessThanOrEqual.Right));
                    }
                case ExpressionType.Or:
                    {
                        var or = expression as BinaryExpression;
                        return string.Format("({0} | {1})", GetExpressionValue(or.Left), GetExpressionValue(or.Right));
                    }
                case ExpressionType.OrElse:
                    {
                        var orElse = expression as BinaryExpression;
                        return string.Format("({0} || {1})", GetExpressionValue(orElse.Left), GetExpressionValue(orElse.Right));
                    }
                case ExpressionType.And:
                    {
                        var and = expression as BinaryExpression;
                        return string.Format("({0} & {1})", GetExpressionValue(and.Left), GetExpressionValue(and.Right));
                    }
                case ExpressionType.AndAlso:
                    {
                        var andAlso = expression as BinaryExpression;
                        return string.Format("({0} && {1})", GetExpressionValue(andAlso.Left), GetExpressionValue(andAlso.Right));
                    }
                case ExpressionType.Conditional:
                    {
                        var conditional = expression as ConditionalExpression;
                        var conditional1 = GetExpressionValue(conditional.Test);
                        var conditional2 = GetExpressionValue(conditional.IfTrue);
                        var conditional3 = GetExpressionValue(conditional.IfFalse);
                        //return string.Format("iif({0},{1},{2})", conditional1, conditional2, conditional3);
                        return string.Format("({0} ? {1} : {2})", conditional1, conditional2, conditional3);
                    }
                case ExpressionType.Subtract:
                    {
                        var subtract = expression as BinaryExpression;
                        return string.Format("({0} - {1})", GetExpressionValue(subtract.Left), GetExpressionValue(subtract.Right));
                    }
                case ExpressionType.Add:
                    {
                        var add = expression as BinaryExpression;
                        return string.Format("({0} + {1})", GetExpressionValue(add.Left), GetExpressionValue(add.Right));
                    }
                case ExpressionType.Modulo:
                    {
                        var modulo = expression as BinaryExpression;
                        return string.Format("({0} % {1})", GetExpressionValue(modulo.Left), GetExpressionValue(modulo.Right));
                    }
                case ExpressionType.Divide:
                    {
                        var divide = expression as BinaryExpression;
                        return string.Format("({0} / {1})", GetExpressionValue(divide.Left), GetExpressionValue(divide.Right));
                    }
                case ExpressionType.Multiply:
                    {
                        var multiply = expression as BinaryExpression;
                        return string.Format("({0} * {1})", GetExpressionValue(multiply.Left), GetExpressionValue(multiply.Right));
                    }
                case ExpressionType.Not:
                    {
                        var not = expression as UnaryExpression;
                        return string.Format("(!{0})", GetExpressionValue(not.Operand));
                    }
                case ExpressionType.Negate:
                    {
                        var negate = expression as UnaryExpression;
                        return string.Format("(-{0})", GetExpressionValue(negate.Operand));
                    }
                case ExpressionType.ArrayIndex:
                    {
                        var arrayIndex = expression as BinaryExpression;
                        var isParameter = IsParameterExpression(arrayIndex);
                        if (isParameter == false)
                        {
                            return LambdaExpressionInvoke(arrayIndex);
                        }
                        else
                        {
                            return string.Format("{0}[{1}]", GetExpressionValue(arrayIndex.Left), GetExpressionValue(arrayIndex.Right));
                        }
                    }
                case ExpressionType.NewArrayInit:
                    {
                        var newArrayInit = expression as NewArrayExpression;
                        var isParameter = IsParameterExpression(newArrayInit);
                        if (isParameter == false)
                        {
                            return LambdaExpressionInvoke(newArrayInit);
                        }
                        else
                        {
                            //return string.Format("{0}[{1}]", GetExpressionValue(arrayIndex.Left), GetExpressionValue(arrayIndex.Right));
                        }
                        break;
                    }
                #endregion 比较操作 Relational
                case ExpressionType.Constant:
                    {
                        var constant = expression as ConstantExpression;
                        //return ConstantToValue(constant.Value, isConstantValue);
                        return ConstantToValue(constant.Value);
                    }
                case ExpressionType.Parameter:
                    {
                        var parameter = expression as ParameterExpression;
                        return GetExpressionParameterValue(parameter);
                    }
                case ExpressionType.MemberAccess:
                    {
                        var memberAccess = expression as MemberExpression;
                        var isParameter = IsParameterExpression(memberAccess);
                        if (isParameter == false)
                        {
                            return LambdaExpressionInvoke(memberAccess);
                        }
                        else
                        {
                            var str = GetExpressionValue(memberAccess.Expression);
                            return (string.IsNullOrWhiteSpace(str) ? string.Empty : str + ".") + memberAccess.Member.Name;
                        }
                    }
                case ExpressionType.Convert:
                    {
                        var convert = expression as UnaryExpression;
                        var isParameter = IsParameterExpression(convert);
                        if (isParameter == false)
                        {
                            //if (convert.Operand.Type.IsEnum)
                            //{
                            //    return LambdaExpressionInvoke(convert.Operand);
                            //}
                            //else
                            {
                                return LambdaExpressionInvoke(convert);
                            }
                        }
                        else
                        {
                            return GetExpressionValue(convert.Operand);
                        }
                    }
                case ExpressionType.Call:
                    {
                        var call = expression as MethodCallExpression;
                        return GetExpressionCallValue(call);
                    }
                case ExpressionType.New:
                    {
                        var new_ = expression as NewExpression;
                        if (predefinedTypes.Any(f => f == new_.Type))
                            return new_.Type.Name + "(" + string.Join(",", new_.Arguments.Select(f => GetExpressionValue(f))) + ")";
                        if (new_.Type.GetCustomAttributes(true).Any(f=>f is System.Runtime.CompilerServices.CompilerGeneratedAttribute))
                        {
                            var constructList = new List<string>();
                            var arguments = new_.Arguments.Select(f => GetExpressionValue(f)).ToArray();
                            for (int i = 0; i < new_.Members.Count; i++)
                            {
                                constructList.Add(arguments[i] + " as " + new_.Members[i].Name);
                            }
                            return string.Format("new({0})", string.Join(",", constructList));
                        }
                        else
                        {
                            var newExpr = Expression.Lambda(expression, null);//创建一个Lambda表达式
                            var value = newExpr.Compile().DynamicInvoke();
                            return ConstantToValue(value);
                        }
                    }
                case ExpressionType.AddChecked:
                    break;
                case ExpressionType.ArrayLength:
                    break;
                case ExpressionType.Coalesce:
                    break;
                case ExpressionType.ConvertChecked:
                    break;
                case ExpressionType.ExclusiveOr:
                    break;
                case ExpressionType.Invoke:
                    break;
                case ExpressionType.LeftShift:
                    break;
                case ExpressionType.ListInit:
                    break;
                case ExpressionType.MemberInit:
                    break;
                case ExpressionType.MultiplyChecked:
                    break;
                case ExpressionType.UnaryPlus:
                    break;
                case ExpressionType.NegateChecked:
                    break;
                case ExpressionType.NewArrayBounds:
                    break;
                case ExpressionType.Power:
                    break;
                case ExpressionType.Quote:
                    break;
                case ExpressionType.RightShift:
                    break;
                case ExpressionType.SubtractChecked:
                    break;
                case ExpressionType.TypeAs:
                    break;
                case ExpressionType.TypeIs:
                    break;
                case ExpressionType.Assign:
                    break;
                case ExpressionType.Block:
                    break;
                case ExpressionType.DebugInfo:
                    break;
                case ExpressionType.Decrement:
                    break;
                case ExpressionType.Dynamic:
                    break;
                case ExpressionType.Default:
                    break;
                case ExpressionType.Extension:
                    break;
                case ExpressionType.Goto:
                    break;
                case ExpressionType.Increment:
                    break;
                case ExpressionType.Index:
                    break;
                case ExpressionType.Label:
                    break;
                case ExpressionType.RuntimeVariables:
                    break;
                case ExpressionType.Loop:
                    break;
                case ExpressionType.Switch:
                    break;
                case ExpressionType.Throw:
                    break;
                case ExpressionType.Try:
                    break;
                case ExpressionType.Unbox:
                    break;
                case ExpressionType.AddAssign:
                    break;
                case ExpressionType.AndAssign:
                    break;
                case ExpressionType.DivideAssign:
                    break;
                case ExpressionType.ExclusiveOrAssign:
                    break;
                case ExpressionType.LeftShiftAssign:
                    break;
                case ExpressionType.ModuloAssign:
                    break;
                case ExpressionType.MultiplyAssign:
                    break;
                case ExpressionType.OrAssign:
                    break;
                case ExpressionType.PowerAssign:
                    break;
                case ExpressionType.RightShiftAssign:
                    break;
                case ExpressionType.SubtractAssign:
                    break;
                case ExpressionType.AddAssignChecked:
                    break;
                case ExpressionType.MultiplyAssignChecked:
                    break;
                case ExpressionType.SubtractAssignChecked:
                    break;
                case ExpressionType.PreIncrementAssign:
                    break;
                case ExpressionType.PreDecrementAssign:
                    break;
                case ExpressionType.PostIncrementAssign:
                    break;
                case ExpressionType.PostDecrementAssign:
                    break;
                case ExpressionType.TypeEqual:
                    break;
                case ExpressionType.OnesComplement:
                    break;
                case ExpressionType.IsTrue:
                    break;
                case ExpressionType.IsFalse:
                    break;
                default:
                    break;
            }
            throw new Exception("GetExpressionValue   " + expression.NodeType);
        }
        protected virtual string GetExpressionParameterValue(ParameterExpression parameter)
        {
            throw new Exception("GetExpressionParameterValue");
        }
        protected virtual string GetExpressionCallValue(MethodCallExpression call)
        {
            throw new Exception("GetExpressionCallValue");
        }
        protected object ssss(Expression body)
        {
            var expr = Expression.Lambda(body, null);//创建一个Lambda表达式
            var value = expr.Compile().DynamicInvoke();
            return value;
        }
        
        protected string LambdaExpressionInvoke(Expression body)
        {
            var value = ssss(body);
            return ConstantToValue(value);
        }

        protected  virtual string ConstantToValue(object value)
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
            //        else if (type == typeof(string[]))
            //        {
            //            var arror = value as string[];
            //            return arror.Select(f => ConstantToValue(f, isConstantValue)).ToArray();
            //        }

            //        else if (type == typeof(decimal))
            //        {
            //            return string.Format("{0}D", value);
            //        }
            //        else if (value == null)
            //        {
            //            return "null";
            //        }

            return Convert.ToString(value);
        }
    }
}
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq.Dynamic
{
    internal abstract class ExpressionToDynamicLinqBase
    {
        protected static readonly Type[] predefinedTypes = getPredefinedTypes();

        private static Type[] getPredefinedTypes()
        {
            var _predefinedTypes = new List<Type> {
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
                typeof(Uri),
                //typeof(System.Data.Objects.EntityFunctions)
        };

            var TryAdd = new Action<string>(typeName =>
            {
                try
                {
                    Type efType = Type.GetType(typeName);
                    if (efType != null)
                    {
                        _predefinedTypes.Add(efType);
                    }
                }
                catch
                {
                    // in case of exception, do not add
                }
            });

#if !(NET35 || SILVERLIGHT || NETFX_CORE || WINDOWS_APP || DOTNET5_1 || UAP10_0 || NETSTANDARD)
            //System.Data.Entity is always here, so overwrite short name of it with EntityFramework if EntityFramework is found.
            //EF5(or 4.x??), System.Data.Objects.DataClasses.EdmFunctionAttribute
            //There is also an System.Data.Entity, Version=3.5.0.0, but no Functions.
            TryAdd("System.Data.Objects.EntityFunctions, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            TryAdd("System.Data.Objects.SqlClient.SqlFunctions, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            TryAdd("System.Data.Objects.SqlClient.SqlSpatialFunctions, System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            //EF6,System.Data.Entity.DbFunctionAttribute
            TryAdd("System.Data.Entity.Core.Objects.EntityFunctions, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            TryAdd("System.Data.Entity.DbFunctions, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            TryAdd("System.Data.Entity.Spatial.DbGeography, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            TryAdd("System.Data.Entity.SqlServer.SqlFunctions, EntityFramework.SqlServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            TryAdd("System.Data.Entity.SqlServer.SqlSpatialFunctions, EntityFramework.SqlServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
#endif
#if NETSTANDARD2_0
            TryAdd("Microsoft.EntityFrameworkCore.DynamicLinq.DynamicFunctions, Microsoft.EntityFrameworkCore.DynamicLinq, Version=1.0.0.0, Culture=neutral, PublicKeyToken=974e7e1b462f3693");
#endif
            return _predefinedTypes.ToArray();
        }

        protected List<Parameter> _parameter = new List<Parameter>();
        protected ParameterExpression root = null;
        protected ParameterExpression it = null;
        protected ParameterExpression outerIt = null;
        private List<ParameterExpression> _expressionParameter = new List<ParameterExpression>();
        private System.Collections.Concurrent.ConcurrentDictionary<Expression, bool> _expressionParameterCache = new System.Collections.Concurrent.ConcurrentDictionary<Expression, bool>();

#if Kahanu_System_Linq_Dynamic
        public const string KEYWORD_IT = "it";
        public const string KEYWORD_PARENT = "outerIt";
#else
        public const string KEYWORD_IT = "it";
        public const string KEYWORD_PARENT = "parent";
        public const string KEYWORD_ROOT = "root";

        public const string SYMBOL_IT = "$";
        public const string SYMBOL_PARENT = "^";
        public const string SYMBOL_ROOT = "~";
#endif


        private void Init()
        {
            root = null;
            it = null;
            outerIt = null;
            _parameter.Clear();
            _expressionParameter.Clear();
        }
        public string GetExpressionString(Expression exp)
        {
            Init();
            return GetExpressionValue(exp);
        }
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
                        if (root == null) root = it;
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
                        return GetBinaryExpressionEnumValue("({0} == {1})", equal);
                        //return string.Format("({0} == {1})", GetExpressionValue(equal.Left), GetExpressionValue( equal.Right));
                    }
                case ExpressionType.NotEqual:
                    {
                        var notEqual = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} != {1})", notEqual);
                        //return string.Format("({0} != {1})", GetExpressionValue(notEqual.Left), GetExpressionValue(notEqual.Right));
                    }
                case ExpressionType.GreaterThan:
                    {
                        var greaterThan = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue(greaterThan, "({0} > {1})", "({0} <= {1})");
                        //return string.Format("({0} > {1})", GetExpressionValue(greaterThan.Left), GetExpressionValue(greaterThan.Right));
                    }
                case ExpressionType.LessThan:
                    {
                        var lessThan = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue(lessThan, "({0} < {1})", "({0} >= {1})");
                        //return string.Format("({0} < {1})", GetExpressionValue(lessThan.Left), GetExpressionValue(lessThan.Right));
                    }
                case ExpressionType.GreaterThanOrEqual:
                    {
                        var greaterThan = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue(greaterThan, "({0} >= {1})", "({0} < {1})");
                        //return string.Format("({0} >= {1})", GetExpressionValue(greaterThan.Left), GetExpressionValue(greaterThan.Right));
                    }
                case ExpressionType.LessThanOrEqual:
                    {
                        var lessThanOrEqual = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue(lessThanOrEqual, "({0} <= {1})", "({0} > {1})");
                        //return string.Format("({0} <= {1})", GetExpressionValue(lessThanOrEqual.Left), GetExpressionValue(lessThanOrEqual.Right));
                    }
                case ExpressionType.Or:
                    {
                        var or = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} | {1})", or);
                        //return string.Format("({0} | {1})", GetExpressionValue(or.Left), GetExpressionValue(or.Right));
                    }
                case ExpressionType.OrElse:
                    {
                        var orElse = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} || {1})", orElse);
                        //return string.Format("({0} || {1})", GetExpressionValue(orElse.Left), GetExpressionValue(orElse.Right));
                    }
                case ExpressionType.And:
                    {
                        var and = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} & {1})", and);
                        //return string.Format("({0} & {1})", GetExpressionValue(and.Left), GetExpressionValue(and.Right));
                    }
                case ExpressionType.AndAlso:
                    {
                        var andAlso = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} && {1})", andAlso);
                        //return string.Format("({0} && {1})", GetExpressionValue(andAlso.Left), GetExpressionValue(andAlso.Right));
                    }
                case ExpressionType.Subtract:
                    {
                        var subtract = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} - {1})", subtract);
                        //return string.Format("({0} - {1})", GetExpressionValue(subtract.Left), GetExpressionValue(subtract.Right));
                    }
                case ExpressionType.Add:
                    {
                        var add = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} + {1})", add);
                        //return string.Format("({0} + {1})", GetExpressionValue(add.Left), GetExpressionValue(add.Right));
                    }
                case ExpressionType.Modulo:
                    {
                        var modulo = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} % {1})", modulo);
                        //return string.Format("({0} % {1})", GetExpressionValue(modulo.Left), GetExpressionValue(modulo.Right));
                    }
                case ExpressionType.Divide:
                    {
                        var divide = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} / {1})", divide);
                        //return string.Format("({0} / {1})", GetExpressionValue(divide.Left), GetExpressionValue(divide.Right));
                    }
                case ExpressionType.Multiply:
                    {
                        var multiply = expression as BinaryExpression;
                        return GetBinaryExpressionEnumValue("({0} * {1})", multiply);
                        //return string.Format("({0} * {1})", GetExpressionValue(multiply.Left), GetExpressionValue(multiply.Right));
                    }
                case ExpressionType.Conditional:
                    {
                        var conditional = expression as ConditionalExpression;

                        var isParameter = IsParameterExpression(conditional.Test);
                        if (isParameter)
                        {
                            var conditional1 = GetExpressionValue(conditional.Test);
                            var conditional2 = GetExpressionValue(conditional.IfTrue);
                            var conditional3 = GetExpressionValue(conditional.IfFalse);

                            //return string.Format("iif({0},{1},{2})", conditional1, conditional2, conditional3);
                            return string.Format("({0} ? {1} : {2})", conditional1, conditional2, conditional3);
                        }
                        else
                        {
                            var newExpr = Expression.Lambda(conditional.Test, null);//创建一个Lambda表达式
                            var value = (bool)newExpr.Compile().DynamicInvoke();
                            return GetExpressionValue(value ? conditional.IfTrue : conditional.IfFalse);
                        }
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
                            return LambdaExpressionInvokeValue(arrayIndex);
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
                            return LambdaExpressionInvokeValue(newArrayInit);
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
                            return LambdaExpressionInvokeValue(memberAccess);
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
                            if (convert.Operand.Type.IsEnum)
                            {
                                return LambdaExpressionInvokeValue(convert.Operand);
                            }
                            else
                            {
                                return LambdaExpressionInvokeValue(convert);
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
                        if (new_.Type.GetCustomAttributes(true).Any(f => f is System.Runtime.CompilerServices.CompilerGeneratedAttribute))
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
                case ExpressionType.ArrayLength:
                    {
                        var arrayLength = expression as UnaryExpression;
                        break;
                    }
                case ExpressionType.AddChecked:
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
#if Kahanu_System_Linq_Dynamic
            return parameter == it ? KEYWORD_IT : KEYWORD_PARENT;
#else
            if (parameter == it) return KEYWORD_IT;
            if (parameter == outerIt) return KEYWORD_PARENT;
            if (parameter == root) return KEYWORD_ROOT;

            //if (parameter == it) return SYMBOL_IT;
            //if (parameter == outerIt) return SYMBOL_PARENT;
            //if (parameter == root) return SYMBOL_ROOT;
#endif
            throw new Exception("错误的 parameter");
        }
        protected virtual string GetExpressionCallValue(MethodCallExpression call)
        {
            throw new Exception("GetExpressionCallValue");
        }
        protected string GetBinaryExpressionEnumValue(BinaryExpression expression, string format, string reversalFormat = null)
        {
#if Kahanu_System_Linq_Dynamic
            var leftParameterExpression = IsParameterExpression(expression.Left);
            var rightParameterExpression = IsParameterExpression(expression.Right);
            if (leftParameterExpression != rightParameterExpression)
            {
                var unaryExpression = (leftParameterExpression ? expression.Left : expression.Right) as UnaryExpression;
                //枚举
                if (unaryExpression?.Operand?.Type?.IsEnum == true && leftParameterExpression)
                {//Parameter值在左边，需要反转，并转换参数
//#if Kahanu_System_Linq_Dynamic
                    var value = GetExpressionValue(expression.Right);
//#else
//                    var rightExpressionValue = LambdaExpressionInvokeValue(expression.Right);
//                    var enumRightExpressionValue = Enum.Parse(unaryExpression?.Operand?.Type, rightExpressionValue);
//                    var value = ConstantToValue(enumRightExpressionValue);
//                    //var value = GetExpressionValue(expression.Right);
//#endif
                    return string.Format(reversalFormat ?? format, value, GetExpressionValue(expression.Left));
                }
                else if (unaryExpression?.Operand?.Type?.IsEnum == true && rightParameterExpression)
                {//Parameter值在右边，不需要反转，并转换参数
//#if Kahanu_System_Linq_Dynamic
                    var value = LambdaExpressionInvokeValue(expression.Left);
//#else
//                    var leftExpressionValue = LambdaExpressionInvokeValue(expression.Left);
//                    var enumLeftExpressionValue = Enum.Parse(unaryExpression?.Operand?.Type, leftExpressionValue);
//                    var value = ConstantToValue(enumLeftExpressionValue);
//                    //var value = GetExpressionValue(expression.Left);
//#endif
                    return string.Format(format, value, GetExpressionValue(expression.Right));
                }
            }
            //else if (leftParameterExpression == rightParameterExpression == true)
            //{
            //    //时间 DateTimeOffset
            //    var leftExpression = expression.Left as UnaryExpression;
            //    var rightExpression = expression.Right as UnaryExpression;
            //    if (leftExpression?.Type == typeof(System.DateTimeOffset) && leftExpression?.Operand?.Type == typeof(System.DateTime))
            //    {
            //        return string.Format(format, GetExpressionValue(expression.Left), GetExpressionValue(expression.Right));
            //    }
            //    else if (rightExpression?.Type == typeof(System.DateTimeOffset) && rightExpression?.Operand?.Type == typeof(System.DateTime))
            //    {
            //        return string.Format(reversalFormat ?? format, GetExpressionValue(expression.Right), GetExpressionValue(expression.Left));
            //    }
            //}
#endif
            {//其他
                //return string.Format(format, GetExpressionValue(expression.Left), GetExpressionValue(expression.Right));
                return GetBinaryExpressionEnumValue(format, expression);
            }
        }
        protected string GetBinaryExpressionEnumValue(string format, BinaryExpression expression)
        {
            var leftParameterExpression = IsParameterExpression(expression.Left);
            var rightParameterExpression = IsParameterExpression(expression.Right);
            var unaryExpression = (leftParameterExpression ? expression.Left : expression.Right) as UnaryExpression;
            //枚举
            if (leftParameterExpression != rightParameterExpression && unaryExpression?.Operand?.Type?.IsEnum == true && leftParameterExpression)
            {
                var _enumValue = LambdaExpressionInvokeEnumValue(expression.Right, unaryExpression.Operand.Type);
                return string.Format(format, GetExpressionValue(expression.Left), _enumValue);
            }
            else if (leftParameterExpression != rightParameterExpression && unaryExpression?.Operand?.Type?.IsEnum == true && rightParameterExpression)
            {
                var _enumValue = LambdaExpressionInvokeEnumValue(expression.Left, unaryExpression.Operand.Type);
                return string.Format(format, _enumValue, GetExpressionValue(expression.Right));
            }
            //时间 DateTimeOffset
            if (leftParameterExpression != rightParameterExpression && unaryExpression?.Type == typeof(System.DateTimeOffset) && leftParameterExpression/* && expression.Right.NodeType == ExpressionType.MemberAccess*/)
            {
                //if (((MemberExpression)expression.Right).Expression != null)
                //{
                var _enumValue = LambdaExpressionInvokeEnumValue(expression.Right, unaryExpression.Operand.Type);
                return string.Format(format, GetExpressionValue(expression.Left), _enumValue);
                //}
            }
            else if (leftParameterExpression != rightParameterExpression && unaryExpression?.Type == typeof(System.DateTimeOffset) && rightParameterExpression/* && expression.Left.NodeType == ExpressionType.MemberAccess*/)
            {
                //if (((MemberExpression)expression.Left).Expression != null)
                //{
                var _enumValue = LambdaExpressionInvokeEnumValue(expression.Left, unaryExpression.Operand.Type);
                return string.Format(format, _enumValue, GetExpressionValue(expression.Right));
                //}
            }
            {//其他
                return string.Format(format, GetExpressionValue(expression.Left), GetExpressionValue(expression.Right));
            }
        }
        protected object LambdaExpressionInvoke(Expression body)
        {
            var expr = Expression.Lambda(body, null);//创建一个Lambda表达式
            var value = expr.Compile().DynamicInvoke();
            return value;
        }
        protected string LambdaExpressionInvokeEnumValue(Expression body, Type type)
        {
            var value = LambdaExpressionInvoke(body);
            if (type.IsEnum)
            {
                var enumValue = Enum.ToObject(type, value);
                return ConstantToValue(enumValue);
            }
            else if (type == typeof(System.DateTimeOffset) && value is System.DateTime)
            {
                var dataValue = new System.DateTimeOffset((System.DateTime)value);
                return ConstantToValue(dataValue);
            }
            else if (type == typeof(System.DateTime) && value is System.DateTimeOffset)
            {
                return ConstantToValue(((System.DateTimeOffset)value).DateTime);
            }
            else
            {
                return ConstantToValue(value);
            }
        }
        protected string LambdaExpressionInvokeValue(Expression body)
        {
            var value = LambdaExpressionInvoke(body);
            return ConstantToValue(value);
        }
        protected virtual string ConstantToValue(object value)
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

            //new(\"dd\" as name)


            var type = value.GetType();
            if (!predefinedTypes.Any(f => f == type))
            {
                var props = type.GetProperties();
                var constructList = props.Select(f => ConstantToValue(f.GetValue(value, null)) + " as " + f.Name).ToArray();
                return string.Format("new({0})", string.Join(",", constructList));
            }
            else
            {
                return Convert.ToString(value);
            }
        }
    }
}
using System;
using System.Globalization;
using System.Linq.Expressions;

namespace FileRandomizer3000.Core.ViewModels
{
    /// <summary>
    /// Taken from: http://www.limilabs.com/blog/property-name-from-lambda
    /// </summary>
    public static class PropertyName
    {
        public static string For<T>(Expression<Func<T, object>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            Expression body = expression.Body;
            return GetMemberName(body);
        }

        public static string For(Expression<Func<object>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            Expression body = expression.Body;
            return GetMemberName(body);
        }

        public static string GetMemberName(Expression expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            if (expression is MemberExpression)
            {
                MemberExpression memberExpression = expression as MemberExpression;

                if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression.Expression) + "." + memberExpression.Member.Name;
                }

                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                UnaryExpression unaryExpression = expression as UnaryExpression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                {
                    throw new Exception(string.Format(CultureInfo.CurrentCulture, "Cannot interpret member from {0}", expression));
                }

                return GetMemberName(unaryExpression.Operand);
            }

            throw new Exception(string.Format("Could not determine member from {0}", expression));
        }
    }
}

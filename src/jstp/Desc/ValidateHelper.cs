using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace jstp
{
    public class ValidateHelper
    {
        public static char[] WhiteSpace = { ' ', '\n', '\t' };

        public static string Name(Expression<Func<string>> prop)
        {
            var name = GetMemberName(prop);
            var value = prop.Compile()();
            return Name(value, name);
        }

        public static bool IsValidName(string value)
        {
            return Regex.IsMatch(value, "^[a-zA-Z][a-zA-Z0-9_]*");
        }

        public static bool IsValidVersion(string versionString)
        {
            Version ver;
            return System.Version.TryParse(versionString, out ver);
        }

        public static string Name(string value, string paramName)
        {
            if (!IsValidName(value))
            {
                RiseValidateException(string.Format("Param '{0}' must start with character. Then character ot symbol '_'. Value:{1}", paramName, value), paramName);
            }
            return value.Trim(WhiteSpace);
        }

        public static Version Version(Expression<Func<string>> prop)
        {
            var name = GetMemberName(prop);
            var value = prop.Compile()();
            return Version(value, name);
        }

        public static void CompatibleVersion(Expression<Func<string>> prop, Version supported)
        {
            var name = GetMemberName(prop);
            var version = Version(prop);
            if (version.Major != supported.Major) RiseValidateException(string.Format("Field {0} : version not supported. Support {0}.X.X", name, version.Major),name);
        }

        public static Version Version(string value, string paramName)
        {
            Version ver;
            if (!System.Version.TryParse(value,out ver))
            {
                RiseValidateException(string.Format("Param '{0}' not version type. For example 1.0.0. Value:{1}", paramName, value), paramName);
            }
            return ver;
        }
        public static void RiseValidateException(string message, Expression<Func<string>> prop)
        {
            var name = GetMemberName(prop);
            RiseValidateException(message, name);
        }

        public static void RiseValidateException(string message, string paramName)
        {
            throw new ArgumentException(message, paramName);
        }

        public static string GetMemberName(LambdaExpression memberSelector)
        {
            Func<Expression, string> nameSelector = null;  //recursive func
            nameSelector = e => //or move the entire thing to a separate recursive method
                           {
                               switch (e.NodeType)
                               {
                                   case ExpressionType.Parameter:
                                       return ((ParameterExpression)e).Name;
                                   case ExpressionType.MemberAccess:
                                       return ((MemberExpression)e).Member.Name;
                                   case ExpressionType.Call:
                                       return ((MethodCallExpression)e).Method.Name;
                                   case ExpressionType.Convert:
                                   case ExpressionType.ConvertChecked:
                                       return nameSelector(((UnaryExpression)e).Operand);
                                   case ExpressionType.Invoke:
                                       return nameSelector(((InvocationExpression)e).Expression);
                                   case ExpressionType.ArrayLength:
                                       return "Length";
                                   default:
                                       throw new Exception("not a proper member selector");
                               }
                           };

            return nameSelector(memberSelector.Body);
        }

        public static void NotNullOrWhiteSpace(Expression<Func<string>> prop)
        {
            var value = prop.Compile()();
            var name = GetMemberName(prop);
            if (string.IsNullOrWhiteSpace(value)) RiseValidateException(string.Format("Field '{0}' is null or white space", name),name);
        }

       
    }
}
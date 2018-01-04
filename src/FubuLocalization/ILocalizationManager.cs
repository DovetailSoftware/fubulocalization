using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FubuLocalization
{
    public interface ILocalizationManager
    {
        string GetTextForKey(StringToken token);
        string GetText(PropertyInfo property);
        string GetText<T>(Expression<Func<T, object>> expression);
        LocalString GetLocalString(LambdaExpression expression);
        string GetText(Type type);
        StringToken KeyFromType(Type type);
        string GetHeader(PropertyInfo property);
        string GetHeader(PropertyToken token);
        string GetHeader<T>(Expression<Func<T, object>> expression);
        string GetTextForType(string name);
        string GetPluralTextForType(Type type);
        string GetPluralTextForType(string type);
    }
}
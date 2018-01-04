using System;
using System.Linq.Expressions;
using System.Reflection;
using FubuCore.Reflection;

namespace FubuLocalization
{
    public class DefaultLocalizationManager : ILocalizationManager
    {
        private readonly ILocalizationDataProvider _localizationDataProvider;
        private const string PluralSufix = "_PLURAL";

        public DefaultLocalizationManager(ILocalizationDataProvider localizationDataProvider)
        {
            _localizationDataProvider = localizationDataProvider;
        }
        
        //public string ToHeader(this PropertyInfo property)
        //{
        //    return GetHeader(property);
        //}

        //public string ToHeader(this Accessor property)
        //{
        //    return GetHeader(property.InnerProperty);
        //}
        
        public string GetTextForKey(StringToken token)
        {
            return _localizationDataProvider.GetTextForKey(token);
        }

        public string GetText(PropertyInfo property)
        {
            return _localizationDataProvider.GetHeader(property);
        }

        public string GetText<T>(Expression<Func<T, object>> expression)
        {
            var propertyInfo = ReflectionHelper.GetProperty(expression);
            return GetHeader(propertyInfo);
        }

        public LocalString GetLocalString(LambdaExpression expression)
        {
            var propertyInfo = ReflectionHelper.GetProperty(expression);
            return new LocalString { display = GetHeader(propertyInfo), value = propertyInfo.Name };
        }

        public string GetText(Type type)
        {
            return _localizationDataProvider.GetTextForKey(KeyFromType(type));
        }

        public StringToken KeyFromType(Type type)
        {
            return StringToken.FromKeyString(type.Name);
        }

        public string GetHeader(PropertyInfo property)
        {
            return _localizationDataProvider.GetHeader(property);
        }

        public string GetHeader(PropertyToken token)
        {
            return _localizationDataProvider.GetHeader(token);
        }

        public string GetHeader<T>(Expression<Func<T, object>> expression)
        {
            var propertyInfo = ReflectionHelper.GetProperty(expression);
            return GetHeader(propertyInfo);
        }

        public string GetTextForType(string name)
        {
            return _localizationDataProvider.GetTextForKey(StringToken.FromKeyString(name));
        }

        public string GetPluralTextForType(Type type)
        {
            return GetPluralTextForType(type.Name);
        }

        public string GetPluralTextForType(string type)
        {
            return _localizationDataProvider.GetTextForKey(StringToken.FromKeyString(type + PluralSufix));
        }
    }
}
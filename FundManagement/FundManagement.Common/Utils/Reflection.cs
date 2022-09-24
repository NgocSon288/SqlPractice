using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FundManagement.Common.Utils
{
    public static class Reflection
    {
        public static object InvokePrivateMethod(Type sourceMethod, string methodName, object instance, Type genergicType, object[] parameters)
        {
            MethodInfo method = sourceMethod.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo generic = method.MakeGenericMethod(genergicType);

            return generic.Invoke(instance, parameters);
        }
        public static Type GetTypeFromAnotherAssembly(string assemblyFullPath, string classNameFullPath)
        {
            var assembly = Assembly.LoadFile(assemblyFullPath);
            var instance = assembly.CreateInstance(classNameFullPath);

            return instance.GetType();
        }
        public static class RefClass
        {
            #region Infrastrucure

            private static Type[] GetGenericTypes(PropertyInfo propertyInfo)
            {
                return propertyInfo
                    .DeclaringType
                    .GenericTypeArguments;
            }
            private static Type GetUnderlyingSystemType(PropertyInfo propertyInfo)
            {
                return GetGenericTypes(propertyInfo)[0].UnderlyingSystemType;
            }
            private static string GetAttributeValueFromCustomAttributeData(CustomAttributeData customAttribute, string attributeClassName)
            {
                if (customAttribute == null)
                {
                    return null;
                }

                return customAttribute.ConstructorArguments[0].Value.ToString();
            }

            #endregion

            #region Attribute

            public static List<CustomAttributeData> GetAllClassAttributes(PropertyInfo propertyInfo)
            {
                var types = GetGenericTypes(propertyInfo);
                return types[0]
                    .CustomAttributes
                    .ToList();
            }
            public static List<CustomAttributeData> GetAllClassAttributes<TEntity>(TEntity entity)
            {
                return entity
                    .GetType()
                    .CustomAttributes
                    .ToList();
            }
            public static CustomAttributeData GetClassAttributeByName(PropertyInfo propertyInfo, string attributeClassName)
            {
                return GetAllClassAttributes(propertyInfo).FirstOrDefault(e => e.AttributeType.Name == attributeClassName);
            }
            public static CustomAttributeData GetClassAttributeByName<TEntity>(TEntity entity, string attributeClassName)
            {
                return GetAllClassAttributes(entity).FirstOrDefault(e => e.AttributeType.Name == attributeClassName);
            }
            public static string GetAttributeValue(PropertyInfo propertyInfo, string attributeClassName)
            {
                return GetAttributeValueFromCustomAttributeData(GetClassAttributeByName(propertyInfo, attributeClassName), attributeClassName);
            }
            public static string GetAttributeValue<TEntity>(TEntity entity, string attributeClassName)
            {
                return GetAttributeValueFromCustomAttributeData(GetClassAttributeByName(entity, attributeClassName), attributeClassName);
            }

            #endregion

            #region Defination

            public static string GetClassNameWithinNamespace(PropertyInfo propertyInfo)
            {
                return GetUnderlyingSystemType(propertyInfo).FullName;
            }
            public static string GetClassNameWithinNamespace<TEntity>()
            {
                return typeof(TEntity).GetTypeInfo().FullName;
            }
            public static string GetClassName(PropertyInfo propertyInfo)
            {
                return GetUnderlyingSystemType(propertyInfo).Name;
            }
            public static string GetNamespace(PropertyInfo propertyInfo)
            {
                return GetUnderlyingSystemType(propertyInfo).Namespace;
            }

            #endregion
        }
        public static class RefProperty
        {
            #region Infrasture

            public static string GetType(PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType.FullName;
            }

            #endregion

            #region Attribute

            public static List<CustomAttributeData> GetAllPropertyAttributes(PropertyInfo propertyInfo)
            {
                return propertyInfo
                   .CustomAttributes
                   .ToList();
            }
            public static CustomAttributeData GetClassAttributeByName(PropertyInfo propertyInfo, string attributeClassName)
            {
                return GetAllPropertyAttributes(propertyInfo)
                    .FirstOrDefault(e => e.AttributeType.Name == attributeClassName);
            }
            public static bool HasAttribute(PropertyInfo propertyInfo, string attributeClassName)
            {
                return GetClassAttributeByName(propertyInfo, attributeClassName) != null;
            }
            public static bool TryGetAttributeValue(PropertyInfo propertyInfo, string attributeClassName, out string value)
            {
                var customAttribute = GetClassAttributeByName(propertyInfo, attributeClassName);
                value = string.Empty;

                if (customAttribute == null || customAttribute.ConstructorArguments.Count <= 0)
                {
                    return false;
                }

                value = customAttribute.ConstructorArguments?[0].Value.ToString();
                return true;
            }

            #endregion
        }
    }
}

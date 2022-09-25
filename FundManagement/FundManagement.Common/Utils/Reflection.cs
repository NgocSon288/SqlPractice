using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FundManagement.Common.Utils
{
    public static class Reflection
    {
        /// <summary>
        /// Invode the Private | Instance method
        /// </summary>
        /// <param name="sourceMethod">The type of Class contains the Method that we want to Invoke at runtime</param>
        /// <param name="methodName">The name of the method we want to Invoke at runtime</param>
        /// <param name="instance">The current Intance will Invoke this method</param>
        /// <param name="genergicTypes">Array of Generic Type such as [T, P, K] in MethodABC<T, P, K></param>
        /// <param name="parameters">Array of data that are the parameters of this method</param>
        /// <returns></returns>
        public static object InvokePrivateMethod(Type sourceMethod, string methodName, object instance, Type[] genergicTypes, object[] parameters)
        { 
            MethodInfo method = sourceMethod.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo generic = method.MakeGenericMethod(genergicTypes);

            return generic.Invoke(instance, parameters);
        }

        /// <summary>
        /// Invode the Private | Static method
        /// </summary>
        /// <param name="sourceMethod">The type of Class contains the Method that we want to Invoke at runtime</param>
        /// <param name="methodName">The name of the method we want to Invoke at runtime</param> 
        /// <param name="genergicTypes">Array of Generic Type such as [T, P, K] in MethodABC<T, P, K></param>
        /// <param name="parameters">Array of data that are the parameters of this method</param>
        public static object InvokePrivateStaticMethod(Type sourceMethod, string methodName, Type[] genergicTypes, object[] parameters)
        { 
            MethodInfo method = sourceMethod.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo generic = method.MakeGenericMethod(genergicTypes);

            return generic.Invoke(null, parameters);
        }
   
        public static class RefClass
        {
            #region Infrastrucure 

            #endregion

            #region Attribute  

            /// <summary>
            /// Get CustomAttributeData base on the TEntity of the Class, and name of the Attribute such as TableAttribute
            /// </summary>
            /// <param name="entity">Specific Entity of class such as Member, Role...</param>
            /// <param name="attributeClassName">Name of Attribute such as TableAttribute</param>
            /// <returns></returns>
            private static CustomAttributeData GetClassAttributeByName<TEntity>(TEntity entity, string attributeClassName)
            {
                return GetClassAttributeByName(typeof(TEntity), attributeClassName);
            }

            /// <summary>
            /// Get CustomAttributeData base on the Type of the Class, and name of the Attribute such as TableAttribute
            /// </summary>
            /// <param name="type">Type of class such as Member, Role...</param>
            /// <param name="attributeClassName">Name of Attribute such as TableAttribute</param>
            /// <returns></returns>
            private static CustomAttributeData GetClassAttributeByName(Type type, string attributeClassName)
            {
                return type.CustomAttributes.FirstOrDefault(e => e.AttributeType.Name == attributeClassName);
            }

            /// <summary>
            /// Get the value of the attribute of the class such as TableAttribute... base on TEntity
            /// </summary>
            /// <param name="entity">The specific Entity of the class that we want to get the AttributeValue</param>
            /// <param name="attributeClassName">Name of the Class Attribute such as TableAttribute</param>
            /// <returns></returns>
            public static string GetAttributeValue<TEntity>(TEntity entity, string attributeClassName)
            {
                return GetAttributeValue(typeof(TEntity), attributeClassName);
            }

            /// <summary>
            /// Get the value of the attribute of the class such as TableAttribute... base on Type
            /// </summary>
            /// <param name="type">Type of the class that we want to get the AttributeValue</param>
            /// <param name="attributeClassName">Name of the Class Attribute such as TableAttribute</param>
            /// <returns></returns>
            public static string GetAttributeValue(Type type, string attributeClassName)
            {
                return GetClassAttributeByName(type, attributeClassName).ConstructorArguments[0].Value.ToString();
            }

            #endregion

            #region Defination


            /// <summary>
            /// Get FullNamespace = Namespace<T> + ClassName<T> base on the PropertyInfo that is DbSet<T>, from DbContext with "Type"
            /// </summary>
            /// <param name="propertyInfo">PropertyInfo if DbSet<T></param>
            public static string GetClassNameWithinNamespaceFromDbSet(PropertyInfo propertyInfo)
            {
                return propertyInfo
                    .DeclaringType
                    .GenericTypeArguments[0]
                    .FullName;
            }

            /// <summary>
            /// Get FullNamespace = Namespace<T> + ClassName<T> base on the PropertyInfo that is List<T>
            /// </summary>
            /// <param name="propertyInfo">PropertyInfo if List<T></param>
            /// <returns></returns>
            public static string GetClassNameWithinNamespaceFromList(PropertyInfo propertyInfo)
            {
                //     List<T>      List<T>      T                       namespaceof(T)   
                return propertyInfo.PropertyType.GenericTypeArguments[0].FullName;
            }

            /// <summary>
            /// Get FullNamespace = Namespace + ClassName base on specificing the TEntity when TEntity is a Class
            /// </summary>
            /// <typeparam name="TEntity">This is a specific Class such as Member, Role, Donation...</typeparam>
            /// <returns></returns>
            public static string GetClassNameWithinNamespace<TEntity>()
            {
                return typeof(TEntity).GetTypeInfo().FullName;
            }

            /// <summary>
            /// Get FullNamespace = Namespace + ClassName base on the type
            /// </summary>
            /// <param name="type">This is the Type of Entity such as Member, Role, Donation...</param>
            /// <returns></returns>
            public static string GetClassNameWithinNamespace(Type type)
            {
                return type.GetTypeInfo().FullName;
            }

            /// <summary>
            /// Get FullNamespace = Namespace + ClassName base on the PropertyInfo of Class
            /// </summary>
            /// <param name="propertyInfo">PropertyInfo of class</param>
            /// <returns></returns>
            public static string GetClassNameWithinNamespaceDirectly(PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType.FullName;
            }

            /// <summary>
            /// Get PropertyInfo of Property (P of TDestinationEntity) that have the TypeName equals TSourceName
            /// Example: TDestinationEntity is Role, TDestinationEntity is Member
            /// </summary>
            /// <typeparam name="TSourceEntity">Similar to Role</typeparam>
            /// <typeparam name="TDestinationEntity">Similar to Member that contains the MemberID having Member is the type</typeparam>
            /// <returns></returns>
            public static PropertyInfo GetPropertyInfoByPropertyNameOfTEntity<TSourceEntity, TDestinationEntity>()
            {
                var sourceName = typeof(TSourceEntity); 
                foreach (var prop in typeof(TDestinationEntity).GetProperties())
                {
                    if (sourceName.Name == prop.PropertyType.Name)
                    {
                        return  prop;
                    }
                }

                return null;
            }

            /// <summary>
            /// Get PropertyInfo of Property from the class
            /// </summary>
            /// <typeparam name="TEntity">The class contains Property</typeparam>
            /// <param name="propertyName">Name of Property</typeparam>
            /// <returns></returns>
            public static PropertyInfo GetPropertyInfoByPropertyName<TEntity>(string propertyName)
            {
                foreach (var prop in typeof(TEntity).GetProperties())
                {
                    if (propertyName == prop.Name)
                    {
                        return prop;
                    }
                }

                return null;
            }

            #endregion
        }
        public static class RefProperty
        {
            #region Infrasture

            /// <summary>
            /// Get name of the Type such as String, Int, DateTime base on the PropertyInfo of specific Property
            /// </summary>
            /// <param name="propertyInfo">PropertyInfo of specific Property such as String, Int, DateTime</param>
            /// <returns></returns>
            public static string GetType(PropertyInfo propertyInfo)
            {
                return propertyInfo.PropertyType.FullName;
            }

            /// <summary>
            /// Get PropertyInfo base on type of the TEntity and PropertyName of this TEntity
            /// </summary>
            /// <param name="type">Type of TEntity</param>
            /// <param name="propertyName">Name of Property such as AbcID, DefID</param>
            /// <returns></returns>
            public static PropertyInfo GetPropertyInfoByPropertyName(Type type, string propertyName)
            {
                foreach (var prop in type.GetProperties())
                {
                    if (prop.Name == propertyName)
                    {
                        return prop;
                    }
                }

                return null;
            }

            #endregion

            #region Attribute

            /// <summary>
            /// Get CustomAttributeData such as PrimaryKeyAttribute, NonMappingDatabaseAttribute of the PropertyInfo
            /// </summary>
            /// <param name="propertyInfo">PropertyInfo of Property such as ID, Name</param>
            /// <returns></returns>
            public static List<CustomAttributeData> GetAllPropertyAttributes(PropertyInfo propertyInfo)
            {
                return propertyInfo
                   .CustomAttributes
                   .ToList();
            }

            /// <summary>
            /// Get Specific CustomAttributeData such as PrimaryKeyAttribute, NonMappingDatabaseAttribute base on the name of its
            /// </summary>
            /// <param name="propertyInfo">PropertyInfo of Property such as ID, Name</param>
            /// <param name="attributeClassName">Name of Attribute such PrimaryKeyAttribute, NonMappingDatabaseAttribute</param>
            /// <returns></returns>
            public static CustomAttributeData GetClassAttributeByName(PropertyInfo propertyInfo, string attributeClassName)
            {
                return GetAllPropertyAttributes(propertyInfo)
                    .FirstOrDefault(e => e.AttributeType.Name == attributeClassName);
            }

            /// <summary>
            /// Check the specific attribute if it exists in the specifict PropertyInfo
            /// </summary>
            /// <param name="propertyInfo">PropertyInfo of Property such as ID, Name</param>
            /// <param name="attributeClassName">Name of attribute such as PrimaryKeyAttribute, NonMappingDatabaseAttribute...</param>
            /// <returns></returns>
            public static bool HasAttribute(PropertyInfo propertyInfo, string attributeClassName)
            {
                return GetClassAttributeByName(propertyInfo, attributeClassName) != null;
            }

            /// <summary>
            /// Either check the Specifict Attribute exists in the specific Property or return the value of this Attribute through out
            /// </summary>
            /// <param name="propertyInfo">PropertyInfo of Property such as ID, Name</param>
            /// <param name="attributeClassName">Name of attribute such as PrimaryKeyAttribute, NonMappingDatabaseAttribute...</param>
            /// <param name="value">Return value</param>
            /// <returns></returns>
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

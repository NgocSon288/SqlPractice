using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using FundManagement.Common.Utils;
using FundManagement.EntityFramework.DataModels;
using FundMangement.EntityFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FundMangement.EntityFramework.Utils
{
    public static class DbSetExtension
    {
        #region Extension Public
        public static DbSet<TEntity> Include<TEntity, TKey>(this DbSet<TEntity> entities, Expression<Func<TEntity, dynamic>> func)
        {
            dynamic body = func.Body;
            string includeName = body.Member.Name;

            if (String.IsNullOrEmpty(includeName))
            {
                return entities;
            }

            var specifictPropertyInfo = GetProertyInfoOfRelationShipInclude<TEntity>(includeName, out string foreignKeyIdName);
            var fullNamespace = "";
            if (string.IsNullOrEmpty(foreignKeyIdName))
            {
                fullNamespace = Reflection.RefClass.GetClassNameWithinNamespaceFromList(specifictPropertyInfo);
            }
            else
            {
                fullNamespace = Reflection.RefClass.GetClassNameWithinNamespaceDirectly(specifictPropertyInfo);
            }

            var type = Type.GetType(fullNamespace);

            return Reflection.InvokePrivateStaticMethod(
                typeof(DbSetExtension),
                nameof(DbSetExtension.IncludeWithoutDestinationGeneric),
                new Type[] { typeof(TEntity), typeof(TKey), type },
                new object[] { entities, specifictPropertyInfo, foreignKeyIdName }) as DbSet<TEntity>;
        }
        #endregion

        #region Infrasture
        private static DbSet<TEntity> IncludeWithoutDestinationGeneric<TEntity, TKey, TDestinationEntity>(DbSet<TEntity> entities, PropertyInfo specifictPropertyInfo, string foreignKeyIdName)
        {
            // Get all data from RelationShip collection
            var fullClassName = string.Empty;

            if (string.IsNullOrEmpty(foreignKeyIdName))
            {
                fullClassName = Reflection.RefClass.GetClassNameWithinNamespaceFromList(specifictPropertyInfo);
            }
            else
            {
                fullClassName = Reflection.RefClass.GetClassNameWithinNamespaceDirectly(specifictPropertyInfo);
            }

            var roleType = Type.GetType(fullClassName);
            var dbSetIncludeObject = DbContext.GetDataFromTableOfDatabase(roleType);
            var dbSetIncludeEntities = dbSetIncludeObject as DbSet<TDestinationEntity>;

            if (!string.IsNullOrEmpty(foreignKeyIdName))
            {
                // Get PropertyInfo of RelationShipID -> help to get data from entity
                var foreignKeyIdPropertyInfo = Reflection.RefProperty.GetPropertyInfoByPropertyName(typeof(TEntity), foreignKeyIdName);

                foreach (var item in entities)
                {
                    // Get value of RelationShipID from item
                    var roleIdValue = foreignKeyIdPropertyInfo.GetValue(item);

                    // Get RelationShipEntity from RelationShipCollection having ID equals RelationShipID
                    var role = dbSetIncludeEntities.FirstOrDefault(e =>
                    {
                        var eBase = e as BaseModel<TKey>;
                        return eBase.ID.Equals(roleIdValue);
                    });

                    // Set value for each entity
                    if (role != null)
                    {
                        specifictPropertyInfo.SetValue(item, role);
                    }
                }
            }
            else
            {
                var sourceType = typeof(TEntity);   /// Member

                // PropertyInfo from TDestination that have TEntity type (Base on type instead of Name)
                var tentityPropertyInfoFromDestination = Reflection.RefClass.GetPropertyInfoByPropertyNameOfTEntity<TEntity, TDestinationEntity>();

                // Get value of Attribute ForeignKey
                Reflection.RefProperty.TryGetAttributeValue(tentityPropertyInfoFromDestination, nameof(ForeignKeyAttribute), out string foreignKeyIdDestinatonName);

                // GetPropertyInfo of value of ForeignKey
                PropertyInfo foreignKeyIdDestinatonPropertyInfo = Reflection.RefClass.GetPropertyInfoByPropertyName<TDestinationEntity>(foreignKeyIdDestinatonName);

                foreach (var item in entities)
                {
                    var idItemPropertyInfo = Reflection.RefProperty.GetPropertyInfoByPropertyName(typeof(TEntity), nameof(BaseModel<TKey>.ID));
                    var idItemValue = idItemPropertyInfo.GetValue(item);

                    var resDestination = new DbSet<TDestinationEntity>();
                    resDestination.Type = typeof(TDestinationEntity);
                    resDestination.AddRange(dbSetIncludeEntities.Where(e => idItemValue.Equals(foreignKeyIdDestinatonPropertyInfo.GetValue(e))));

                    specifictPropertyInfo.SetValue(item, resDestination);
                }
            }

            return entities;
        }
        private static PropertyInfo GetProertyInfoOfRelationShipInclude<TEntity>(string includeName, out string foreignKeyIdName)
        {
            PropertyInfo specifictPropertyInfo = null;
            foreignKeyIdName = string.Empty;

            foreach (var prop in typeof(TEntity).GetProperties())
            {
                if (prop.Name != includeName)
                {
                    continue;
                }

                specifictPropertyInfo = prop;
                if (Reflection.RefProperty.TryGetAttributeValue(prop, nameof(ForeignKeyAttribute), out foreignKeyIdName))
                {
                    break;
                }
            }

            return specifictPropertyInfo;
        }
        #endregion
    }
}

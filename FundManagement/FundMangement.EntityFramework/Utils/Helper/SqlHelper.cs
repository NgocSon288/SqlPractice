using System.Collections.Generic;
using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using FundManagement.Common.Custom.Attribute;
using FundMangement.EntityFramework.Utils; 
using FundManagement.Common.Utils;
using FundManagement.Common.Models;

namespace FundManagement.EntityFramework.Utils.Helper
{
    public static class SqlHelper
    {
        public static string ConvertEntityToInsertSqlQuery<TEntity>(TEntity entity)
        {
            List<string> statementParams = new List<string>();

            foreach (var prop in entity.GetType().GetProperties())
            {  
                if(Reflection.RefProperty.HasAttribute(prop, nameof(NonMappingDatabaseAttribute)))
                {
                    continue;
                } 

                var type = Reflection.RefProperty.GetType(prop);
                var value = prop.GetValue(entity, null);

                statementParams.Add(ConvertEntityValueToSqlValue(type, value)); 
            }

            return string.Format(EFConstants.INSERT_STATEMENT, Reflection.RefClass.GetAttributeValue(entity, nameof(TableAttribute)), String.Join(',', statementParams));
        }

        public static string ConvertEntityToDeleteSqlQuery<TEntity, TKey>(TEntity entity, TKey id)
        {
            return string.Format(EFConstants.DELETE_STATEMENT, Reflection.RefClass.GetAttributeValue(entity, nameof(TableAttribute)), id);
        }

        public static string ConvertEntityToUpdateSqlQuery<TEntity, TKey>(TEntity entityUpdate, TEntity newEntity)
        {
            var bm = newEntity as BaseModel<TKey>;
            var id = bm.ID;
            List<string> statementParams = new List<string>();

            foreach (var propEn in entityUpdate.GetType().GetProperties())
            {
                var oldVal = propEn.GetValue(entityUpdate);
                var newVal = propEn.GetValue(newEntity);
                var type = Reflection.RefProperty.GetType(propEn);

                if (oldVal.Equals(newVal))
                    continue;

                propEn.SetValue(entityUpdate, newVal);
                 
                statementParams.Add(string.Format("{0}={1}", propEn.Name, ConvertEntityValueToSqlValue(type, newVal)));
            }

            if(statementParams.Count <= 0)
            {
                return null;
            }

            return string.Format(EFConstants.UPDATE_STATEMENT, Reflection.RefClass.GetAttributeValue(entityUpdate, nameof(TableAttribute)), String.Join(',', statementParams), id);
        }

        #region Private
        private static string ConvertEntityValueToSqlValue(string type, object value)
        {
            if (value == null)
            {
                return "null";
            }

            if (type.Contains("Int"))
                return value.ToString();

            if (type.Contains("String"))
                return string.Format("'{0}'", value.ToString());

            if (type.Contains("DateTime"))
                return string.Format("'{0}'", DateTime.Parse(value.ToString()));

            return null;
        }
        #endregion
    }
}

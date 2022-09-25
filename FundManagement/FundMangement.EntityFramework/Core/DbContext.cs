using System.Data.Common;
using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System.Reflection.PortableExecutable;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using FundMangement.EntityFramework.Utils;
using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Utils;
using System.IO;

namespace FundMangement.EntityFramework.Core
{
    public class DbContext
    {
        public delegate void OnDatabaseFetch();

        #region Property 

        private SqlStatement sqlStatement;
        private string sqlConnectionString;
        public OnDatabaseFetch OnFetch { get; set; }

        #endregion
        public DbContext(string sqlConnectionString)
        {
            _CurrentIntance = this;
            this.sqlConnectionString = sqlConnectionString;
            sqlStatement = new SqlStatement();

            Fetch();

        }

        #region Public Main
        public virtual bool Fetch()
        {
            sqlStatement.Clear();

            foreach (var prop in GetType().GetProperties())
            {
                var propertyInfo = prop.PropertyType.GetProperty(EFConstants.DBSET_NAME);

                if (propertyInfo == null)
                    continue; 

                var fullClassName = Reflection.RefClass.GetClassNameWithinNamespaceFromDbSet(propertyInfo);
                var type = Type.GetType(fullClassName);
                var result = DbContext.GetDataFromTableOfDatabase(type);

                prop.SetValue(this, result, null);
            }

            OnFetch?.Invoke();
            return true;
        }

        public virtual IEnumerable<T> ExecuteStoreQuery<T>(string storeProcedureName, Dictionary<string, string> parameters)
        {
            return ExecuteQuery<T>(storeProcedureName, parameters, true);
        }
        public IEnumerable<T> SqlQuery<T>(string query)
        {
            return ExecuteQuery<T>(query);
        }

        #endregion

        #region Public UnitOfWork

        public virtual void Rollback()
        {
            // Fetch previous data
            Fetch();
        }
        public virtual void SaveChange()
        {
            SqlTransaction trans = null;

            try
            {
                var connection = new SqlConnection(sqlConnectionString);
                connection.Open();

                trans = connection.BeginTransaction();

                foreach (var commandString in sqlStatement)
                {
                    SqlCommand command = new SqlCommand(commandString, connection, trans);
                    command.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans?.Rollback();
                throw new Exception(ex.Message);
            }

            // Call rollback to fetch data
            Fetch();
        }

        #endregion

        #region Public Related
        public void AddSqlStatement(string statement)
        {
            sqlStatement.Add(statement);
        }

        #endregion

        #region Private 
        private DbSet<T> LoadDataFromTable<T>(string tableName)
        {
            DbSet<T> list = new DbSet<T>() { Type = typeof(T) };
            var connection = new SqlConnection(sqlConnectionString);
            connection.Open();

            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = string.Format(EFConstants.SELECT_STATEMENT, tableName);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var obj = Activator.CreateInstance(typeof(T));

                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        // Get Attribute of prop, if contain NonMapping => 
                        if (Reflection.RefProperty.HasAttribute(prop, nameof(NonMappingMemoryAttribute)))
                        {
                            continue;
                        }

                        // Set value for Complex type: Member, Role, Consome...
                        // Ignore mapping, map in Include extension method
                        var name = prop.PropertyType.Name;
                        if (!name.Contains("Int")
                            && !name.Contains("String")
                            && !name.Contains("DateTime")
                            && !name.Contains("Nullable"))
                        {
                            continue;
                        }

                        // Set value for Basic type: Int, String, DateTime...
                        var propertyDatabaseName = "";
                        if (!Reflection.RefProperty.TryGetAttributeValue(prop, nameof(ColumnDisplayNameAttribute), out propertyDatabaseName))
                        {
                            propertyDatabaseName = prop.Name;
                        }

                        if (!Equals(reader[propertyDatabaseName], DBNull.Value))
                        {
                            prop.SetValue(obj, reader[propertyDatabaseName], null);
                        }
                    }
                    list.Add((T)obj);
                }
            }

            connection.Close();
            return list;
        }
        private IEnumerable<T> ExecuteQuery<T>(string queryString, Dictionary<string, string> parameters = null, bool isStoreProcedure = false)
        {
            var connection = new SqlConnection(sqlConnectionString);
            connection.Open();
            SqlCommand connand = new SqlCommand(queryString, connection);

            if (isStoreProcedure)
            {
                connand.CommandType = CommandType.StoredProcedure;
                foreach (var (key, val) in parameters)
                {
                    connand.Parameters.Add(new SqlParameter($"@{key}", val));
                }
            }

            using (SqlDataReader reader = connand.ExecuteReader())
            {
                foreach (var item in ConvertDataFromDataReaderToDbset<T>(reader))
                {
                    yield return item;
                }
            }
            connection.Close();
        }
        private IEnumerable<T> ConvertDataFromDataReaderToDbset<T>(DbDataReader reader)
        {
            while (reader.Read())
            {
                var obj = Activator.CreateInstance(typeof(T));

                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    // Check the column is exists
                    if (!reader.GetColumnSchema().Any(e => e.ColumnName == prop.Name))
                    {
                        continue;
                    }

                    if (!Equals(reader[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, reader[prop.Name], null);
                    }
                }

                yield return (T)obj;
            }
        }

        #endregion

        #region Ensure only one intance, use to support DbSet extension for making Include() method

        protected static DbContext _CurrentIntance;

        #endregion

        #region Global Access 
        public static object GetDataFromTableOfDatabase(Type classType)
        { 
            var tableName = Reflection.RefClass.GetAttributeValue(classType, nameof(TableAttribute));

            return Reflection.InvokePrivateMethod(typeof(DbContext), nameof(LoadDataFromTable), DbContext._CurrentIntance, new Type[] { classType }, new object[] { tableName });
        } 


        #endregion
    }
}
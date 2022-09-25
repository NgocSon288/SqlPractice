using FundManagement.Common.Models;
using FundManagement.Common.Utils;
using FundManagement.EntityFramework.DataModels;
using FundManagement.EntityFramework.Utils.Helper;
using FundMangement.EntityFramework.Core;
using FundMangement.EntityFramework.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FundManagement.DataAccess.Infrastructure
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    {
        private AppDbContext dataContext;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        public AppDbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.GetDbContext()); }
        }
        public DbSet<TEntity> DbSet { get; set; }
        public BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;

            DbContext.OnFetch += Fetch;

            if (DbSet == null)
            {
                Fetch();
            }
        }  

        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, dynamic>> includeFunc = null, Func<TEntity, bool> condition = null)
        {
            if (includeFunc !=null)
            {
                DbSet = DbSet.Include<TEntity, TKey>(includeFunc);
            }

            if (condition == null)
            {
                return DbSet;
            }

            return DbSet.Where(condition);
        }
        public virtual TEntity FirstOrDefault(Func<TEntity, bool> condition = null)
        {
            return DbSet.FirstOrDefault(condition);
        }
        public virtual TEntity GetById(TKey id)
        {
            return DbSet.FirstOrDefault(ent =>
            {
                var bm = ent as BaseModel<TKey>;

                return bm.ID.Equals(id);
            });
        }

        #region Regarding Sql Statement
        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);

            // Build SqlCommand
            DbContext.AddSqlStatement(SqlHelper.ConvertEntityToInsertSqlQuery(entity));
        }
        public virtual void Update(TKey id, TEntity newEntity)
        {
            var entityBase = newEntity as BaseModel<TKey>;
            var entityBaseId = entityBase.ID;
            var entityUpdate = GetById(entityBaseId);

            if (entityUpdate == null || !entityBaseId.Equals(id))
                throw new Exception("The entity is not exists");

            // Build SqlCommand
            var statement = SqlHelper.ConvertEntityToUpdateSqlQuery<TEntity, TKey>(entityUpdate, newEntity);
            if (statement != null)
            {
                DbContext.AddSqlStatement(statement);
            }

            // Update data entity
            foreach (var propEn in entityUpdate.GetType().GetProperties())
            {
                var oldVal = propEn.GetValue(entityUpdate);
                var newVal = propEn.GetValue(newEntity);

                if (oldVal == newVal)
                    continue;

                propEn.SetValue(entityUpdate, newVal);
            }
        }
        public virtual void RemoveById(TKey id)
        {
            var entityRemove = GetById(id);

            if (entityRemove == null)
                throw new Exception("The entity is not exists");

            DbSet.Remove(entityRemove);

            // Build SqlCommand
            DbContext.AddSqlStatement(SqlHelper.ConvertEntityToDeleteSqlQuery(entityRemove, id));
        }
        public virtual TEntity GetLatestEntity()
        {
            return DbSet.LastOrDefault();
        }
        #endregion

        #region Private

        private void Fetch()
        {
            var specificProp = GetSpecifictPropertyInfoBySetOfDbSet();
            DbSet = specificProp.GetValue(DbContext) as DbSet<TEntity>; 
        }

        /// <summary>
        /// Get specific DbSet in set of DbSet of ApplicationDbConext
        /// </summary>
        /// <returns></returns>
        private PropertyInfo GetSpecifictPropertyInfoBySetOfDbSet()
        {
            var entityNamespace = Reflection.RefClass.GetClassNameWithinNamespace<TEntity>();

            foreach (var prop in DbContext.GetType().GetProperties())
            {
                var propertyInfo = prop.PropertyType.GetProperty(EFConstants.DBSET_NAME);
                var fullNamespace = Reflection.RefClass.GetClassNameWithinNamespaceFromDbSet(propertyInfo);

                if (fullNamespace == entityNamespace)
                {
                    return prop;
                }
            }

            return null;
        }

        #endregion
    }
}

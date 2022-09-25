using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public interface IBaseService<TEntity, TKey>
    {
        TEntity Insert(TEntity entity);
        IEnumerable<T> ExecuteStoreQuery<T>(string storeProcedureName, Dictionary<string, string> parameters);
        IEnumerable<TEntity> GetAll(string includeName = null, Func<TEntity, bool> condition = null);
        TEntity GetById(TKey id);
        void RemoveById(TKey id);
        //void Rollback();
        //void SaveChange();
        IEnumerable<T> SqlQuery<T>(string query);
        void Update(TKey id, TEntity newEntity);
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FundManagement.DataAccess.Infrastructure
{
    public interface IBaseRepository<TEntity, TKey>
    {
        void Add(TEntity entity); 
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, dynamic>> includeFunc = null, Func<TEntity, bool> condition = null);
        TEntity GetById(TKey id);
        TEntity GetLatestEntity();
        void RemoveById(TKey id);
        void Update(TKey id, TEntity newEntity);
    }
}
using System;
using System.Collections.Generic;

namespace FundManagement.DataAccess.Infrastructure
{
    public interface IBaseRepository<TEntity, TKey>
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> condition = null);
        TEntity GetById(TKey id);
        TEntity GetLatestEntity();
        void RemoveById(TKey id);
        void Update(TKey id, TEntity newEntity);
    }
}
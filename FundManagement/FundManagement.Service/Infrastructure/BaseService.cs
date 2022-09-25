using System.Collections.Generic;
using System;
using FundManagement.DataAccess.Infrastructure;
using FundManagement.DataAccess;

namespace FundManagement.Service.Infrastructure
{
    public abstract class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey>
    {
        protected readonly IBaseRepository<TEntity, TKey> _repo;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IDbFactory _dbFactory;
        protected AppDbContext appDbContext;
        public BaseService(IBaseRepository<TEntity, TKey> repo, IUnitOfWork unitOfWork, IDbFactory dbFactory)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _dbFactory = dbFactory;

            appDbContext = _dbFactory.GetDbContext();
        }
        public virtual TEntity Insert(TEntity entity)
        {
            _repo.Add(entity);

            try
            {
                _unitOfWork.Commit();

                return _repo.GetLatestEntity();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }
        public virtual IEnumerable<TEntity> GetAll(string includeName, Func<TEntity, bool> condition = null) => _repo.GetAll(includeName, condition);
        public virtual TEntity GetById(TKey id) => _repo.GetById(id);
        public virtual void RemoveById(TKey id)
        {
            try
            {
                _repo.RemoveById(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public virtual void Update(TKey id, TEntity newEntity)
        {
            try
            {
                _repo.Update(id, newEntity);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }
        }
        public virtual IEnumerable<T> ExecuteStoreQuery<T>(string storeProcedureName, Dictionary<string, string> parameters)
        {
            return appDbContext.ExecuteStoreQuery<T>(storeProcedureName, parameters);
        }
        public virtual IEnumerable<T> SqlQuery<T>(string query)
        {
            return appDbContext.SqlQuery<T>(query);
        }
    }
}

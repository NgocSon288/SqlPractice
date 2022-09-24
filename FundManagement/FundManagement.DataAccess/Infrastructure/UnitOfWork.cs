using System;

namespace FundManagement.DataAccess.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private AppDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public AppDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.GetDbContext()); }
        }

        /// <summary>
        /// Commit the changes to the database
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Commit()
        {
            try
            {
                DbContext.SaveChange();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Rollback to previous data, clear all the memory statement
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Rollback()
        {
            try
            {
                DbContext.Rollback();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

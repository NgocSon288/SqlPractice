namespace FundManagement.DataAccess.Infrastructure
{
    public interface IUnitOfWork
    {
        AppDbContext DbContext { get; }

        void Commit();
        void Rollback();
    }
}
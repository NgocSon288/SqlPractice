namespace FundManagement.DataAccess.Infrastructure
{
    public interface IDbFactory
    {
        string sqlConnectionString { get; }

        AppDbContext GetDbContext();
    }
}
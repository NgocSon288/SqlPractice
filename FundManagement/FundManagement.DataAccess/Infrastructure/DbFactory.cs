using FundManagement.DataAccess;

namespace FundManagement.DataAccess.Infrastructure
{
    public class DbFactory : IDbFactory
    {
        private AppDbContext dbContext;

        public DbFactory(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public string sqlConnectionString { get; }

        public AppDbContext GetDbContext()
        {
            return dbContext ?? (dbContext = new AppDbContext(sqlConnectionString));
        }
    }
}

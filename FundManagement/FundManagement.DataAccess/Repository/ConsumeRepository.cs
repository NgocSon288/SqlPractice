using FundManagement.EntityFramework.DataModels;
using FundManagement.DataAccess.Infrastructure;

namespace FundManagement.DataAccess.Repository
{
    public class ConsumeRepository : BaseRepository<Consume, int>, IConsumeRepository
    {
        public ConsumeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

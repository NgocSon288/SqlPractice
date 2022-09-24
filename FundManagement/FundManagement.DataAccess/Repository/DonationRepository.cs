using FundManagement.EntityFramework.DataModels;
using FundManagement.DataAccess.Infrastructure;

namespace FundManagement.DataAccess.Repository
{
    public class DonationRepository : BaseRepository<Donation, int>, IDonationRepository
    {
        public DonationRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

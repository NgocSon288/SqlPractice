using FundManagement.EntityFramework.DataModels;
using FundManagement.DataAccess.Infrastructure; 

namespace FundManagement.DataAccess.Repository
{
    public interface IDonationRepository : IBaseRepository<Donation, int>
    {

    }
}

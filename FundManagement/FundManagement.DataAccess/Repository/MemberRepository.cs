using FundManagement.EntityFramework.DataModels;
using FundManagement.DataAccess.Infrastructure;

namespace FundManagement.DataAccess.Repository
{
    public class MemberRepository : BaseRepository<Member, int>, IMemberRepository
    {
        public MemberRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

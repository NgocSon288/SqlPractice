using FundManagement.EntityFramework.DataModels;
using FundManagement.DataAccess.Infrastructure;
using System.Linq;
using FundMangement.EntityFramework.Utils;

namespace FundManagement.DataAccess.Repository
{
    public class MemberRepository : BaseRepository<Member, int>, IMemberRepository
    {
        public MemberRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Member GetMemberByCreditial(string username, string password)
        {
            DbSet.Include<Member, int>(e => e.Role);
            return DbSet.FirstOrDefault(e => e.Username == username && e.Password == password);
        }
    }
}

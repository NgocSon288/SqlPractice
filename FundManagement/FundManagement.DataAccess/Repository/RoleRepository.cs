using FundManagement.EntityFramework.DataModels;
using FundManagement.DataAccess.Infrastructure;

namespace FundManagement.DataAccess.Repository
{
    public class RoleRepository : BaseRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

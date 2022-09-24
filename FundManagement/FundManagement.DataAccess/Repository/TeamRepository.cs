using FundManagement.EntityFramework.DataModels;
using FundManagement.DataAccess.Infrastructure;

namespace FundManagement.DataAccess.Repository
{
    public class TeamRepository : BaseRepository<Team, int>, ITeamRepository
    {
        public TeamRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}

using FundManagement.DataAccess.Infrastructure;
using FundManagement.DataAccess.Repository;
using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure;
using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public class TeamService : BaseService<Team, int>, ITeamService
    {  
        public TeamService(ITeamRepository TeamRepository, IUnitOfWork unitOfWork) : base(TeamRepository, unitOfWork)
        { 
        }
    }
}

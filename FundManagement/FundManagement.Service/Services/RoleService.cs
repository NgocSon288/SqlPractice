using FundManagement.DataAccess.Infrastructure;
using FundManagement.DataAccess.Repository;
using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure;
using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public class RoleService : BaseService<Role, int>, IRoleService
    { 
        public RoleService(IRoleRepository RoleRepository, IUnitOfWork unitOfWork, IDbFactory dbFactory) : base(RoleRepository, unitOfWork, dbFactory)
        { 
        }
    }
}

using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure; 
using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public interface IRoleService : IBaseService<Role, int>
    {
    }
}
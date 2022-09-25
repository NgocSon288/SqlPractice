using FundManagement.EntityFramework.DataModels;
using FundManagement.Model;
using FundManagement.ViewModel.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundManagement.Authentication
{
    public interface IJWTManagerService
    {
        Tokens Authenticate(MemberLoginInput input);
    }
}

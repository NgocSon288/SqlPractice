using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure;
using FundManagement.ViewModel.Member;
using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public interface IAuthenticationService
    {
        MemberLoginOutput Login(MemberLoginInput input);
    }
}
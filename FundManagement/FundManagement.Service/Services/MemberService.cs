using FundManagement.DataAccess.Infrastructure;
using FundManagement.DataAccess.Repository;
using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure;
using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public class MemberService : BaseService<Member, int>, IMemberService
    {  
        public MemberService(IMemberRepository memberRepository, IUnitOfWork unitOfWork) : base(memberRepository, unitOfWork)
        { 
        }
    }
}

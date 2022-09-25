using FundManagement.DataAccess.Infrastructure;
using FundManagement.DataAccess.Repository;
using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure;
using FundManagement.ViewModel.Member;
using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbFactory _dbFactory;
        private readonly IMemberRepository _memberRepository;

        public AuthenticationService(IUnitOfWork unitOfWork, IDbFactory dbFactory, IMemberRepository memberRepository)
        {
            _unitOfWork = unitOfWork;
            _dbFactory = dbFactory;
            _memberRepository = memberRepository;
        }

        public MemberLoginOutput Login(MemberLoginInput input)
        {
            var member = _memberRepository.GetMemberByCreditial(input.Username, input.Password);

            if (member == null)
            {
                return null;
            }

            return new MemberLoginOutput()
            {
                Username = member.Username,
                Role = member.Role.Name
            };
        }
    }
}

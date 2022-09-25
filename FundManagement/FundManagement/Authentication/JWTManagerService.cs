using FundManagement.Common.Utils;
using FundManagement.EntityFramework.DataModels;
using FundManagement.Model;
using FundManagement.Service.Infrastructure;
using FundManagement.ViewModel.Member;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundManagement.Authentication
{
    public class JWTManagerService : IJWTManagerService
    {
        private readonly IConfiguration _iconfiguration;
        private readonly IAuthenticationService _authenticationService;
        public JWTManagerService(IConfiguration iconfiguration, IAuthenticationService authenticationService)
        {
            this._iconfiguration = iconfiguration;
            this._authenticationService = authenticationService;
        }
        public Tokens Authenticate(MemberLoginInput input)
        {
            var member = _authenticationService.Login(input);
            if (member == null)
            {
                return null;
            }
             
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration[AppConstants.JWT.KEY]);
             
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, member.Username),
                    new Claim(ClaimTypes.Role, member.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}

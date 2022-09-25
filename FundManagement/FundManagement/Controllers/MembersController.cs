using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using FundManagement.Service;
using FundManagement.EntityFramework.DataModels;
using FundManagement.ViewModel.Dto;
using FundManagement.Service.Infrastructure;
using FundManagement.Common.Api.Utils;

namespace FundManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : BaseController<Member, int>
    {  
        public MembersController(ILogger<MembersController> logger, IMemberService memberService) : base(logger, memberService)
        { 
        }

        [HttpGet("ByTeam/{teamId}")]
        public ApiResult<IEnumerable<Member>> GetAllByTeamID(int? teamId)
        {
            return new ApiResult<IEnumerable<Member>>(_service.GetAll(x => x.TeamID == teamId));
        }
    }
}

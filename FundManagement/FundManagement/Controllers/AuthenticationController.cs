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
using FundManagement.Authentication;
using Microsoft.AspNetCore.Authorization;
using FundManagement.Common.Api.Utils;
using FundManagement.Model;
using FundManagement.ViewModel.Member;

namespace FundManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<ConsumesController> _logger;
        private readonly IJWTManagerService _jWTManager;

        public AuthenticationController(ILogger<ConsumesController> logger, IJWTManagerService jWTManager)
        {
            this._logger = logger;
            this._jWTManager = jWTManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ApiResult<Tokens> Authenticate(MemberLoginInput member)
        {
            var token = _jWTManager.Authenticate(member);

            if (token == null)
            {
                return new ApiResult<Tokens>(false, "Unauthorized");
            }

            return new ApiResult<Tokens>(token);
        }
    }
}

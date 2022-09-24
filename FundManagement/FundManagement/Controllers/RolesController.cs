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
using FundManagement.Service.Infrastructure;

namespace FundManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, int>
    {  
        public RolesController(ILogger<RolesController> logger, IRoleService roleService) : base(logger, roleService)
        { 
        } 
    }
}

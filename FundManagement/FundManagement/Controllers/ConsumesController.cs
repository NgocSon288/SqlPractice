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

namespace FundManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumesController : BaseController<Consume, int>
    {  
        public ConsumesController(ILogger<ConsumesController> logger, IConsumeService consumeService) : base(logger, consumeService)
        {  
        }
    }
}

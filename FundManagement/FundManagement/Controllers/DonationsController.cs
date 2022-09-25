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
    public class DonationsController : BaseController<Donation, int>
    { 
        public DonationsController(ILogger<DonationsController> logger, IDonationService donationService) : base(logger, donationService)
        {
        }

        [HttpGet]
        public override ApiResult<IEnumerable<Donation>> GetAll()
        {
            _service.GetAll(e => e.Member);

            return new ApiResult<IEnumerable<Donation>>(_service.GetAll());

        }
    }
}

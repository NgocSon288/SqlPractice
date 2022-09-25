using FundManagement.Common.Api.Utils;
using FundManagement.Service.Infrastructure;
using FundManagement.ViewModel.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FundManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        protected readonly ILogger<ReportController> _logger;
        protected readonly IReportService _service;

        public ReportController(ILogger<ReportController> logger, IReportService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetIncomeByMonthAndTeam")]
        public ApiResult<decimal> GetIncomeByMonthAndTeam([FromQuery]IncomOutcomeByMonthAndTeamInput input)
        {
            return new ApiResult<decimal>(_service.GetIncomeByMonthAndTeam(input).TotalMoney);
        }

        [HttpGet("GetOutcomeByMonthAndTeam")]
        public ApiResult<decimal> GetOutcomeByMonthAndTeam([FromQuery]IncomOutcomeByMonthAndTeamInput input)
        {
            return new ApiResult<decimal>(_service.GetOutcomeByMonthAndTeam(input).TotalMoney);
        }

        [HttpGet("GetCurrentMoneyByTeam")]
        public ApiResult<decimal> GetCurrentMoneyByTeam(int teamID)
        {
            return new ApiResult<decimal>(_service.GetCurrentMoneyByTeam(teamID).TotalMoney);
        }
    }
}

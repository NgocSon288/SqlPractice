using FundManagement.ViewModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagement.Service.Infrastructure
{
    public interface IReportService
    {
        MoneyOutput GetIncomeByMonthAndTeam(IncomOutcomeByMonthAndTeamInput input);
        MoneyOutput GetOutcomeByMonthAndTeam(IncomOutcomeByMonthAndTeamInput input);
        MoneyOutput GetCurrentMoneyByTeam(int teamID);

    }
}

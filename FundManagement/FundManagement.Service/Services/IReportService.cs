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
        MoneyOutput GetIncomeByMonthAndTeam(MonthAndTeamInput input);
        MoneyOutput GetOutcomeByMonthAndTeam(MonthAndTeamInput input);
        MoneyOutput GetCurrentMoneyByTeam(int teamID);
        IEnumerable<RankingDonatorOutput> GetRankingDonatorsByMonthAndTeam(MonthAndTeamInput input);

    }
}

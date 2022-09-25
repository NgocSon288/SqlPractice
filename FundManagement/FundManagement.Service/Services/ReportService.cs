using FundManagement.Common.Utils;
using FundManagement.DataAccess;
using FundManagement.DataAccess.Infrastructure;
using FundManagement.ViewModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundManagement.Service.Infrastructure
{
    public class ReportService: IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private AppDbContext appDbContext;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            appDbContext = _unitOfWork.DbContext;
        }

        public MoneyOutput GetCurrentMoneyByTeam(int teamID)
        {
            var parameters = Helper.ConvertObjectToDictionary(teamID, nameof(teamID));
            return appDbContext.ExecuteStoreQuery<MoneyOutput>(AppConstants.StoreProdedureConstants.GET_CURRENT_MONEY_BY_TEAM, parameters).FirstOrDefault();
        }

        public MoneyOutput GetIncomeByMonthAndTeam(MonthAndTeamInput input)
        {
            var parameters = Helper.ConvertObjectToDictionary(input);
            return appDbContext.ExecuteStoreQuery<MoneyOutput>(AppConstants.StoreProdedureConstants.GET_INCOME_BY_MONTH_AND_TEAM, parameters).FirstOrDefault();
        }

        public MoneyOutput GetOutcomeByMonthAndTeam(MonthAndTeamInput input)
        {
            var parameters = Helper.ConvertObjectToDictionary(input);
            return appDbContext.ExecuteStoreQuery<MoneyOutput>(AppConstants.StoreProdedureConstants.GET_OUTCOME_BY_MONTH_AND_TEAM, parameters).FirstOrDefault();
        }

        public IEnumerable<RankingDonatorOutput> GetRankingDonatorsByMonthAndTeam(MonthAndTeamInput input)
        {
            var parameters = Helper.ConvertObjectToDictionary(input);
            return appDbContext.ExecuteStoreQuery<RankingDonatorOutput>(AppConstants.StoreProdedureConstants.GET_RANKING_DONATORS_BY_MONTH_AND_TEAM, parameters);
        }
    }
}

using FundManagement.DataAccess.Infrastructure;
using FundManagement.DataAccess.Repository;
using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure;
using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public class DonationService : BaseService<Donation, int>, IDonationService
    {
        public DonationService(IDonationRepository DonationRepository, IUnitOfWork unitOfWork, IDbFactory dbFactory) : base(DonationRepository, unitOfWork, dbFactory)
        {

        }
    }
}

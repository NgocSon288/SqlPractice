using FundManagement.DataAccess.Infrastructure;
using FundManagement.DataAccess.Repository;
using FundManagement.EntityFramework.DataModels;
using FundManagement.Service.Infrastructure; 
using System;
using System.Collections.Generic;

namespace FundManagement.Service.Infrastructure
{
    public class ConsumeService : BaseService<Consume, int>, IConsumeService
    {  
        public ConsumeService(IConsumeRepository ConsumeRepository, IUnitOfWork unitOfWork) : base(ConsumeRepository, unitOfWork)
        {

        }
    }
}

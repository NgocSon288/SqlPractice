using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;
using System.Collections.Generic;

namespace FundManagement.EntityFramework.DataModels
{
    [Table("Teams")]
    public class Team : BaseModel<int>
    { 
        public string Name { get; set; }

        public List<Donation> Donations { get; set; }
    }
}

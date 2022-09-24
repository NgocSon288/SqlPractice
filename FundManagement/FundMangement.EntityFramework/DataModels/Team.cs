using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;

namespace FundManagement.EntityFramework.DataModels
{
    [Table("Teams")]
    public class Team : BaseModel<int>
    { 
        public string Name { get; set; }
    }
}

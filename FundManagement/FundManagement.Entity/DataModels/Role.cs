using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;

namespace FundManagement.Entity.DataModels
{
    [Table("Roles")]
    public class Role : BaseModel<int>
    { 
        public string Name { get; set; }
    }
}

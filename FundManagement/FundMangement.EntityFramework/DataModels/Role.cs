using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;

namespace FundManagement.EntityFramework.DataModels
{
    [Table("Roles")]
    public class Role : BaseModel<int>
    { 
        public string Name { get; set; }
    }
}

using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;

namespace FundManagement.Entity.DataModels
{
    [Table("Consumes")]
    public class Consume : BaseModel<int>
    { 
        public string Comment { get; set; }
        public decimal Money { get; set; }
        public DateTime Date { get; set; } 
    }
}

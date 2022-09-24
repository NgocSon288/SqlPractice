using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;

namespace FundManagement.Entity.DataModels
{
    [Table("Donations")]
    public class Donation : BaseModel<int>
    { 
        public DateTime Date { get; set; }
        public decimal Money { get; set; }
        public string Comment { get; set; }
        public int? MemberID { get; set; }
    }
}

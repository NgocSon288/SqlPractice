using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;

namespace FundManagement.EntityFramework.DataModels
{
    [Table("Donations")]
    public class Donation : BaseModel<int>
    {
        public DateTime Date { get; set; }
        public decimal Money { get; set; }
        public string Comment { get; set; }
        public int? MemberID { get; set; }

        [ForeignKey("MemberID")]
        public Member Member { get; set; }
    }
}

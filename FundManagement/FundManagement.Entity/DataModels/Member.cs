using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;

namespace FundManagement.Entity.DataModels
{
    [Table("Members")]
    public class Member : BaseModel<int>
    { 
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? TeamID { get; set; }
        public int? RoleID { get; set; }
    }
}

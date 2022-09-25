using FundManagement.Common.Custom.Attribute;
using FundManagement.Common.Models;
using System;
using System.Collections.Generic;

namespace FundManagement.EntityFramework.DataModels
{
    [Table("Members")]
    public class Member : BaseModel<int>
    {
        [ColumnDisplayName("Name")]
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [ColumnDisplayName("TeamID")]
        public int? TeamID { get; set; }

        [ColumnDisplayName("RoleID")]
        public int? RoleID { get; set; }

        [ForeignKey("TeamID")]
        public Team Team { get; set; }

        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        [NonMappingMemory]
        public string Introduce
        {
            get
            {
                return String.Format("My fullname is {0}, I was born on {1}", FullName, BirthDay.ToShortDateString());
            }
        }

        public List<Donation> Donations { get; set; }
    }
}

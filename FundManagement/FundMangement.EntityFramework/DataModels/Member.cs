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
        public int? TeamIDaaa { get; set; }
        
        [ColumnDisplayName("RoleID")]
        public int? RoleIDaaa { get; set; }

        [ForeignKey("TeamIDaaa")]
        public Team Teamxxxaaa { get; set; }

        [ForeignKey("RoleIDaaa")]
        public Role Rolexxx { get; set; }

        [NonMappingMemory]
        public string Introduce
        {
            get
            {
                return String.Format("My fullname is {0}, I was born on {1}", FullName, BirthDay.ToShortDateString());
            }
        }

        public List<Donation> Donationsxxxxx { get; set; } 
    }
}

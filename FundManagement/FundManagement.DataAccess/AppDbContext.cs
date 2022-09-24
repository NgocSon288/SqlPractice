using FundManagement.EntityFramework.DataModels; 
using FundMangement.EntityFramework.Core;
using System;
using System.Collections.Generic;

namespace FundManagement.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string sqlConnectionString) : base(sqlConnectionString)
        {
        }

        public DbSet<Consume> Consumes { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}

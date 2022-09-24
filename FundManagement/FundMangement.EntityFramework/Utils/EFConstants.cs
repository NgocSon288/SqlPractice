using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundMangement.EntityFramework.Core;

namespace FundMangement.EntityFramework.Utils
{
    public static class EFConstants
    {
        public const string DBSET_NAME = nameof(DbSet<object>.Type);
        public const string SELECT_STATEMENT = "SELECT * FROM {0}";
        public const string INSERT_STATEMENT = "INSERT INTO {0} VALUES ({1})";
        public const string UPDATE_STATEMENT = "UPDATE {0} SET {1} WHERE ID={2}";
        public const string DELETE_STATEMENT = "DELETE FROM {0} WHERE ID={1}";
    }
}

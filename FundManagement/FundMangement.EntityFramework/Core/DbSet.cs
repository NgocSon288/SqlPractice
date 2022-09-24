using System.Collections.Generic;
using System;

namespace FundMangement.EntityFramework.Core
{
    public class DbSet<T> : List<T>
    {
        public Type Type { get; set; } = typeof(T);
    }
}

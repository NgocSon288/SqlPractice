using System;

namespace FundManagement.Common.Custom.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NonMappingDatabaseAttribute : System.Attribute
    {
        public NonMappingDatabaseAttribute()
        {
        }
    }
}

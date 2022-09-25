using System;

namespace FundManagement.Common.Custom.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NonMappingMemoryAttribute : System.Attribute
    {
        public NonMappingMemoryAttribute()
        {
        }
    }
}

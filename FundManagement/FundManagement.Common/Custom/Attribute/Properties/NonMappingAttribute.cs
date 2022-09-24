using System;

namespace FundManagement.Common.Custom.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NonMappingAttribute : System.Attribute
    {
        public NonMappingAttribute()
        {
        }
    }
}

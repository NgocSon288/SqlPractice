using System;

namespace FundManagement.Common.Custom.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyDatabaseNameAttribute : System.Attribute
    {
        private string name;
        public PropertyDatabaseNameAttribute(string name)
        {
            this.name = name;
        }
    }
}

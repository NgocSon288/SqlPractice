using System;

namespace FundManagement.Common.Custom.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnDisplayNameAttribute : System.Attribute
    {
        private string name;
        public ColumnDisplayNameAttribute(string name)
        {
            this.name = name;
        }
    }
}

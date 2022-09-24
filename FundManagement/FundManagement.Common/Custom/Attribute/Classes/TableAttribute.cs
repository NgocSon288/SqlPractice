using System;

namespace FundManagement.Common.Custom.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : System.Attribute
    {
        private string name;

        public TableAttribute(string name)
        {
            this.name = name;
        }
    }
}

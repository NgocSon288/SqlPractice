using System;

namespace FundManagement.Common.Custom.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : System.Attribute
    {
        private string primaryKeyId;
        public PrimaryKeyAttribute(string primaryKeyId)
        {
            this.primaryKeyId = primaryKeyId;
        }
    }
}

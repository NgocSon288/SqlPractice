using System;

namespace FundManagement.Common.Custom.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : System.Attribute
    {
        private string foreignKeyId;
        public ForeignKeyAttribute(string foreignKeyId)
        {
            this.foreignKeyId = foreignKeyId;
        }
    }
}

using FundManagement.Common.Custom.Attribute; 

namespace FundManagement.Common.Models
{
    public class BaseModel<TKey>
    {
        [PrimaryKey("ID")]
        [NonMappingDatabase]
        public virtual TKey ID { get; set; }
    }
}

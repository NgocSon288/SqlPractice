using FundManagement.Common.Custom.Attribute; 

namespace FundManagement.Common.Models
{
    public class BaseModel<TKey>
    {
        [NonMapping]
        public TKey ID { get; set; }
    }
}

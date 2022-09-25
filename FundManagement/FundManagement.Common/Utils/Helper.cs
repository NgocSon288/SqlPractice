using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagement.Common.Utils
{
    public class Helper
    {
        public static Dictionary<string, string> ConvertObjectToDictionary(object obj, string objName = "")
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var type = obj.GetType();
            if (type.IsValueType)
            {
                result.Add(objName, obj.ToString());
            }
            else
            {
                foreach(var prop in type.GetProperties())
                {
                    result.Add(prop.Name, prop.GetValue(obj).ToString());
                }
            }
            return result;
        }
    }
}

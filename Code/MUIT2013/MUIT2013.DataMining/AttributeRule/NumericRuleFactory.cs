using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{
    public class NumericRuleFactory : IRuleFactory<INumericRule>
    {
        public INumericRule CreateRule(string type, string data)
        {
            if (type == "SingleNumericRule")
            {
                return JsonConvert.DeserializeObject<SingleNumericRule>(data);
            }
            else if (type == "RangeNumericRule")
            {
                return JsonConvert.DeserializeObject<RangeNumericRule>(data);
            }
            else if (type == "MultiplyNumericRule")
            {
                return JsonConvert.DeserializeObject<MultiplyNumericRule>(data);
            }
            else if (type == "AddNumericRule")
            {
                return JsonConvert.DeserializeObject<AddNumericRule>(data);
            }
            return null;
        }
    }
}

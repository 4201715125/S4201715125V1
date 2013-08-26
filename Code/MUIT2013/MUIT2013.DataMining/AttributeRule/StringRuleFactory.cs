using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{
    public class StringRuleFactory : IRuleFactory<IStringRule>
    {
        public IStringRule CreateRule(string type, string data)
        {
            if (type == "SingleStringRule")
            {
                return JsonConvert.DeserializeObject<SingleStringRule>(data);
            }
            else if (type == "RangeStringRule")
            {
                return JsonConvert.DeserializeObject<RangeStringRule>(data);
            }
            else if (type == "RegexStringRule")
            {
                return JsonConvert.DeserializeObject<RegexStringRule>(data);
            }
            return null;
        }
    }
}

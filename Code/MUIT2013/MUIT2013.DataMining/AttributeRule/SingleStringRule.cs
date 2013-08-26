using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{
    public class SingleStringRule : IStringRule
    {
        public string Source { get; set; }
        public int Destination { get; set; }
        public int? Apply(string source)
        {
            if (source == Source)
            {
                return Destination;
            }
            return null;
        }

        public string ToSerialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return string.Format("SingleStringRule ({0} -> {1})",
                string.IsNullOrEmpty(Source) ? "{empty}" : Source,                
                Destination);           
        }

        public string GetRuleType()
        {
            return this.GetType().UnderlyingSystemType.Name;
        }


        public object Apply(object source)
        {
            if (source is string)
            {
                return Apply(source.ToString());
            }
            return source;
        }
    }
}

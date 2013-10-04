using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{    
    public class SingleNumericRule: INumericRule
    {
        public double Source { get; set; }
        public double Destination { get; set; }

        public SingleNumericRule()
        {

        }

        public double? Apply(double source)
        {
            if (source == Source)
            {
                return Destination;
            }
            return source;
        }

        public string ToSerialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return string.Format("SingleNumericRule ({0} -> {1})", Source, Destination);
        }

        public string GetRuleType()
        {
            return this.GetType().UnderlyingSystemType.Name;
        }


        public object Apply(object source)
        {
            if (source is double)
            {
                return Apply(double.Parse(source.ToString()));
            }
            return source;
        }
    }
}

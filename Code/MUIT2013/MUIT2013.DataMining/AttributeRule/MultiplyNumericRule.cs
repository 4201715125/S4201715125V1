using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{    
    public class MultiplyNumericRule : INumericRule
    {        
        public float Factor { get; set; }        
        public MultiplyNumericRule()
        {
            
        }

        public float? Apply(float source)
        {            
            return source * Factor;
        }

        public string ToSerialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return string.Format("MultiplyNumericRule (x{0})", Factor);
        }

        public string GetRuleType()
        {
            return this.GetType().UnderlyingSystemType.Name;
        }


        public object Apply(object source)
        {
            if (source is float)
            {
                return Apply(float.Parse(source.ToString()));
            }
            return source;
        }
    }
}

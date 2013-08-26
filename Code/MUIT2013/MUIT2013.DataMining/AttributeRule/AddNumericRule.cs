using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{
    public class AddNumericRule : INumericRule
    {
        public float Add { get; set; }
        public AddNumericRule()
        {

        }

        public float? Apply(float source)
        {
            return source + Add;
        }

        public string ToSerialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            return string.Format("AddNumericRule (+{0})", Add);
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

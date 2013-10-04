using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{
    public class RangeStringRule : IStringRule
    {
        private string from;
        public string From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
                if (string.Compare(from,to) > 0)
                {
                    to = from;
                }
            }
        }

        private string to;
        public string To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
                if (string.Compare(from, to) > 0)
                {
                    from = to;
                }
            }
        }

        public int Destination { get; set; }

        public double? Apply(string source)
        {
            int compareWithMin = string.Compare(source, From);
            int compareWithMax = string.Compare(source, To);
            if (compareWithMin >= 0 && compareWithMax <= 0)
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
            return string.Format("RangeStringRule ([{0}-{1}] -> {2})", 
                string.IsNullOrEmpty(From) ? "{empty}" : From, 
                string.IsNullOrEmpty(To) ? "{empty}" : To,
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

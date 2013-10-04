using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{    
    public class RangeNumericRule : INumericRule
    {
        private double from;
        public double From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
                if (to < from)
                {
                    to = from;
                }
            } 
        }

        private double to;
        public double To
        {
            get {
                return to;
            }
            set {
                to = value;
                if (to < from)
                {
                    from = to;
                }
            } 
        }

        public double Destination { get; set; }

        public double? Apply(double source)
        {
            if (source >= From && source <= To)
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
            return string.Format("RangeNumericRule ([{0}-{1} -> {2}])", From, To, Destination);
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

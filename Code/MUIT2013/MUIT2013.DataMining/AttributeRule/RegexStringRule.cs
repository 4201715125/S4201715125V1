using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{
    public class RegexStringRule : IStringRule
    {
        private string pattern;
        public string Pattern {
            get {
                return pattern;
            }
            set {
                regex = new Regex(value);
                pattern = value;
            } 
        }

        public int Destination { get; set; }

        private Regex regex;

        public double? Apply(string source)
        {

            if (regex!=null && regex.IsMatch(source))
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
            return string.Format("RegexStringRule ({0} -> {1})",
                string.IsNullOrEmpty(Pattern) ? "{empty}" : Pattern,
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

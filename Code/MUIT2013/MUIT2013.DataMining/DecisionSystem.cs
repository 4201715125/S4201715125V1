using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining
{
    public class DecisionSystem
    {
        public int[] ConditionAttributes { get; set; }
        public int[] DecisionAttributes { get; set; }
        public int DecisionAttribute { get; set; }
        public double[][] AttributesDomain { get; set; }

        public double?[][] Universe { get; set; }

        public int ObjectCount {
            get { return this.Universe.Length; }
        }

        public DecisionSystem(){}

        public DecisionSystem(double?[][] universe, int[] condAttrs, int decisionAttr) {
            this.Universe = universe;
            this.ConditionAttributes = condAttrs;
            this.DecisionAttribute = decisionAttr;
            this.DecisionAttributes = new int[] { decisionAttr };
            
            List<double[]> attrDomains = new List<double[]>();
            List<int> allAttrs = new List<int>(condAttrs);
            allAttrs.Add(decisionAttr);
            foreach (var attr in allAttrs) {
                var domain = universe
                        .Where(i => i[attr] != null)
                        .Select(i => (double)i[attr])
                        .ToArray();
                attrDomains.Add(domain);
            }
            this.AttributesDomain = attrDomains.ToArray();
        }
    }
}

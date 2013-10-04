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
    }
}

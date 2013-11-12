using System.Collections.Generic;
using System.Linq;

namespace MUIT2013.DataMining.DecisionPartition
{
    public class Rules
    {
        public List<DiscernibilityMatrix> DiscernMatrix { get; private set; }
        public Rules(DecisionSystem ds,IEnumerable<int> reduct)
        {

            var data = new List<double>(ds.AttributesDomain[ds.DecisionAttribute]);
            DiscernMatrix = new List<DiscernibilityMatrix>();
            foreach (var m in data.Select(d => reduct != null ? new DiscernibilityMatrix(ds, reduct, d) : new DiscernibilityMatrix(ds, ds.ConditionAttributes, d)))
            {
                m.GenerateRules();
                DiscernMatrix.Add(m);
            }
        }
    }
}

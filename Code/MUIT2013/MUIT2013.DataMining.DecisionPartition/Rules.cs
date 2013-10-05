using System.Collections.Generic;

namespace MUIT2013.DataMining.DecisionPartition
{
    public class Rules
    {
        public List<DiscernibilityMatrix> DiscernMatrix { get; private set; }
        public Rules(DecisionSystem DS)
        {

            var data = new List<double>(DS.AttributesDomain[DS.DecisionAttribute]);
            DiscernMatrix = new List<DiscernibilityMatrix>();
            foreach (var d in data)
            {
                var m = new DiscernibilityMatrix(DS, d);
                m.generateRules();
                DiscernMatrix.Add(m);
            }
        }
    }
}

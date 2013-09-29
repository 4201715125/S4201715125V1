using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining
{   
    class ReductProcessor
    {
        public ApproximationSpace ApprSpace { get; private set; }
        public DecisionSystem DS { 
            get { return this.ApprSpace.IS; } 
        }
        protected List<IEnumerable<int>> results = new List<IEnumerable<int>>();

        public ReductProcessor(ApproximationSpace apprSpace)
        {
            ApprSpace = apprSpace;
        }
    }
}

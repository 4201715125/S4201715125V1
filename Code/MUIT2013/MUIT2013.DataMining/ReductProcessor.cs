using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining
{   
    public class ReductProcessor
    {
        public DecisionSystem DS {get; private set;}
        protected List<IEnumerable<int>> results = new List<IEnumerable<int>>();

        public ReductProcessor(DecisionSystem _deciSystem)
        {
            DS = _deciSystem;
        }
    }
}

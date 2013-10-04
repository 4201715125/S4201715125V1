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
        public List<HashSet<int>> Results {get; protected set;}
        public IEnumerable<int> CoreAttrs { get; private set; }
        public IEnumerable<HashSet<int>> MinimalReducts { get; private set; }

        public ReductProcessor(DecisionSystem _deciSystem)
        {
            DS = _deciSystem;
            Results = new List<HashSet<int>>();
            Process();
            MinimalReducts = GetMinimalReducts();
            CoreAttrs = GetCoreAttrs();
        }

        protected virtual void Process(){}

        private IEnumerable<int> GetCoreAttrs() {
            HashSet<int> core = new HashSet<int>();
            foreach (var x in this.MinimalReducts)
                core.IntersectWith(x);
            return core;
        }

        private IEnumerable<HashSet<int>> GetMinimalReducts()
        {
            int minCount = DS.ConditionAttributes.Length;
            foreach (var rs in Results)
            {
                if (minCount > rs.Count) minCount = rs.Count;
            }
            return Results.Where(x => x.Count == minCount);
        }
    }
}

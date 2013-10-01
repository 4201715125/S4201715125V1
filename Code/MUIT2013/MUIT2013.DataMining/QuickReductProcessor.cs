using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining
{
    public class QuickReductProcessor : ReductProcessor
    {
        public QuickReductProcessor(DecisionSystem _deciSystem)
            : base(_deciSystem)
        { }

        public IEnumerable<int> LastResult
        {
            get {
                return this.results.FindLast(s => true);
            }
        }

        public IEnumerable<int> FindNewResult()
        {
            this.Process();
            return this.LastResult;
        }
        
        /// <summary>
        /// implementation of QuickReduct algorithm 
        /// </summary>
        protected void Process()
        {
            // dependency degree of all attributes
            HashSet<int> attrs = new HashSet<int>(this.DS.ConditionAttributes);
            float deDegreeAll = this.ValuateAttrs(attrs);
            HashSet<int> newReduct = new HashSet<int>();
            float deDegreeReduct = 0;
            foreach (int attr in attrs) {
                if(deDegreeReduct == deDegreeAll) break;

                HashSet<int> X = new HashSet<int>(newReduct);
                X.Add(attr);
                Console.WriteLine("Test attr: " + string.Join(",", X));
                float deDegreeX = this.ValuateAttrs(X);
                if (deDegreeReduct < deDegreeX)
                {
                    newReduct = X;
                    deDegreeReduct = deDegreeX;
                }
            }
            if(newReduct.Count != 0)
                this.results.Add(newReduct);
        }

        // This function could be overried in child classes of other
        //   algorithms for finding reducts
        protected float ValuateAttrs(IEnumerable<int> targetAttrs)
        {
            return this.CalcDependencyDegree(targetAttrs);
        }

        
        // Current only support only one denpendent attribute, 
        //   which is decision attr in DecisionSystem object
        private float CalcDependencyDegree(IEnumerable<int> targetAttrs)
        {
            // cardinal number of object univers
            StandardApproximationSpace apprSpace = new StandardApproximationSpace(this.DS, targetAttrs);
            int posCardNum = apprSpace.PositiveRegion(this.DS.DecisionAttribute).Count();
            float dependDegree = (float)posCardNum / this.DS.ObjectCount;
            return dependDegree;
        }
    }
}

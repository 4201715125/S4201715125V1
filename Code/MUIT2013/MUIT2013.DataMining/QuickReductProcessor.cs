using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining
{
    class QuickReductProcessor : ReductProcessor
    {
        public QuickReductProcessor(ApproximationSpace _apprSpace)
            :base(_apprSpace)
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
        private void Process()
        {
            // dependency degree of all attributes
            HashSet<int> attrs = new HashSet<int>(this.DS.ConditionAttributes);
            float deDegreeAll = this.CalcDependencyDegree(attrs);
            HashSet<int> newReduct = new HashSet<int>();
            float deDegreeReduct = 0;
            foreach (int attr in attrs) {
                if(deDegreeReduct == deDegreeAll) break;

                HashSet<int> X = new HashSet<int>(newReduct);
                X.Add(attr);
                float deDegreeX = this.CalcDependencyDegree(X);
                if (deDegreeReduct < deDegreeX)
                {
                    newReduct = X;
                    deDegreeReduct = deDegreeX;
                }
            }
            if(newReduct.Count != 0)
                this.results.Add(newReduct);
        }

        /// <summary>
        /// Current only support only one denpendent attribute, 
        ///     which is decision attr in DecisionSystem object
        /// </summary>
        /// <param name="targetAttrs"></param>
        /// <param name="dependentAttrs"></param>
        /// <returns></returns>
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

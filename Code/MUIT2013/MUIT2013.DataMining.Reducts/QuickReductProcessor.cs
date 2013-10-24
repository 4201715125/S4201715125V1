using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.Utils;


namespace MUIT2013.DataMining.Reducts
{
    public class QuickReductProcessor : ReductProcessor
    {
        public QuickReductProcessor(DecisionSystem _deciSystem)
            : base(_deciSystem)
        { }

        /// <summary>
        /// implementation of QuickReduct algorithm 
        /// </summary>
        protected override void Process()
        {
            // dependency degree of all attributes
            int[] attrs = this.DS.ConditionAttributes;
            float degreeAll = this.ValuateAttrs(attrs);
            HashSet<int> X, newReduct;
            float degreeX, degreeReduct;
            int tryTimes = 0;
            while ((++tryTimes) <= 50) // infinite loop
            {
                newReduct = new HashSet<int>();
                degreeReduct = 0;
                foreach (int attr in attrs.Shuffle())
                {
                    if (degreeReduct == degreeAll) break;
                    X = new HashSet<int>(newReduct);
                    X.Add(attr);
                    degreeX = this.ValuateAttrs(X);
                    if (degreeReduct < degreeX)
                    {
                        newReduct = X;
                        degreeReduct = degreeX;
                    }
                }

                if (newReduct.Count != 0 && newReduct.Count != attrs.Length && 
                    !this.Results.Exists(x => x.SetEquals(newReduct)))
                {
                    this.Results.Add(newReduct);
                    tryTimes = 0;
                }
                
                //Thread.Sleep(50);
            }
        }

        // This function could be overried in child classes of other
        //   algorithms for finding reducts
        protected virtual float ValuateAttrs(IEnumerable<int> targetAttrs)
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

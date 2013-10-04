using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MUIT2013.DataMining
{
    public class ApproximationSpace
    {
        #region Properties
        public DecisionSystem IS { get; set; }
        public Func<double?[], double?[], bool> IndRelation { get; set; }

        /// <summary>
        /// Rough Inclusion function, take 2 set of object X, Y and return [0,1]
        /// </summary>
        public Func<IEnumerable<double?[]>, IEnumerable<double?[]>, double> RoughIncl { get; set; }
        #endregion

        #region Constructors
        public ApproximationSpace(
            DecisionSystem infoSys,
            Func<double?[], double?[], bool> indiscerRel,
            Func<IEnumerable<double?[]>, IEnumerable<double?[]>, double> roughInclusion)
        {
            IS = infoSys;
            IndRelation = indiscerRel;
            RoughIncl = roughInclusion;
        }
        #endregion

        #region Methods
        
        public IEnumerable<double?[]> IndiscernibilityClass(double?[] x)
        {
            return IS.Universe.Where(y => IndRelation(x, y));
        }
        protected IEnumerable<IEnumerable<double?[]>> iClasses;
        public virtual IEnumerable<IEnumerable<double?[]>> IndiscernibilityClasses()
        {
            Debug.WriteLine("General AS ind-classes");
            return iClasses ?? (iClasses = IS.Universe.Select(IndiscernibilityClass));
        }

        public virtual IEnumerable<double?[]> LowerApproximation(IEnumerable<double?[]> X)
        {
            Debug.WriteLine("General AS Lower Aprroximation");

            return IndiscernibilityClasses()
                .Where(Ix => RoughIncl(Ix, X) == 1d)
                .Aggregate((lowX, Ix) => lowX.Union(Ix))
                ;
        }
        public virtual IEnumerable<double?[]> UpperApproximation(IEnumerable<double?[]> X)
        {
            Debug.WriteLine("General AS Upper Aprroximation");

            return IndiscernibilityClasses()
                .Where(Ix => RoughIncl(Ix, X) > 0d)
                .Aggregate((lowX, Ix) => lowX.Union(Ix))
                ;
        }

        /// <summary>
        /// Lower Approximation
        /// </summary>
        /// <param name="fX">A function that determine whether an object $x$ is belong to set $X$</param>
        /// <returns>Lower Approximation of $X$</returns>
        public virtual IEnumerable<double?[]> LowerApproximation(Func<double?[], bool> fX)
        {
            return LowerApproximation(IS.Universe.Where(fX));
        }
        public virtual IEnumerable<double?[]> UpperApproximation(Func<double?[], bool> fX)
        {
            return UpperApproximation(IS.Universe.Where(fX));
        }
        
        public virtual IEnumerable<double?[]> LowerApproximation(double decisionValue)
        {
            return LowerApproximation(x => x[IS.DecisionAttribute] == decisionValue);
        }
        public virtual IEnumerable<double?[]> UpperApproximation(double decisionValue)
        {
            return UpperApproximation(x => x[IS.DecisionAttribute] == decisionValue);
        }

        public virtual IEnumerable<double?[]> PositiveRegion(IEnumerable<IEnumerable<double?[]>> Xs)
        {
            Debug.WriteLine("General AS Positive Region");

            return Xs.Select(LowerApproximation)
                     .Aggregate((X, Y) => X.Union(Y))
                     ;
        }
        public virtual IEnumerable<double?[]> PositiveRegion(IEnumerable<Func<double?[], bool>> fXs)
        {
            Debug.WriteLine("General AS Positive Region");

            return fXs.Select(LowerApproximation)
                      .Aggregate((X, Y) => X.Union(Y))
                      ;
        }
        public virtual IEnumerable<double?[]> PositiveRegion(int decisionAttrIndex)
        {
            Debug.WriteLine("General AS Positive Region");

            return IS.AttributesDomain[decisionAttrIndex]
                .Select(LowerApproximation)
                .Aggregate((X, Y) => X.Union(Y))
                ;
        }
        #endregion
    }
}

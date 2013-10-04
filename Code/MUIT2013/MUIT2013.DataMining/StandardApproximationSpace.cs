using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MUIT2013.DataMining
{
    public class StandardApproximationSpace : ApproximationSpace
    {
        public IEnumerable<int> Attrs { get; private set; }

        public StandardApproximationSpace(DecisionSystem infoSys, IEnumerable<int> B)
            : base(infoSys, IndiscernibilityRelation.Equivalence(B), RoughInclusion.Standard)
        {
            if (!B.Any()) throw new ArgumentException("Should have at least 1 condition attributes", "B");
            Attrs = B;
        }

        public override IEnumerable<IEnumerable<double?[]>> IndiscernibilityClasses()
        {
            Debug.WriteLine("Standard AS ind-classes");

            return iClasses ?? 
                // O( #Univer * #Attribute )
                (iClasses = Attrs.Aggregate(
                    (IEnumerable<IEnumerable<double?[]>>) new[] {IS.Universe},
                    (partitions, a) => partitions.Select(xs => xs.GroupBy(x => x[a])).HConcat())
                );

            // TODO: a more better way with O( #Univer)
        }

        public override IEnumerable<double?[]> LowerApproximation(IEnumerable<double?[]> X)
        {
            Debug.WriteLine("Standard AS Lower Approximation");

            return IndiscernibilityClasses()
                .Where(X.SubsetEq)
                .Aggregate(
                    Enumerable.Empty<double?[]>(),
                    (lowX, Ix) => lowX.Union(Ix))
                ;
        }

        public override IEnumerable<double?[]> UpperApproximation(IEnumerable<double?[]> X)
        {
            Debug.WriteLine("Standard AS Upper Approximation");

            return IndiscernibilityClasses()
                .Where(Ix => X.Intersect(Ix).Any())
                .Aggregate(
                    Enumerable.Empty<double?[]>(),
                    (upX, Ix) => upX.Union(Ix))
                ;
        }

        /// <summary>
        /// Lower Approximation
        /// </summary>
        /// <param name="fX">A function that determine whether an object $x$ is belong to set $X$</param>
        /// <returns>Lower Approximation of $X$</returns>
        public override IEnumerable<double?[]> LowerApproximation(Func<double?[], bool> fX)
        {
            Debug.WriteLine("Standard AS Lower Approximation");

            return IndiscernibilityClasses()
                .Where(Ix => Ix.All(fX))
                .Aggregate(
                    Enumerable.Empty<double?[]>(),
                    (lowX, Ix) => lowX.Union(Ix))
                ;
        }
        public override IEnumerable<double?[]> UpperApproximation(Func<double?[], bool> fX)
        {
            Debug.WriteLine("Standard AS Upper Approximation");

            return IndiscernibilityClasses()
                .Where(Ix => Ix.Any(fX))
                .Aggregate(
                    Enumerable.Empty<double?[]>(),
                    (upX, Ix) => upX.Union(Ix))
            ;
        }
    }
}

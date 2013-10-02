using System;
using System.Collections.Generic;
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
            return IndiscernibilityClasses()
                .Where(X.SubsetEq)
                .Aggregate(
                    ((IEnumerable<double?[]>)new double?[][] { }),
                    (lowX, Ix) => lowX.Union(Ix))
                ;
        }

        public override IEnumerable<double?[]> UpperApproximation(IEnumerable<double?[]> X)
        {
            return IndiscernibilityClasses()
                .Where(Ix => X.Intersect(Ix).Any())
                .Aggregate(
                    ((IEnumerable<double?[]>)new double?[][] { }),
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
            //var indClasses = IndiscernibilityClasses();
            //var cIndClasses = indClasses.Where(Ix => Ix.All(fX)).ToArray();
            //var rs = cIndClasses.Aggregate(
            //    ((IEnumerable<double?[]>) new double?[][] { }),
            //    (lowX, Ix) => lowX.Union(Ix)).ToArray();
            //return rs;

            return IndiscernibilityClasses()
                .Where(Ix => Ix.All(fX))
                .Aggregate(
                    ((IEnumerable<double?[]>)new double?[][] { }),
                    (lowX, Ix) => lowX.Union(Ix))
                ;
        }
        public override IEnumerable<double?[]> UpperApproximation(Func<double?[], bool> fX)
        {
            return IndiscernibilityClasses()
                .Where(Ix => Ix.Any(fX))
                .Aggregate(
                    ((IEnumerable<double?[]>)new double?[][] { }),
                    (upX, Ix) => upX.Union(Ix))
            ;
        }
    }
}

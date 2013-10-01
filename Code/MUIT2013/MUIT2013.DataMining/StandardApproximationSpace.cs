using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (iClasses == null) iClasses = Attrs.Aggregate(
                    (IEnumerable<IEnumerable<double?[]>>) new[] { IS.Universe },
                    (partitions, a) => partitions.Select(xs => xs.GroupBy(x => x[a])).HConcat()
                );

            return iClasses;
            // TODO: a more better way
        }

        public override IEnumerable<double?[]> LowerApproximation(IEnumerable<double?[]> X)
        {
            return IS.Universe
                .Select(x => IndiscernibilityClass(x))
                .Where(Ix => X.SubsetEq(Ix))
                .Aggregate((lowX, Ix) => lowX.Union(Ix));
        }

        public override IEnumerable<double?[]> UpperApproximation(IEnumerable<double?[]> X)
        {
            return IS.Universe
                .Select(x => IndiscernibilityClass(x))
                .Where(Ix => X.Intersect(Ix).Any())
                .Aggregate((upX, Ix) => upX.Union(Ix));
        }

        /// <summary>
        /// Lower Approximation
        /// </summary>
        /// <param name="fX">A function that determine whether an object $x$ is belong to set $X$</param>
        /// <returns>Lower Approximation of $X$</returns>
        public override IEnumerable<double?[]> LowerApproximation(Func<double?[], bool> fX)
        {
            try
            {
                var rs = IS.Universe
                    .Select(x => IndiscernibilityClass(x))
                    .Where(Ix => Ix.All(x => fX(x)))
                    .Aggregate((lowX, Ix) => lowX.Union(Ix));
                return rs;
            }
            catch (InvalidOperationException e)
            {
                return new double?[][]{
                    new double?[] {}
                };
            }
            
        }
        public override IEnumerable<double?[]> UpperApproximation(Func<double?[], bool> fX)
        {
            return IS.Universe
                .Select(x => IndiscernibilityClass(x))
                .Where(Ix => Ix.Any(x => fX(x)))
                .Aggregate((upX, Ix) => upX.Union(Ix));
        }
    }
}

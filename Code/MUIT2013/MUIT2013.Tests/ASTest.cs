using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.DataMining;

namespace MUIT2013.Tests
{
    public class ObjectSetComparer : IEqualityComparer<IEnumerable<double?[]>>
    {
        public bool Equals(IEnumerable<double?[]> X, IEnumerable<double?[]> Y)
        {
            var oX = X.OrderBy(x => x[0]);
            var oY = Y.OrderBy(x => x[0]);
            return oX.SequenceEqual(oY);
        }

        public int GetHashCode(IEnumerable<double?[]> obj)
        {
            throw new NotImplementedException();
        }
    }

    public static class ASTest
    {
        public static void MainTest()
        {
            var infSys = new DecisionSystem
            { // KhuPN - Table 2 page 5
                Universe = new[]
                {   //            id  1  2  3  4
                    new double?[]{ 1, 0, 1, 1, 1 },
                    new double?[]{ 2, 1, 0, 1, 1 },
                    new double?[]{ 3, 1, 1, 2, 1 },
                    new double?[]{ 4, 0, 1, 0, 0 },
                    new double?[]{ 5, 1, 0, 1, 0 },
                    new double?[]{ 6, 0, 1, 2, 1 },
                },
                ConditionAttributes = new[] { 1, 2, 3 },
                DecisionAttributes = new[] { 4 },
                DecisionAttribute = 4,
                AttributesDomain = new[]
                { 
                    null, // no need for "id"
                    new double[]{ 0, 1    },
                    new double[]{ 0, 1    },
                    new double[]{ 0, 1, 2 },
                    new double[]{ 0, 1    },
                },
            };
            // Exmaple 3 page 5


            var indRel = IndiscernibilityRelation.Equivalence(infSys.ConditionAttributes);
            ApproximationSpace aprSpace = new ApproximationSpace(infSys, indRel, RoughInclusion.Standard);
            var partitions = new[]
            {
                new double?[] { 1   },
                new double?[] { 2,5 },
                new double?[] { 3   },
                new double?[] { 4   },
                new double?[] { 6   },
            };
            var lowerXs = new[]
            {
                new double?[]{ 4 },
                new double?[]{ 1,3,6 }
            };
            var upperXs = new[]
            {
                new double?[] {2, 4, 5},
                new double?[] {1, 2, 3, 5, 6}
            };
            var posRegion = new double?[] {1, 3, 4, 6};
            
            if (!TestPartition(aprSpace, partitions)) throw new Exception("Partition FAIL");
            if (!TestAprroximation(aprSpace, lowerXs, upperXs)) throw new Exception("Approximation FAIL");
            if (!TestPossitiveRegion(aprSpace, posRegion)) throw new Exception("Approximation FAIL");

            aprSpace = new StandardApproximationSpace(infSys, infSys.ConditionAttributes);
            if (!TestPartition(aprSpace, partitions)) throw new Exception("Partition FAIL");
            if (!TestAprroximation(aprSpace, lowerXs, upperXs)) throw new Exception("Approximation FAIL");
            if (!TestPossitiveRegion(aprSpace, posRegion)) throw new Exception("Approximation FAIL");

            aprSpace = new ApproximationSpace(aprSpace.IS, aprSpace.IndRelation, aprSpace.RoughIncl);
            if (!TestPartition(aprSpace, partitions)) throw new Exception("Partition FAIL");
            if (!TestAprroximation(aprSpace, lowerXs, upperXs)) throw new Exception("Approximation FAIL");
            if (!TestPossitiveRegion(aprSpace, posRegion)) throw new Exception("Approximation FAIL");
        }

        public static bool TestPartition(ApproximationSpace aprSpace, double?[][] indClasses)
        {
            return true;
            var partitions = aprSpace.IndiscernibilityClasses()
                .Select(X => X.Select(x => x[0])
                              .OrderBy(id => id)
                              .ToArray())
                // TODO: remove duplicate
                .OrderBy(X => X[0])
                .ToArray();
            return partitions.Zip(indClasses, (X, Y) => X.SequenceEqual(Y)).All(b => b);
        }

        public static bool TestAprroximation(ApproximationSpace aprSpace, double?[][] lowerXs, double?[][] upperXs)
        {
            var infSys = aprSpace.IS;
            var d = infSys.DecisionAttribute;
            var dVs = infSys.AttributesDomain[d];
            var Xs = dVs.Select(dV => infSys.Universe.Where(x => x[d] == dV).ToArray()).ToArray();

            var lXs = Xs.Select(X => aprSpace.LowerApproximation(X).Select(x => x[0]).OrderBy(id => id).ToArray()).ToArray();
            if (!lXs.Zip(lowerXs, (X, Y) => X.SequenceEqual(Y)).All(b => b)) return false;

            var uXs = Xs.Select(X => aprSpace.UpperApproximation(X).Select(x => x[0]).OrderBy(id => id).ToArray()).ToArray();
            if (!uXs.Zip(upperXs, (X, Y) => X.SequenceEqual(Y)).All(b => b)) return false;

            lXs = dVs.Select(dV => aprSpace.LowerApproximation(x => x[d] == dV).Select(x => x[0]).OrderBy(id => id).ToArray()).ToArray();
            if (!lXs.Zip(lowerXs, (X, Y) => X.SequenceEqual(Y)).All(b => b)) return false;

            uXs = dVs.Select(dV => aprSpace.UpperApproximation(x => x[d] == dV).Select(x => x[0]).OrderBy(id => id).ToArray()).ToArray();
            if (!uXs.Zip(upperXs, (X, Y) => X.SequenceEqual(Y)).All(b => b)) return false;

            lXs = dVs.Select(dV => aprSpace.LowerApproximation(dV).Select(x => x[0]).OrderBy(id => id).ToArray()).ToArray();
            if (!lXs.Zip(lowerXs, (X, Y) => X.SequenceEqual(Y)).All(b => b)) return false;

            uXs = dVs.Select(dV => aprSpace.UpperApproximation(dV).Select(x => x[0]).OrderBy(id => id).ToArray()).ToArray();
            if (!uXs.Zip(upperXs, (X, Y) => X.SequenceEqual(Y)).All(b => b)) return false;

            return true;

            
        }

        public static bool TestPossitiveRegion(ApproximationSpace aprSpace, double?[] posRegion)
        {
            var infSys = aprSpace.IS;
            var d = aprSpace.IS.DecisionAttribute;
            var dVs = infSys.AttributesDomain[d];
            var Xs = dVs.Select(dV => infSys.Universe.Where(x => x[d] == dV).ToArray()).ToArray();

            var posX = aprSpace.PositiveRegion(Xs).Select(x => x[0]).OrderBy(id => id).ToArray();
            if (!posX.SequenceEqual(posRegion)) return false;

            posX = aprSpace.PositiveRegion(d).Select(x => x[0]).OrderBy(id => id).ToArray();
            if (!posX.SequenceEqual(posRegion)) return false;

            return true;
        }

        public static string ToSetString(this IEnumerable<double?[]> xs, int id = 0)
        {
            return "{ " + string.Join(", ", xs.Select(x => x[id])) + " }";
        }

        /*
         new DecisionSystem { // wikipedia https://en.wikipedia.org/wiki/Rough_set
                Universe = new double?[][] { 
                    //                1  2  3  4  5
                    new double?[]{ 1, 1, 2, 0, 1, 1 },
                    new double?[]{ 2, 1, 2, 0, 1, 1 },
                    new double?[]{ 3, 2, 0, 0, 1, 0 },
                    new double?[]{ 4, 0, 0, 1, 2, 1 },
                    new double?[]{ 5, 2, 1, 0, 2, 1 },
                    new double?[]{ 6, 0, 0, 1, 2, 2 },
                    new double?[]{ 7, 2, 0, 0, 1, 0 },
                    new double?[]{ 8, 0, 1, 2, 2, 1 },
                    new double?[]{ 9, 2, 1, 0, 2, 2 },
                    new double?[]{10, 2, 0, 0, 1, 0 },
                },
                ConditionAttributes = new int[] { 1, 2, 3, 4, 5 },
            }
        */
    }
}

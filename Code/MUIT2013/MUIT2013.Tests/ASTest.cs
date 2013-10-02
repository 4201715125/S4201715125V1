using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.DataMining;

namespace MUIT2013.Tests
{
    public static class ASTest
    {
        public static void TestAprroximationSpace()
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
            var indRel = IndiscernibilityRelation.Equivalence(infSys.ConditionAttributes);
            ApproximationSpace aprSpace =
                //new ApproximationSpace(infSys, indRel, RoughInclusion.Standard)
                new StandardApproximationSpace(infSys, infSys.ConditionAttributes)
                ;
            //aprSpace = new ApproximationSpace(aprSpace.IS, aprSpace.IndRelation, aprSpace.RoughIncl);

            // Exmaple 3 page 5
            var partitions = aprSpace.IndiscernibilityClasses();

            const int d = 4;
            var X0 = infSys.Universe.Where(x => x[d] == 0);
            var lX0 = aprSpace.LowerApproximation(0);
            var uX0 = aprSpace.UpperApproximation(0);

            Func<double?[], bool> fX1 = x => x[d] == 1;
            var X1 = infSys.Universe.Where(fX1);
            var lX1 = aprSpace.LowerApproximation(
                //fX
                //X
                decisionValue:1d
                ).OrderBy(x => x[0]);
            var uX1 = aprSpace.UpperApproximation(
                //fX
                //X
                decisionValue:1d
                ).OrderBy(x => x[0]);
            var posX = aprSpace.PositiveRegion(
                //new[] { X0, X1}
                decisionAttrIndex:d
                ).OrderBy(x => x[0]);

            Console.WriteLine("partitions = {\n" +
                string.Join("\n", partitions.Select(partition => "    " + partition.ToSetString() + ","))
            + "\n}");
            Console.WriteLine();
            Console.WriteLine("X0 = " + X0.ToSetString());
            Console.WriteLine("lower(X0) = " + lX0.ToSetString());
            Console.WriteLine("upper(X0) = " + uX0.ToSetString());
            Console.WriteLine();
            Console.WriteLine("X1 = " + X1.ToSetString());
            Console.WriteLine("lower(X1) = " + lX1.ToSetString());
            Console.WriteLine("upper(X1) = " + uX1.ToSetString());
            Console.WriteLine();
            Console.WriteLine("pos(X) = " + posX.ToSetString());
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

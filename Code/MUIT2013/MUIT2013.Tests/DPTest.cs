using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.DataMining;
using MUIT2013.DataMining.DecisionPartition;

namespace MUIT2013.Tests
{
    public static class DPTest
    {
        public static void RunTest()
        {
            // Prepare test data
            var infSys = new DecisionSystem
                {
                    // KhuPN, Hong - "Datamining based on Rough set theory" pg. 8
                    Universe = new double?[][]
                        {
                            //            id  1  2  3  4  5
                            new double?[] {1, 2, 1, 0, 1, 0},
                            new double?[] {2, 2, 0, 1, 0, 1},
                            new double?[] {3, 1, 0, 0, 1, 0},
                            new double?[] {4, 0, 1, 0, 0, 1},
                            new double?[] {5, 0, 0, 0, 1, 1},
                            new double?[] {6, 2, 0, 0, 1, 0},
                            new double?[] {7, 0, 0, 1, 0, 1},
                            new double?[] {8, 2, 1, 1, 0, 1}

                        },
                    ConditionAttributes = new[] {1,2, 3, 4},
                    DecisionAttributes = new[] {5},
                    DecisionAttribute = 5,
                    AttributesDomain = new double[][]
                        {
                            null, // no need for "id"
                            new double[] {0, 1, 2},
                            new double[] {0, 1},
                            new double[] {0, 1},
                            new double[] {0, 1},
                            new double[] {0, 1},
                        },
                };

            var r=new Rules(infSys);
            //Output
            foreach (var discernibilityMatrix in r.DiscernMatrix)
            {
                Console.WriteLine("Attribute: "+discernibilityMatrix.RuleValue);
                Console.WriteLine("Prime Impicants:");
                foreach (var primeImplicant in discernibilityMatrix.primeImplicants)
                {
                    foreach (var pairId in primeImplicant)
                    {
                        Console.Write(pairId.AID+":"+pairId.VID+" ; ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.DataMining;
using MUIT2013.DataMining.Reducts;
using System.Threading;

namespace MUIT2013.Tests
{
    public static class ReductTest
    {
        public static void TestReduct()
        {
            // Prepare test data
            var infSys = new DecisionSystem
            {   // KhuPN, Hong - "Datamining based on Rough set theory" pg. 16
                Universe = new double?[][] 
                {   //            id  1  2  3  4  5
                    new double?[]{ 1, 2, 1, 0, 0, 1},
                    new double?[]{ 2, 0, 2, 1, 2, 1},
                    new double?[]{ 3, 1, 2, 1, 1, 2},
                    new double?[]{ 4, 1, 0, 0, 1, 1},
                    new double?[]{ 5, 2, 1, 2, 0, 0},
                    new double?[]{ 6, 1, 0, 1, 1, 2},
                    new double?[]{ 7, 0, 2, 1, 0, 1},
                    new double?[]{ 8, 0, 1, 2, 1, 2}
                },
                ConditionAttributes = new[] { 1, 2, 3, 4},
                DecisionAttributes = new[] { 5 },
                DecisionAttribute = 5,
                AttributesDomain = new double[][] 
                { 
                    null, // no need for "id"
                    new double[]{ 0, 1, 2 },
                    new double[]{ 0, 1, 2 },
                    new double[]{ 0, 1, 2 },
                    new double[]{ 0, 1, 2 },
                    new double[]{ 0, 1, 2 },
                },
            };

            Console.WriteLine("Running...");
            QuickReductProcessor processor = new QuickReductProcessor(infSys);
            
            // Show results
            Console.WriteLine("\nAll reducts: ");
            for(int i = 0; i < processor.Results.Count; i++ ) { 
                var rs = processor.Results[i];
                Console.WriteLine((i+1).ToString() + ". {" + string.Join(",", rs.OrderBy(x => x) ) + "}");
            }

            Console.WriteLine("\nMinimal reducts: ");
            var miRd = processor.MinimalReducts.ToArray();
            for (int i = 0; i < miRd.Length; i++)
            {
                var rs = miRd[i];
                Console.WriteLine((i + 1).ToString() + ". {" + string.Join(",", rs.OrderBy(x => x)) + "}");
            }

            Console.WriteLine("\nCore attributes: " + string.Join(",", processor.CoreAttrs));

            // Keep the console open in debug mode.
            Console.WriteLine(System.Environment.NewLine);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

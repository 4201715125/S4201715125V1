using System;
using System.Linq;
using MUIT2013.DataMining.Discretization;

namespace MUIT2013.Tests
{
    public static class MDTest
    {
        public static void RunTest()
        {
            // Prepare test data
            // KhuPN, Hong - "Datamining based on Rough set theory" Table 6 pg. 12
            var originalUniverse = new[]
                {
                    new object[] {1, 0.8, 2, 1},
                    new object[] {2, 1, 0.5, 0},
                    new object[] {3, 1.3, 3, 0},
                    new object[] {4, 1.4, 1, 1},
                    new object[] {5, 1.4, 2, 0},
                    new object[] {6, 1.6, 3, 1},
                    new object[] {7, 1.3, 1, 1}
                };
            var mdtest = new MD_Heuristic(originalUniverse, 0, new[] {1, 2}, new[] {3});
            Console.WriteLine("Cuts:");
            foreach (var discretization in mdtest.ConditionList)
            {
                Console.WriteLine("Attribute: " + discretization.Header + ": ");
                if (discretization.IsDiscreted) Console.WriteLine("Discreted");
                for (var i = 0; i < discretization.MappedTable.Count(); i++)
                {
                    Console.WriteLine(i + ", " + discretization.MappedTable[i]);
                }
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
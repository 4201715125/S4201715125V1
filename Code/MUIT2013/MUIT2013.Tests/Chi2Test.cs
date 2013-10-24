using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.DataMining;
using MUIT2013.DataMining.Chi2;

namespace MUIT2013.Tests
{
    public class Chi2Test
    {
        public static void perfom() {
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
                ConditionAttributes = new[] { 1, 2 },//{ 1, 2, 3, 4 },
                DecisionAttributes = new[] { 5 },
                DecisionAttribute = 5,
                AttributesDomain = new double[][] 
                { 
                    null, // no need for "id"
                    new double[]{ 0, 1, 2 },    //1
                    new double[]{ 0, 1, 2 },    //2
                    new double[]{ 0, 1, 2 },    //3
                    new double[]{ 0, 1, 2 },    //4
                    new double[]{ 0, 1, 2 },    //5
                },
            };

            var apprSpace = new StandardApproximationSpace(infSys, infSys.ConditionAttributes);

            double rate = apprSpace.InConsistencyRate();

            Chi2DistributionTable.GetValueAt(0.2, 1);
            int a = 1;
        }
    }
}

using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using MUIT2013.DataMining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class DataMiningService
    {
        public DecisionSystem GetDecisionSystem(List<AttributeDefinition> attributeDefinitions, string tableName) {
            //var infSys = new DecisionSystem
            //{ // KhuPN - Table 2 page 5
            //    Universe = new double?[][] 
            //    {   //            id  1  2  3  4
            //        new double?[]{ 1, 0, 1, 1, 1 },
            //        new double?[]{ 2, 1, 0, 1, 1 },
            //        new double?[]{ 3, 1, 1, 2, 1 },
            //        new double?[]{ 4, 0, 1, 0, 0 },
            //        new double?[]{ 5, 1, 0, 1, 0 },
            //        new double?[]{ 6, 0, 1, 2, 1 },
            //    },
            //    ConditionAttributes = new[] { 1, 2, 3 },
            //    DecisionAttributes = new[] { 4 },
            //    DecisionAttribute = 4,
            //    AttributesDomain = new double[][] 
            //    { 
            //        null, // no need for "id"
            //        new double[]{ 0, 1    },
            //        new double[]{ 0, 1    },
            //        new double[]{ 0, 1, 2 },
            //        new double[]{ 0, 1    },
            //    },
            //};
            
            int[] conditionAttributes;
            int[] decisionAttributes;
            double?[][] universe;
            double[][] attributeDomain;
            int idIndex = 0;
            DataRepository.GetDataForDecisionSystem(attributeDefinitions, tableName, out universe, out attributeDomain, out decisionAttributes, out conditionAttributes, out idIndex);
            DecisionSystem ds = new DecisionSystem
            {
                Universe = universe,
                ConditionAttributes = conditionAttributes,
                DecisionAttributes = decisionAttributes,
                DecisionAttribute = decisionAttributes[0],
                AttributesDomain = attributeDomain
            };
            return ds;
        }
    }
}

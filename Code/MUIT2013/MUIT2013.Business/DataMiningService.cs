using MUIT2013.Data.Enums;
using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using MUIT2013.DataMining;
using MUIT2013.DataMining.Reducts;
using MUIT2013.Utils;
using System;
using System.Collections;
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
            DataRepository.GetDataForDecisionSystem(attributeDefinitions, tableName, out universe, out attributeDomain, out decisionAttributes, out conditionAttributes, ref idIndex);
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

        public Tuple<DecisionTableHistory, IEnumerable<Reduct>> QuickReduct(List<AttributeDefinition> attributeDefinitions, long previousDecisionTableHistoryId)
        {
            var previousDTHistory = DecisionTableHistoryRepository.GetList().FirstOrDefault(p => p.Id == previousDecisionTableHistoryId);
            if (previousDTHistory == null) return null;
            var ds = GetDecisionSystem(attributeDefinitions, previousDTHistory.DecisionTable.TableName);
            QuickReductProcessor processor = new QuickReductProcessor(ds);
            var dtHistory = new DecisionTableHistory { 
                Action = EDTHistoryAction.QuickReduct,
                ParentId = previousDTHistory.Id,
                DecisionTableId = previousDTHistory.DecisionTableId,
                CreatedDate = DateTime.Now.ToNormalDateTimeString(),
                DecisionTable = previousDTHistory.DecisionTable,
                Decription = string.Format("Run Quick Reduct on Table \"{0}\"", previousDTHistory.DecisionTable.TableName)
            };

            //DecisionTableHistoryRepository.Create(dtHistory);
            var reducts = new List<Reduct>();
            // Show results
            // Console.WriteLine("\nAll reducts: ");
            for (int i = 0; i < processor.Results.Count; i++)
            {
                var rs = processor.Results[i];
                foreach (var attributeIndex in rs)
                {
                    reducts.Add(new Reduct
                    {
                        ResultIndex = i,
                        DecisionTableHistoryId = dtHistory.Id,
                        AttributeIndex = attributeIndex,
                        ReductType = EReductType.Normal
                    });
                }                
                //Console.WriteLine((i + 1).ToString() + ". {" + string.Join(",", rs.OrderBy(x => x)) + "}");
            }

            //Console.WriteLine("\nMinimal reducts: ");
            var miRd = processor.MinimalReducts.ToArray();
            for (int i = 0; i < miRd.Length; i++)
            {
                var rs = miRd[i];
                foreach (var attributeIndex in rs)
                {
                    reducts.Add(new Reduct
                    {
                        ResultIndex = i,
                        DecisionTableHistoryId = dtHistory.Id,
                        AttributeIndex = attributeIndex,
                        ReductType = EReductType.Minimal,                        
                    });
                }
                //Console.WriteLine((i + 1).ToString() + ". {" + string.Join(",", rs.OrderBy(x => x)) + "}");
            }

            foreach (var attributeIndex in processor.CoreAttrs)
            {
                reducts.Add(new Reduct
                {
                    ResultIndex = 0,
                    DecisionTableHistoryId = dtHistory.Id,
                    AttributeIndex = attributeIndex,
                    ReductType = EReductType.Core
                });
            }
            //Console.WriteLine("\nCore attributes: " + string.Join(",", processor.CoreAttrs));
            //ReductRepository.Create(reducts);
            return new Tuple<DecisionTableHistory, IEnumerable<Reduct>>(dtHistory, reducts);
        }

        public void SaveQuickReductResult(DecisionTableHistory dtHistory, IEnumerable<Reduct> reducts)
        {
            dtHistory.Decription = string.Format(
                "Run Quick Reduct on Table \"{0}\" with reduct attributes: {1}", 
                dtHistory.DecisionTable.TableName,
                string.Join(", ", (new Digitizer(dtHistory.DecisionTable)).Translate(
                    reducts.Select(p => p.AttributeIndex)                    
                ))
            );
            DecisionTableHistoryRepository.Create(dtHistory);
            foreach (var reduct in reducts)
            {
                reduct.DecisionTableHistoryId = dtHistory.Id;
            }
            
            ReductRepository.Create(reducts);
        }

        public IEnumerable<DecisionTableHistory> GetReductResult()
        {
            var dtHistories = DecisionTableHistoryRepository.GetList();
            var reducts = ReductRepository.GetListByDecisionTableHistory(dtHistories);
            foreach (var dtHistory in dtHistories)
            {
                dtHistory.Reducts = reducts.Where(p => p.DecisionTableHistoryId == dtHistory.Id);   
            }
            return dtHistories;
        }
        
        public void Approximation(List<AttributeDefinition> attributeDefinitions, long previousDecisionTableHistoryId)
        {
            var previousDTHistory = DecisionTableHistoryRepository.GetList().FirstOrDefault(p => p.Id == previousDecisionTableHistoryId);
            if (previousDTHistory == null) return;
            var ds = GetDecisionSystem(attributeDefinitions, previousDTHistory.DecisionTable.TableName);

            previousDTHistory = GetReductResult().FirstOrDefault(p => p.Id == previousDecisionTableHistoryId);
            ds.ConditionAttributes = previousDTHistory.Reducts.Select(p => p.AttributeIndex + 1).ToArray();

            var indRel = IndiscernibilityRelation.Equivalence(ds.ConditionAttributes);
            ApproximationSpace aprSpace = new ApproximationSpace(ds, indRel, RoughInclusion.Standard);
            var objectIdClasses = aprSpace.IndiscernibilityClasses().Select(c => c.Select(o => (long)o[0]));

            var dtHistory = new DecisionTableHistory
            {
                Action = EDTHistoryAction.Approximation,
                ParentId = previousDTHistory.Id,
                DecisionTableId = previousDTHistory.DecisionTableId,
                CreatedDate = DateTime.Now.ToNormalDateTimeString(),
                DecisionTable = previousDTHistory.DecisionTable,
                Decription = string.Format("Run Approximation on Table \"{0}\"", previousDTHistory.DecisionTable.TableName)
            };
            DecisionTableHistoryRepository.Create(dtHistory);
            ApproximationRepository.CreateIndiscernibilityClass(dtHistory.Id, objectIdClasses);
        }
    }
}

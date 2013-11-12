using MUIT2013.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using System.Data.Common;

namespace MUIT2013.Data.Repository
{
    public class AttributeDefinitionRepository : RepositoryBase
    {        
        public static long Create(AttributeDefinition model)
        {
            long id = 0;
            using (var con = EstablishConnection())
            {
                con.Open();
                id = con.Query<long>(@"
                    INSERT INTO AttributeDefinitions(name, rawName, dataFileId)
                    VALUES(@Name,@RawName, @DataFileId);
                    SELECT last_insert_rowid();
                ", model).First();
                model.Id = id;
                con.Close();
            }          
            return id;
        }

        public static IEnumerable<AttributeDefinition> GetList(long dataFileId)
        {
            IEnumerable<AttributeDefinition> models;
            using (var con = EstablishConnection())
            {
                con.Open();

                models = con.Query<AttributeDefinition>(@"
                    SELECT * FROM AttributeDefinitions cd                    
                    WHERE DataFileId IN (@DataFileId)
                    ;
                ", new { DataFileId = dataFileId });
                var AttributeDefinitionIds = string.Join(",", models.Select(p => p.Id));
                var mapRuleModels = con.Query<MapRule>(string.Format(@"
                    SELECT * FROM MapRules
                    WHERE AttributeDefinitionId IN ({0})", AttributeDefinitionIds));

                foreach (var model in models)
                {
                    foreach (var mapRuleModel in mapRuleModels)
                    {
                        if (mapRuleModel.AttributeDefinitionId == model.Id)
                        {
                            model.MapRules.Add(mapRuleModel);
                            mapRuleModel.AttributeDefinition = model;
                        }
                    }
                }

                con.Close();
            }
            return models;
        }

        public static void BulkInsert(DbConnection con, List<AttributeDefinition> models)
        {       
            con.Execute(@"
                INSERT INTO AttributeDefinitions(name, rawName, dataFileId, attributeIndex)
                VALUES(@Name,@RawName, @DataFileId, @AttributeIndex);                
            ", models);          
        }

        public static void BulkUpdate(DbConnection con, List<AttributeDefinition> models)
        {
            StringBuilder queryBuilder = new StringBuilder();
            string updateAttributeDefinitionQuery = @"
                UPDATE AttributeDefinitions
                SET IsIdentifier = @IsIdentifier,
                IsAutoEncoding = @IsAutoEncoding,
                IsDecision = @IsDecision,
                AttributeDataType = @AttributeDataType,
                Description = @Description,
                ValidationStatus = @ValidationStatus
                WHERE Id = @Id;
            ";

            string deleteMapRuleQuery = @"
                DELETE FROM MapRules WHERE AttributeDefinitionId = @AttributeDefinitionId;
            ";

            string insertMapRuleQuery = @"
                INSERT INTO MapRules(AttributeDefinitionId, RuleContent, RuleType) VALUES(@AttributeDefinitionId, @RuleContent, @RuleType);
            ";

            con.Execute(updateAttributeDefinitionQuery, models);
            var mapRuleModels = new List<MapRule>();
            models.ForEach(p => {
                mapRuleModels.AddRange(p.MapRules);
            });

            if (mapRuleModels.Count > 0) 
            {
                con.Execute(deleteMapRuleQuery, mapRuleModels);
                con.Execute(insertMapRuleQuery, mapRuleModels);
            }                       
        }

        public static void BulkUpdate(List<AttributeDefinition> models)
        {
            using (var con = EstablishConnection())
            {
                con.Open();
                var transaction = con.BeginTransaction();
                BulkUpdate(con, models);
                transaction.Commit();
                con.Close();
            }
        }

        public static Dictionary<string, string> GetAttributeNameDictionary(IEnumerable<AttributeDefinition> attributeDefs)
        {
            var attributeDefDict = new Dictionary<string, string>();
            attributeDefDict = attributeDefs.Aggregate(attributeDefDict, (d, ad) =>
            {
                d.Add(ad.Name, ad.RawName);
                return d;
            });
            return attributeDefDict;
        }

        public static string[] GetAttributeNameDictionary(IEnumerable<AttributeDefinition> attributeDefs, int[] attributeIndices) 
        { 
            var dict = GetAttributeNameDictionary(attributeDefs);
            return attributeIndices.Select(i => dict[string.Format("Column_{0}", i)]).ToArray();
        }
    }
}

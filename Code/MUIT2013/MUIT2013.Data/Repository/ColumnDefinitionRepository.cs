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
    public class ColumnDefinitionRepository : RepositoryBase
    {        
        public static long Create(ColumnDefinition model)
        {
            long id = 0;
            using (var con = EstablishConnection())
            {
                con.Open();
                id = con.Query<long>(@"
                    INSERT INTO ColumnDefinitions(name, rawName, dataFileId)
                    VALUES(@Name,@RawName, @DataFileId);
                    SELECT last_insert_rowid();
                ", model).First();
                model.Id = id;
                con.Close();
            }          
            return id;
        }

        public static IEnumerable<ColumnDefinition> GetList(long dataFileId)
        {
            IEnumerable<ColumnDefinition> models;
            using (var con = EstablishConnection())
            {
                con.Open();

                models = con.Query<ColumnDefinition>(@"
                    SELECT * FROM ColumnDefinitions cd                    
                    WHERE DataFileId IN (@DataFileId)
                    ;
                ", new { DataFileId = dataFileId });
                var columnDefinitionIds = string.Join(",", models.Select(p => p.Id));
                var mapRuleModels = con.Query<MapRule>(string.Format(@"
                    SELECT * FROM MapRules
                    WHERE ColumnDefinitionId IN ({0})", columnDefinitionIds));

                foreach (var model in models)
                {
                    foreach (var mapRuleModel in mapRuleModels)
                    {
                        if (mapRuleModel.ColumnDefinitionId == model.Id)
                        {
                            model.MapRules.Add(mapRuleModel);
                            mapRuleModel.ColumnDefinition = model;
                        }
                    }
                }

                con.Close();
            }
            return models;
        }

        public static void BulkInsert(DbConnection con, List<ColumnDefinition> models)
        {       
            con.Execute(@"
                INSERT INTO ColumnDefinitions(name, rawName, dataFileId)
                VALUES(@Name,@RawName, @DataFileId);                
            ", models);          
        }

        public static void BulkUpdate(DbConnection con, List<ColumnDefinition> models)
        {
            StringBuilder queryBuilder = new StringBuilder();
            string updateColumnDefinitionQuery = @"
                UPDATE ColumnDefinitions
                SET IsCondition = @IsCondition,
                IsDecision = @IsDecision,
                ColumnType = @ColumnType,
                Description = @Description,
                ValidationStatus = @ValidationStatus
                WHERE Id = @Id;
            ";

            string deleteMapRuleQuery = @"
                DELETE FROM MapRules WHERE ColumnDefinitionId = @ColumnDefinitionId;
            ";

            string insertMapRuleQuery = @"
                INSERT INTO MapRules(ColumnDefinitionId, RuleContent, RuleType) VALUES(@ColumnDefinitionId, @RuleContent, @RuleType);
            ";

            con.Execute(updateColumnDefinitionQuery, models);
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

        public static void BulkUpdate(List<ColumnDefinition> models)
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
    }
}

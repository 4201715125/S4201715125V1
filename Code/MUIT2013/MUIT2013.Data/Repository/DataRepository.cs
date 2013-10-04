using CsvHelper;
using MUIT2013.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using MUIT2013.Utils;

namespace MUIT2013.Data.Repository
{
    public class DataRepository: RepositoryBase
    {
        public static void UploadData(string fileName, string absoluteFilePath, string relativeFilePath)
        {                        
            // create data file
            var dataFile = new DataFile { 
                RawTableName = string.Format("RawTable{0}",DateTime.Now.ToTimeStamps()),
                Name = fileName,
                CreatedDate = DateTime.Now.ToTimeStamps(),
                RelativePath = relativeFilePath
            };
            DataFileRepository.Create(dataFile);

            using (var con = EstablishConnection())
            {
                con.Open();
                var transaction = con.BeginTransaction();
                var csvReader = new CsvReader(new StreamReader(absoluteFilePath), new CsvHelper.Configuration.CsvConfiguration
                {
                    //Delimiter = "\t",
                    TrimFields = true,
                    TrimHeaders = true
                });
                csvReader.Read();
                // create column definitions
                var fieldHeaders = csvReader.FieldHeaders;
                var AttributeDefinitions = new List<AttributeDefinition>();
                for (int i = 0; i < fieldHeaders.Length; i++)
                {
                    var fieldHeader = fieldHeaders[i];
                    var AttributeDefinition = new AttributeDefinition
                    {
                        RawName = fieldHeader,
                        Name = string.Format("Column_{0}", i + 1),
                        AttributeIndex = i,
                        DataFileId = dataFile.Id
                    };
                    AttributeDefinitions.Add(AttributeDefinition);
                }
                AttributeDefinitionRepository.BulkInsert(con, AttributeDefinitions);

                // create raw table
                DataRepository.CreateRawTable(con, dataFile.RawTableName, AttributeDefinitions);

                // fill data into raw table
                var records = new List<string[]>();
                while (csvReader.Read()) records.Add(csvReader.CurrentRecord);
                DataRepository.BulkInsertRawTable(con, dataFile.RawTableName, AttributeDefinitions, records);

                transaction.Commit();
                con.Close();
            }
        }

        public static void CreateRawTable(DbConnection con, string rawTableName, List<AttributeDefinition> AttributeDefinitions)
        {                        
            var AttributeDefinitionList = AttributeDefinitions.Select(p => string.Format("{0} VARCHAR(255)", p.Name));
            var AttributeDefinitionSql = string.Join(", ", AttributeDefinitionList);
            var sql = string.Format("CREATE TABLE {0} (Id INTEGER PRIMARY KEY, {1});", rawTableName, AttributeDefinitionSql);
            con.Execute(sql);            
        }

        public static void CreateMapTable(DbConnection con, string mapTableName, List<AttributeDefinition> AttributeDefinitions)
        {
            var AttributeDefinitionList = new List<string>();
            AttributeDefinitions.ForEach(p => {
                if (p.IsAutoEncoding)
                {
                    AttributeDefinitionList.Add(string.Format("{0} REAL", p.Name));
                }
                else if (p.MapRules.Count==0)
	            {
                    AttributeDefinitionList.Add(string.Format("{0} VARCHAR(255)", p.Name));
	            }
                else
            	{
                    if (p.ValidationStatus == "Invalid")
	                {
                        AttributeDefinitionList.Add(string.Format("{0} VARCHAR(255)", p.Name));
                        
	                }else{
                        if (p.ColumnType == "Numeric")
                        {
                            AttributeDefinitionList.Add(string.Format("{0} REAL", p.Name));
                        }
                        else AttributeDefinitionList.Add(string.Format("{0} INTEGER", p.Name));
                                
                    }
	            }
            });            
            var AttributeDefinitionSql = string.Join(", ", AttributeDefinitionList);
            var sql = string.Format("CREATE TABLE {0} (Id INTEGER, {1});", mapTableName, AttributeDefinitionSql);
            con.Execute(sql);
        }

        public static void BulkInsertRawTable(DbConnection con, string rawTableName, List<AttributeDefinition> AttributeDefinitions, List<string[]> records)
        {                        
            var attributeClause = string.Join(",", AttributeDefinitions.Select(p => p.Name));
            var insertValueClause = string.Join(",", AttributeDefinitions.Select(p => "@" + p.Name));
            var sql = string.Format("INSERT INTO {0}({1}) VALUES({2});", rawTableName, attributeClause, insertValueClause);
            
            var rows = new List<Dictionary<string, string>>();
            foreach (var record in records)
            {
                var command = con.CreateCommand();
                command.CommandText = sql;
                for (int i = 0; i < record.Length; i++)
                {
                    command.Parameters.Add(new SQLiteParameter("@" + AttributeDefinitions[i].Name, record[i]));
                }
                command.ExecuteNonQuery();
            }            
        }

        public static void BulkInsertMapTable(DbConnection con, string mapTableName, List<AttributeDefinition> AttributeDefinitions, List<string[]> records)
        {
            var attributeClause = string.Join(",", AttributeDefinitions.Select(p => p.Name));
            var insertValueClause = string.Join(",", AttributeDefinitions.Select(p => "@" + p.Name));
            var sql = string.Format("INSERT INTO {0}(Id, {1}) VALUES(@Id, {2});", mapTableName, attributeClause, insertValueClause);

            var rows = new List<Dictionary<string, string>>();
            foreach (var record in records)
            {
                var command = con.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SQLiteParameter("@Id", record[0]));
                for (int i = 0; i < record.Length - 1; i++)
                {
                    command.Parameters.Add(new SQLiteParameter("@" + AttributeDefinitions[i].Name, record[i+1]));
                }
                command.ExecuteNonQuery();
            }
        }

        public static void GenerateMapTable(DataFile dataFile, List<AttributeDefinition> AttributeDefinitions, List<string[]> records)
        {
            // create data file
            var mapTableName = dataFile.MapTableName = string.Format("MapTable{0}", DateTime.Now.ToTimeStamps());            
            DataFileRepository.UpdateMapTable(dataFile);
            using (var con = EstablishConnection())
            {
                con.Open();
                var transaction = con.BeginTransaction();
                CreateMapTable(con, mapTableName, AttributeDefinitions);
                BulkInsertMapTable(con, mapTableName, AttributeDefinitions, records);
                transaction.Commit();
                con.Close();
            }
        }

        public static IEnumerable<string> GetDistinctValues(DbConnection con, AttributeDefinition AttributeDefinition, string rawTableName)
        {
            var sql = string.Format("SELECT DISTINCT {0} FROM {1}", AttributeDefinition.Name, rawTableName);
            return con.Query<string>(sql);
        }

        public static IEnumerable<string> GetDistinctValues(AttributeDefinition AttributeDefinition, string rawTableName)
        {
            IEnumerable<string> models;
            using (var con = EstablishConnection())
            {
                con.Open();
                models = GetDistinctValues(con, AttributeDefinition, rawTableName);
                con.Close();
            }
            return models;
        }

        public static IEnumerable<dynamic> GetDataForTable(DbConnection con, string rawTableName)
        { 
            var sql = string.Format("SELECT * FROM {0}", rawTableName);
            return con.Query(sql);
        }

        public static IEnumerable<dynamic> GetDataForTable(string rawTableName)
        {
            using (var con = EstablishConnection())
            {
                con.Open();
                IEnumerable<dynamic> data = GetDataForTable(con, rawTableName);
                con.Close();

                return data;
            }            
        }

        public static void GetDataForDecisionSystem(IEnumerable<AttributeDefinition> attributeDefinitions, string rawTableName, out double?[][] universe, out double[][] attributeDomain, out int[] decisionAttributes, out int[] conditionAttributes, out int idIndex) 
        {
            var da = new List<int>();
            var ca = new List<int>();
            idIndex = 0;
            foreach (var ad in attributeDefinitions)
            {                
                if (ad.IsDecision)
                {
                    da.Add(ad.AttributeIndex);
                }
                else
                {
                    if (ad.IsIdentifier)
                    {
                        idIndex = ad.AttributeIndex;
                    }
                    else
                    {
                        ca.Add(ad.AttributeIndex);
                    }
                }                
            }

            decisionAttributes = da.ToArray();
            conditionAttributes = ca.ToArray();

            var data = GetDataForTable(rawTableName);
            var dataCount = data.Count();
            var attributeCount = attributeDefinitions.Count();            
            universe = new double?[dataCount][];
            var attributeDomainDict = new Dictionary<int, List<double>>();
            var i = 0;
            foreach (var row in data)
            {
                var obj = new double?[attributeCount];
                var rowDict = (IDictionary<string, object>)row;
                foreach (var ad in attributeDefinitions)
                {
                    obj[ad.AttributeIndex] = (double?)rowDict[ad.Name];
                    if (!attributeDomainDict.ContainsKey(ad.AttributeIndex))
                    {
                        if (ad.IsIdentifier)
                        {
                            attributeDomainDict[ad.AttributeIndex] = null;
                        }
                        else
                        {
                            attributeDomainDict[ad.AttributeIndex] = new List<double>();
                        }
                    }
                    if (attributeDomainDict[ad.AttributeIndex] != null)
                    {
                        attributeDomainDict[ad.AttributeIndex].Add((double)rowDict[ad.Name]);
                    }
                }
                universe[i] = obj;
                i++;
            }

            attributeDomain = new double[attributeCount][];
            for (int j = 0; j < attributeCount; j++)
            {
                attributeDomain[j] = attributeDomainDict[j] == null ? null : attributeDomainDict[j].Distinct().ToArray();
            }
        }
    }
}

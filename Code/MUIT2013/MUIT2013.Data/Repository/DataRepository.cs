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
                var columnDefinitions = new List<ColumnDefinition>();
                for (int i = 0; i < fieldHeaders.Length; i++)
                {
                    var fieldHeader = fieldHeaders[i];
                    var columnDefinition = new ColumnDefinition
                    {
                        RawName = fieldHeader,
                        Name = string.Format("Column_{0}", i + 1),
                        DataFileId = dataFile.Id
                    };
                    columnDefinitions.Add(columnDefinition);
                }
                ColumnDefinitionRepository.BulkInsert(con, columnDefinitions);

                // create raw table
                DataRepository.CreateRawTable(con, dataFile.RawTableName, columnDefinitions);

                // fill data into raw table
                var records = new List<string[]>();
                while (csvReader.Read()) records.Add(csvReader.CurrentRecord);
                DataRepository.BulkInsertRawTable(con, dataFile.RawTableName, columnDefinitions, records);

                transaction.Commit();
                con.Close();
            }
        }

        public static void CreateRawTable(DbConnection con, string rawTableName, List<ColumnDefinition> columnDefinitions)
        {                        
            var columnDefinitionList = columnDefinitions.Select(p => string.Format("{0} VARCHAR(255)", p.Name));
            var columnDefinitionSql = string.Join(", ", columnDefinitionList);
            var sql = string.Format("CREATE TABLE {0} (Id INTEGER PRIMARY KEY, {1});", rawTableName, columnDefinitionSql);
            con.Execute(sql);            
        }

        public static void CreateMapTable(DbConnection con, string mapTableName, List<ColumnDefinition> columnDefinitions)
        {
            var columnDefinitionList = new List<string>();
            columnDefinitions.ForEach(p => {
                if (p.MapRules.Count==0)
	            {
                    columnDefinitionList.Add(string.Format("{0} VARCHAR(255)", p.Name));
	            }
                else
            	{
                    if (p.ValidationStatus == "Invalid")
	                {
                        columnDefinitionList.Add(string.Format("{0} VARCHAR(255)", p.Name));
                        
	                }else{
                        if (p.ColumnType == "Numeric")
                        {
                            columnDefinitionList.Add(string.Format("{0} REAL", p.Name));
                        }
                        else columnDefinitionList.Add(string.Format("{0} INTEGER", p.Name));
                                
                    }
	            }
            });            
            var columnDefinitionSql = string.Join(", ", columnDefinitionList);
            var sql = string.Format("CREATE TABLE {0} (Id INTEGER, {1});", mapTableName, columnDefinitionSql);
            con.Execute(sql);
        }

        public static void BulkInsertRawTable(DbConnection con, string rawTableName, List<ColumnDefinition> columnDefinitions, List<string[]> records)
        {                        
            var attributeClause = string.Join(",", columnDefinitions.Select(p => p.Name));
            var insertValueClause = string.Join(",", columnDefinitions.Select(p => "@" + p.Name));
            var sql = string.Format("INSERT INTO {0}({1}) VALUES({2});", rawTableName, attributeClause, insertValueClause);
            
            var rows = new List<Dictionary<string, string>>();
            foreach (var record in records)
            {
                var command = con.CreateCommand();
                command.CommandText = sql;
                for (int i = 0; i < record.Length; i++)
                {
                    command.Parameters.Add(new SQLiteParameter("@" + columnDefinitions[i].Name, record[i]));
                }
                command.ExecuteNonQuery();
            }            
        }

        public static void BulkInsertMapTable(DbConnection con, string mapTableName, List<ColumnDefinition> columnDefinitions, List<string[]> records)
        {
            var attributeClause = string.Join(",", columnDefinitions.Select(p => p.Name));
            var insertValueClause = string.Join(",", columnDefinitions.Select(p => "@" + p.Name));
            var sql = string.Format("INSERT INTO {0}(Id, {1}) VALUES(@Id, {2});", mapTableName, attributeClause, insertValueClause);

            var rows = new List<Dictionary<string, string>>();
            foreach (var record in records)
            {
                var command = con.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SQLiteParameter("@Id", record[0]));
                for (int i = 0; i < record.Length - 1; i++)
                {
                    command.Parameters.Add(new SQLiteParameter("@" + columnDefinitions[i].Name, record[i+1]));
                }
                command.ExecuteNonQuery();
            }
        }

        public static void GenerateMapTable(DataFile dataFile, List<ColumnDefinition> columnDefinitions, List<string[]> records)
        {
            // create data file
            var mapTableName = dataFile.MapTableName = string.Format("MapTable{0}", DateTime.Now.ToTimeStamps());            
            DataFileRepository.UpdateMapTable(dataFile);
            using (var con = EstablishConnection())
            {
                con.Open();
                var transaction = con.BeginTransaction();
                CreateMapTable(con, mapTableName, columnDefinitions);
                BulkInsertMapTable(con, mapTableName, columnDefinitions, records);
                transaction.Commit();
                con.Close();
            }
        }

        public static IEnumerable<string> GetDistinctValues(DbConnection con, ColumnDefinition columnDefinition, string rawTableName)
        {
            var sql = string.Format("SELECT DISTINCT {0} FROM {1}", columnDefinition.Name, rawTableName);
            return con.Query<string>(sql);
        }

        public static IEnumerable<string> GetDistinctValues(ColumnDefinition columnDefinition, string rawTableName)
        {
            IEnumerable<string> models;
            using (var con = EstablishConnection())
            {
                con.Open();
                models = GetDistinctValues(con, columnDefinition, rawTableName);
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

       
    }
}

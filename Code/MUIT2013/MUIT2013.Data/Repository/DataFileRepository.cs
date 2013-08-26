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
    public class DataFileRepository : RepositoryBase
    {
        public static long Create(DataFile model)
        {
            long id = 0;
            using (var con = EstablishConnection())
            {
                con.Open();
                con.Execute(@"
                    INSERT INTO DataFiles(name, rawTableName, createdDate, relativePath)
                    VALUES(@Name,@RawTableName, @CreatedDate, @RelativePath);                    
                ", model);
                id = con.Query<long>("SELECT last_insert_rowid();").First();
                model.Id = id;
                con.Close();
            }
            return id;
        }

        public static IEnumerable<DataFile> GetList()
        {
            IEnumerable<DataFile> models;
            using (var con = EstablishConnection())
            {
                con.Open();
                models = con.Query<DataFile>("SELECT * FROM DataFiles");
                con.Close();
            }
            return models;
        }

        public static void Activate(DataFile dataFile)
        {
            using(var con = EstablishConnection())
	        {
                con.Open();
                var transaction = con.BeginTransaction();

                con.Execute(@"
                    Update DataFiles SET IsActivated = 0;
                    Update DataFiles SET IsActivated = 1 WHERE id = @Id;
                ", dataFile);
                dataFile.IsActivated = true;
                transaction.Commit();
                con.Close();
	        }
        }

        public static void UpdateMapTable(DataFile dataFile)
        {
            using (var con = EstablishConnection())
            {
                con.Open();
                con.Execute(@"Update DataFiles SET MapTableName = @MapTableName, IsMapped = 1 WHERE Id = @Id", dataFile);
                con.Close();
            }
        }

        public static DataFile GetActivatedDateFile()
        {
            return GetList().FirstOrDefault(p => p.IsActivated);
        }

        public static void BulkUpdate(DbConnection con, List<DataFile> models)
        {
            StringBuilder queryBuilder = new StringBuilder();            
            string updateDataFileQuery = @"
                UPDATE DataFiles
                SET Description = @Description                
                WHERE Id = @Id;
            ";
            
            con.Execute(updateDataFileQuery, models);           
        }

        public static void BulkUpdate(List<DataFile> models)
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

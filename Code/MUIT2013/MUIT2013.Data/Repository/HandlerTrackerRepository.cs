using MUIT2013.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;

namespace MUIT2013.Data.Repository
{
    public class HandlerTrackerRepository : RepositoryBase
    {
        public static long Create(HandlerTracker model)
        {
            long id = 0;
            using (var con = EstablishConnection())
            {
                con.Open();
                id = con.Query<long>(@"
                    INSERT INTO HandlerTrackers(tableName, previousTableName, content, createdDate)
                    VALUES(@TableName, @PreviousTableName,@Content, @CreatedDate);
                    SELECT last_insert_rowid();
                ", model).First();
                model.Id = id;
                con.Close();
            }
            return id;
        }

        public static IEnumerable<HandlerTracker> GetList()
        {
            IEnumerable<HandlerTracker> models;
            using (var con = EstablishConnection())
            {
                con.Open();

                models = con.Query<HandlerTracker>(@"
                    SELECT * FROM HandlerTrackers                    
                    ;
                ");                

                con.Close();
            }
            return models;
        }
    }
}

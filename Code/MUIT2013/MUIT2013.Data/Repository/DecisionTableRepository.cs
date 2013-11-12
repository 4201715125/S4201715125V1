using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using MUIT2013.Data.Models;

namespace MUIT2013.Data.Repository
{
    public class DecisionTableRepository : RepositoryBase
    {
        public static long Create(DecisionTable model)
        {
            long id = 0;
            using (var con = EstablishConnection())
            {
                con.Open();
                con.Execute(@"
                    INSERT INTO DecisionTables(PreviousId, TableName, Content, CreatedDate)
                    VALUES(@PreviousId, @TableName, @Content, @CreatedDate);                    
                ", model);
                id = con.Query<long>("SELECT last_insert_rowid();").First();
                model.Id = id;
                con.Close();
            }
            return id;
        }

        public static IEnumerable<DecisionTable> GetList()
        {
            IEnumerable<DecisionTable> models;
            using (var con = EstablishConnection())
            {
                con.Open();
                models = con.Query<DecisionTable>(@"
                    SELECT * FROM DecisionTables cd                                        
                    ;
                ");
                con.Close();
            }
            return models;
        }
    }
}

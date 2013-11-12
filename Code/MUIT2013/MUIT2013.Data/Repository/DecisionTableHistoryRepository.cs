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
    public class DecisionTableHistoryRepository : RepositoryBase
    {
        public static long Create(DecisionTableHistory model)
        {
            long id = 0;
            using (var con = EstablishConnection())
            {
                con.Open();
                con.Execute(@"
                    INSERT INTO DecisionTableHistories(DecisionTableId, Action, Decription, CreatedDate, ParentId)
                    VALUES(@DecisionTableId, @Action, @Decription, @CreatedDate, @ParentId);                    
                ", model);
                id = con.Query<long>("SELECT last_insert_rowid();").First();
                model.Id = id;
                con.Close();
            }            
            return id;
        }

        public static IEnumerable<DecisionTableHistory> GetList()
        {
            IEnumerable<DecisionTableHistory> models;
            using (var con = EstablishConnection())
            {
                con.Open();

                models = con.Query<DecisionTableHistory>(@"
                    SELECT * FROM DecisionTableHistories dth                    
                    ;
                ");

                models = con.Query<DecisionTableHistory, DecisionTable, DecisionTableHistory>(@"
                    SELECT * FROM DecisionTableHistories dth
                    LEFT JOIN DecisionTables dt ON dt.Id = dth.DecisionTableId
                    ;
                ", (dth, dt) => { 
                     dth.DecisionTable = dt; return dth; 
                 });

                con.Close();
            }
            return models;
        }
    }
}

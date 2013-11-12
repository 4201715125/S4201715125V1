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
    public class ReductRepository : RepositoryBase
    {
        public static string InsertInto = @"
                    INSERT INTO Reducts(DecisionTableHistoryId, AttributeIndex, ResultIndex)
                    VALUES(@DecisionTableHistoryId, @AttributeIndex, @ResultIndex);                    
                ";


        public static long Create(Reduct model)
        {
            long id = 0;
            using (var con = EstablishConnection())
            {
                con.Open();
                con.Execute(@"
                    INSERT INTO Reducts(DecisionTableHistoryId, AttributeIndex, ResultIndex)
                    VALUES(@DecisionTableHistoryId, @AttributeIndex, @ResultIndex);                    
                ", model);
                id = con.Query<long>("SELECT last_insert_rowid();").First();
                model.Id = id;              
                con.Close();
            }
            return id;
        }

        public static void Create(IEnumerable<Reduct> models)
        {            
            using (var con = EstablishConnection())
            {
                con.Open();
                con.Execute(InsertInto, models);
                con.Close();
            }            
        }

        public static IEnumerable<Reduct> GetListByDecisionTableHistory(IEnumerable<DecisionTableHistory> dtHistories)
        {
            IEnumerable<Reduct> models;
            using (var con = EstablishConnection())
            {
                con.Open();                

                models = con.Query<Reduct, DecisionTableHistory, Reduct>(@"
                    SELECT * FROM Reducts r
                    LEFT JOIN DecisionTableHistories dth ON r.DecisionTableHistoryId = dth.id                    
                    ;
                ", (r, dth) =>
                 {
                     r.DecisionTableHistory = dth;
                     return r;
                 });

                models = models.Where(p => dtHistories.Any(k => k.Id == p.DecisionTableHistoryId));
                con.Close();
            }
            return models;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;

namespace MUIT2013.Data.Repository
{
    public class ApproximationRepository : RepositoryBase
    {        
        public static void CreateIndiscernibilityClass(long DecisionTableHistoryId, IEnumerable<IEnumerable<long>> objectIdClasses)
        {            
            using (var con = EstablishConnection())
            {
                con.Open();

                var models = new List<dynamic>();
                var i = 0;
                foreach (var objectIds in objectIdClasses)
                {
                    models.AddRange(
                        objectIds.Select(p => new
                        { 
                            ObjectId = p, 
                            ClassIndex = i, 
                            DecisionTableHistoryId = DecisionTableHistoryId
                        })
                    );
                    i++;
                }
                
                con.Execute(@"
                    INSERT INTO IndiscernibilityClasses(DecisionTableHistoryId, ObjectId, ClassIndex)
                    VALUES(@DecisionTableHistoryId, @ObjectId, @ClassIndex);                    
                ", models);                
                
                con.Close();
            }            
        }
    }
}

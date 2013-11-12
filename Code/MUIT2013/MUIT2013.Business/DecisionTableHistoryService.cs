using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using MUIT2013.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class DecisionTableHistoryService : ServiceBase
    {
        public void ExecuteApproximation(long decisionTableId, IEnumerable<IEnumerable<long>> objectIdClasses, long? decisionTableHistoryId = null)
        {            
            var id = DecisionTableHistoryRepository.Create(new DecisionTableHistory { 
                DecisionTableId = decisionTableId,
                Action = "Approximation",
                Decription = "",
                ParentId = decisionTableHistoryId.HasValue ? decisionTableHistoryId.Value : 0,
                CreatedDate = DateTime.Now.ToShortDateString()
            });

            ApproximationRepository.CreateIndiscernibilityClass(id, objectIdClasses);
        }

        public void ExecuteReduct(long decisionTableId, IEnumerable<Reduct> models, long? decisionTableHistoryId = null)
        {
            var id = DecisionTableHistoryRepository.Create(new DecisionTableHistory
            {
                DecisionTableId = decisionTableId,
                Action = "Reduct",
                Decription = "",
                ParentId = decisionTableHistoryId.HasValue ? decisionTableHistoryId.Value : 0,
                CreatedDate = DateTime.Now.ToShortDateString()
            });
            ReductRepository.Create(models);
        }

        public List<DecisionTableHistoryView> GetList(Func<DecisionTableHistory, bool> condition = null)
        {
            var models = DecisionTableHistoryRepository.GetList();
            if (condition!=null)
            {
                models = models.Where(condition);
            }
            return models.Select(p => new DecisionTableHistoryView{
                Id = p.Id,
                TableName = p.DecisionTable.TableName,
                Action = p.Action,
                CreatedDate = p.CreatedDate,
                Description = p.Decription
            }).ToList();
        }        
    }
}

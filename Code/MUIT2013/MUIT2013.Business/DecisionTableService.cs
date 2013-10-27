using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class DecisionTableService : ServiceBase
    {
        public List<DecisionTable> GetList()
        {
            return DecisionTableRepository.GetList().ToList();
        }

        public long Create(DecisionTable model)
        {
            return DecisionTableRepository.Create(model);
        }
    }
}

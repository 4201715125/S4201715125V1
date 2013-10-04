using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.Utils;
using MUIT2013.Data.Models;
using MUIT2013.DataMining.AttributeRule;
using MUIT2013.Data.ViewModels;
using MUIT2013.Data.Repository;

namespace MUIT2013.Business
{
    public class HandlerTrackerService : ServiceBase
    {
        public List<HandlerTracker> GetList()
        {
            return HandlerTrackerRepository.GetList().ToList();
        }        

        public long Create(HandlerTracker model)
        {
            return HandlerTrackerRepository.Create(model);
        }        
    }
}

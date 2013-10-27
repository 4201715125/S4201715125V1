using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.ViewModels
{
    public class ApproximationDTHistoryView
    {
        public long Id { get; set; }
        public string DecisionTableName { get; set; }
        public string Action { get; set; }
        public IEnumerable<IEnumerable<long>> IndiscernibilityClasses { get; set; }

    }
}

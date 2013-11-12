using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.ViewModels
{
    public class DecisionTableHistoryView
    {
        public long Id { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string CreatedDate { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Models
{
    public class Reduct
    {
        public long Id { get; set; }
        public long DecisionTableHistoryId { get; set; }
        public int AttributeIndex { get; set; }
        public int ResultIndex { get; set; }
        public string ReductType { get; set; }
        public DecisionTableHistory DecisionTableHistory { get; set; }
    }
}

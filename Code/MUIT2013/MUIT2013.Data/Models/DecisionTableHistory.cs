using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Models
{
    public class DecisionTableHistory
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public long DecisionTableId { get; set; }
        public string Action { get; set; }
        public string Decription { get; set; }
        public string CreatedDate { get; set; }
        public DecisionTable DecisionTable { get; set; }
        public IEnumerable<Reduct> Reducts { get; set; }
        public DecisionTableHistory()
        {
            Reducts = new List<Reduct>();
        }
    }
}

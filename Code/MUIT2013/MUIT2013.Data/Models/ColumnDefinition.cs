using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Models
{
    public class ColumnDefinition
    {
        public long Id { get; set; }
        public string RawName { get; set; }
        public string Name { get; set; }
        public string ColumnType { get; set; }
        public bool IsCondition { get; set; }
        public bool IsDecision { get; set; }
        public long DataFileId { get; set; }
        public string ValidationStatus { get; set; }        
        public List<MapRule> MapRules { get; set; }
        public string Description { get; set; }

        public ColumnDefinition()
        {
            MapRules = new List<MapRule>();
        }
    }
}

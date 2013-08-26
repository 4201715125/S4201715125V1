using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Models
{
    public class MapRule
    {
        public long Id { get; set; }
        public long ColumnDefinitionId { get; set; }
        public string RuleContent { get; set; }
        public string RuleType { get; set; }

        public ColumnDefinition ColumnDefinition { get; set; }

        public MapRule()
        {

        }
    }
}

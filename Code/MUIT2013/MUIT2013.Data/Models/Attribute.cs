using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Models
{
    public class Attribute
    {
        public long Id { get; set; }
        public long DecisionTableId { get; set; }
        public int AttributeIndex { get; set; }
        public string AttributeType { get; set; }
        public string CreatedDate { get; set; }
    }
}

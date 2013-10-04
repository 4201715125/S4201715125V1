using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Models
{
    public class HandlerTracker
    {
        public long Id { get; set; }
        public string PreviousTableName { get; set; }
        public string TableName { get; set; }
        public string Content { get; set; }
        public string CreatedDate { get; set; }
    }
}

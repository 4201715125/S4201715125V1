using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Models
{
    public class ColumnDefinition
    {
        public virtual int Id { get; set; }
        public virtual string RawName { get; set; }
        public virtual string MapName { get; set; }
        
        public virtual int DataFileId { get; set; }
        public virtual DataFile DataFile { get; set; }

        public virtual int ColumnTypeId { get; set; } // condition, decision, identifier?

        public virtual int IsNormalized { get; set; }
    }
}

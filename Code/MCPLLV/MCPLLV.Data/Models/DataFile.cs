using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Models
{
    public class DataFile
    {        
        public virtual int Id { get; set; }
        public virtual string FileName { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
        public virtual string RawTBName { get; set; }
        public virtual string MapTBName { get; set; }
        public virtual int TotalColumns { get; set; }
        public virtual int TotalRows { get; set; }
        public virtual IList<ColumnDefinition> ColumnDefinitions { get; set; }

        public DataFile()
        {
            ColumnDefinitions = new List<ColumnDefinition>();
        }
    }
}

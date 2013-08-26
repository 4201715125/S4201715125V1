using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Models
{
    public class DataFile
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public string RawTableName { get; set; }
        public string MapTableName { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActivated { get; set; }
        public bool IsMapped { get; set; }
        public string Description { get; set; }
    }
}

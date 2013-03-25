using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Models
{
    public class UserProject
    {
        public virtual int Id { get; set; }        
        public virtual bool IsAuthor { get; set; }

        public virtual int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}

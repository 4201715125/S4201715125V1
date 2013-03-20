using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Models
{
    public class Project
    {
        public virtual int Id { get; set; }
        public virtual string ProjectName { get; set; }
        public virtual string Description { get; set; }        
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
        
        public virtual IList<User> Users { get; set; }        

        public Project()
        {
            Users = new List<User>();
        }
    }
}

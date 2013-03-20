using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Models
{
    public class UserGroup
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int Priority { get; set; }

        public virtual IList<User> Users { get; set; }

        public UserGroup()
        {
            Users = new List<User>();
        }
    }
}
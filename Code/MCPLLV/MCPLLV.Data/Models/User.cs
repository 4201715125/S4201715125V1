using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Address { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }

        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime UpdatedDate { get; set; }
        
        public virtual int UserGroupId { get; set; }
        public virtual UserGroup UserGroup { get; set; }

        public virtual IList<UserProject> UserProjects { get; set; }

        public User()
        {
            UserProjects = new List<UserProject>();
        }
    }
}

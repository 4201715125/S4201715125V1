using FluentNHibernate.Mapping;
using MCPLLV.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Mappings
{
    public class UserProjectMap : ClassMap<UserProject>
    {
        public UserProjectMap()
        {
            Id(x => x.Id);       
            Map(x => x.IsAuthor).Default("0").Not.Nullable();

            References<User>(x => x.User, "UserId").Not.Nullable();
            References<Project>(x => x.Project, "ProjectId").Not.Nullable();               
        }
    }
}

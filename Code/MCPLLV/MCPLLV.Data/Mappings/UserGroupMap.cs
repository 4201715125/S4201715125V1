using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using MCPLLV.Data.Models;

namespace MCPLLV.Data.Mappings
{
    public class UserGroupMap : ClassMap<UserGroup>
    {
        public UserGroupMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Description);
            Map(x => x.Priority).Not.Nullable();

            HasMany<User>(x => x.Users)
                .KeyColumn("UserGroupId")
                .Inverse()
                .Cascade.All();
        }
    }
}

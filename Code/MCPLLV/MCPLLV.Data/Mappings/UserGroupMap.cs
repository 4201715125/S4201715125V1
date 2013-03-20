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
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Priority);

            HasMany(x => x.Users)
                .Inverse()
                .Cascade.All();
        }
    }
}

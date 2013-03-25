using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using MCPLLV.Data.Models;

namespace MCPLLV.Data.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.UserName).Not.Nullable();
            Map(x => x.Password).Not.Nullable();

            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Address);
            Map(x => x.Phone);
            Map(x => x.Email);

            Map(x => x.CreatedDate);
            Map(x => x.UpdatedDate);

            References<UserGroup>(x => x.UserGroup, "UserGroupId");
            HasMany<UserProject>(x => x.UserProjects)
                .KeyColumn("UserId")
                .Inverse()
                .Cascade.All();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using MCPLLV.Data.Models;

namespace MCPLLV.Data.Mappings
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Id(x => x.Id);
            Map(x => x.ProjectName);
            Map(x => x.Description);        
            Map(x => x.CreatedDate);
            Map(x => x.UpdatedDate);

            HasManyToMany(x => x.Users)
                .Cascade.All()
                .Table("UserProject");
        }
    }
}

using FluentNHibernate.Mapping;
using MCPLLV.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Mappings
{
    public class ColumnDefinitionMap : ClassMap<ColumnDefinition>
    {
        public ColumnDefinitionMap()
        {
            Id(x => x.Id);
            Map(x => x.RawName).Not.Nullable();
            Map(x => x.MapName).Not.Nullable();
            Map(x => x.ColumnTypeId).Not.Nullable(); // condition, decision, identifier
            Map(x => x.IsNormalized).Default("0").Not.Nullable();

            References<DataFile>(x => x.DataFile, "DataFileId");            
        }
    }
}

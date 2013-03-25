using FluentNHibernate.Mapping;
using MCPLLV.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Data.Mappings
{
    public class DataFileMap : ClassMap<DataFile>
    {
        public DataFileMap()
        {
            Id(x => x.Id);
            Map(x => x.FileName).Not.Nullable();
            Map(x => x.CreatedDate);
            Map(x => x.UpdatedDate);
            Map(x => x.RawTBName).Not.Nullable();
            Map(x => x.MapTBName);
            Map(x => x.TotalRows).Default("0").Not.Nullable();
            Map(x => x.TotalColumns).Default("0").Not.Nullable();            

            HasMany<ColumnDefinition>(x => x.ColumnDefinitions)
                .KeyColumn("DataFileId")
                .Inverse()
                .Cascade.All();
        }
    }
}

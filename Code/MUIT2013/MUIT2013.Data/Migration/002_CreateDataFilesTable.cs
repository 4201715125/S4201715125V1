using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Migration
{
    [FluentMigrator.Migration(2)]
    public class _002_CreateDataFilesTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("DataFiles")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("RawTableName").AsString().NotNullable()
                .WithColumn("MapTableName").AsString().Nullable()
                .WithColumn("RelativePath").AsString().NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("IsActivated").AsBoolean().WithDefaultValue(false)
                .WithColumn("IsMapped").AsBoolean().WithDefaultValue(false)
                .WithColumn("CreatedDate").AsString(); 
        }

        public override void Down()
        {
            Delete.Table("DataFiles");
        }
    }
}

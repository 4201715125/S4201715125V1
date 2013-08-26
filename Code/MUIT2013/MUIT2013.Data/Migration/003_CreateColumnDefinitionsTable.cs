using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Migration
{
    [FluentMigrator.Migration(3)]
    public class _003_CreateColumnDefinitionsTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("ColumnDefinitions")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("RawName").AsString().NotNullable()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ColumnType").AsFixedLengthString(32).NotNullable().WithDefaultValue("String")
                .WithColumn("ValidationStatus").AsFixedLengthString(32).NotNullable().WithDefaultValue("Not Ready")
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("IsCondition").AsBoolean().WithDefaultValue(false)
                .WithColumn("IsDecision").AsBoolean().WithDefaultValue(false)
                .WithColumn("DataFileId").AsInt64().ForeignKey("DataFiles", "Id");                
        }

        public override void Down()
        {
            Delete.Table("ColumnDefinitions");
        }
    }
}

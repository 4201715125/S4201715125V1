using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Migration
{
    [FluentMigrator.Migration(5)]
    public class _005_CreateHandlerTrackersTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("HandlerTrackers")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("PreviousTableName").AsString().NotNullable()
                .WithColumn("TableName").AsString().NotNullable()                
                .WithColumn("Content").AsString().Nullable()
                .WithColumn("CreatedDate").AsString();
        }

        public override void Down()
        {
            Delete.Table("HandlerTrackers");
        }
    }
}

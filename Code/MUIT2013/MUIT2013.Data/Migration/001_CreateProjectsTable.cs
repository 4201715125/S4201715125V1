using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Migration
{
    [FluentMigrator.Migration(1)]
    public class _001_CreateProjectsTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Projects")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("Name").AsString().WithDefaultValue("")
                .WithColumn("Password").AsString().WithDefaultValue("")
                .WithColumn("Description").AsString().WithDefaultValue("")
                .WithColumn("RelativePath").AsString().WithDefaultValue("");
        }

        public override void Down()
        {
            Delete.Table("Projects");
        }
    }
}

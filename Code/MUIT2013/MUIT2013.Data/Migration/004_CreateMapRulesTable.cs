using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Migration
{    
    [FluentMigrator.Migration(4)]
    public class _004_CreateMapRulesTable : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("MapRules")
                .WithColumn("Id").AsInt64().PrimaryKey()                
                .WithColumn("RuleContent").AsString().NotNullable()
                .WithColumn("RuleType").AsString().NotNullable()
                .WithColumn("ColumnDefinitionId").AsInt64().ForeignKey("ColumnDefinitions", "Id");
        }

        public override void Down()
        {
            Delete.Table("MapRules");
        }
    }
}

using MUIT2013.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Data.Migration
{
    [FluentMigrator.Migration(5)]
    public class _005_CreateDecisionTables : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("DecisionTables")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("PreviousId").AsInt64()
                .WithColumn("TableName").AsString().NotNullable()                
                .WithColumn("Content").AsString().Nullable()
                .WithColumn("CreatedDate").AsString();

            Create.Table("Attributes")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("DecisionTableId").AsInt64().NotNullable()
                .WithColumn("AttributeIndex").AsInt32().NotNullable()
                .WithColumn("AttributeType").AsString().WithDefaultValue(EAttributeType.Condition)
                .WithColumn("CreatedDate").AsString();

            Create.Table("DecisionTableHistories")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("ParentId").AsInt64()
                .WithColumn("DecisionTableId").AsInt64().NotNullable()
                .WithColumn("Action").AsFixedLengthString(32).NotNullable()
                .WithColumn("Decription").AsString()
                .WithColumn("CreatedDate").AsString();

            Create.Table("Reducts")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("DecisionTableHistoryId").AsInt64()                
                .WithColumn("AttributeIndex").AsInt32().NotNullable()
                .WithColumn("ResultIndex").AsInt32()
                .WithColumn("ReductType").AsString().WithDefaultValue(EReductType.Normal)
                ;

            Create.Table("IndiscernibilityClasses")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("DecisionTableHistoryId").AsInt64()
                .WithColumn("ClassIndex").AsInt64().NotNullable()                
                .WithColumn("ObjectId").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("DecisionTables");
            Delete.Table("Attributes");
            Delete.Table("DecisionTableHistories"); 
            Delete.Table("Reducts");
            Delete.Table("IndiscernibilityClasses");            
        }
    }
}

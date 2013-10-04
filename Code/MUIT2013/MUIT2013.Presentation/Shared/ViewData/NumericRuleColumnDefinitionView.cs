using MUIT2013.Data.Models;
using MUIT2013.Presentation.Shared.Collection;
using MUIT2013.Presentation.Shared.Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Presentation.Shared.ViewData
{
    public class NumericRuleAttributeDefinitionView : AttributeDefinitionView
    {
        [Editor(typeof(NumericRuleEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Contains list of Numeric Rules")]
        [Category("Configuration")]
        [DisplayName("Rule Collection")]        
        public NumericRuleCollection RuleCollection { get; set; }

        public NumericRuleAttributeDefinitionView(AttributeDefinition AttributeDefinition)
            : base(AttributeDefinition)
        {
            RuleCollection = new NumericRuleCollection();
        }
    }
}

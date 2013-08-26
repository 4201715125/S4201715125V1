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
    public class StringRuleColumnDefinitionView : ColumnDefinitionView
    {
        [Editor(typeof(StringRuleEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Contains list of String Rules")]
        [Category("Configuration")]
        [DisplayName("Rule Collection")]
        public StringRuleCollection RuleCollection { get; set; }

        public StringRuleColumnDefinitionView(ColumnDefinition columnDefinition) : base(columnDefinition)
        {
            RuleCollection = new StringRuleCollection();
        }
    }
}

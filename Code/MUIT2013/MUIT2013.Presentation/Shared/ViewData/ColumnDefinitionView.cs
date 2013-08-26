using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.Data.Models;
using MUIT2013.Presentation.Shared.Converter;
using MUIT2013.DataMining.AttributeRule;
using MUIT2013.Presentation.Shared.Editor;
using System.Drawing.Design;
using MUIT2013.Presentation.Shared.Collection;
using System.Reflection;
using System.ComponentModel.Design;

namespace MUIT2013.Presentation.Shared.ViewData
{
    public class ColumnDefinitionView
    {
        [Browsable(true)]
        [ReadOnly(true)]
        [Description("The name of column")]
        [Category("General")]
        [DisplayName("Column Name")]
        public string RawName { get { return this.ColumnDefinition.RawName; } }        

        [Browsable(true)]
        [Description("determine whether the column is a condition attribute or not")]
        [Category("Configuration")]
        [DisplayName("Is Condition")]
        public bool IsCondition {
            get
            {
                return this.ColumnDefinition.IsCondition;
            }
            set
            {
                this.ColumnDefinition.IsCondition = value;
            }
        }

        [Browsable(true)]
        [Description("determine whether the column is a decision attribute or not")]
        [DisplayName("Is Decision")]
        [Category("Configuration")]
        public bool IsDecision {
            get
            {
                return this.ColumnDefinition.IsDecision;
            }
            set
            {
                this.ColumnDefinition.IsDecision = value;
            }
        }

        [Browsable(true)]
        [Description("description of this attribute")]
        [DisplayName("Description")]
        [Category("Configuration")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Description {
            get {
                return this.ColumnDefinition.Description;
            }
            set {
                this.ColumnDefinition.Description = value;
            }
        }

        [Browsable(true)]
        [Description("")]
        [DisplayName("Validation Status")]
        [Category("Configuration")]
        [ReadOnly(true)]
        [DefaultValue("Not Ready")]
        public string ValidationStatus { get; set; }

        [Browsable(false)]
        public ColumnDefinition ColumnDefinition { get; set; }                 

        public ColumnDefinitionView(ColumnDefinition columnDefinition)
        {
            this.ColumnDefinition = columnDefinition;            
        }                
    }
}

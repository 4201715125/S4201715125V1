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
    public class AttributeDefinitionView
    {
        [Browsable(true)]
        [ReadOnly(true)]
        [Description("The name of attribute")]
        [Category("General")]
        [DisplayName("Attribute Name")]
        public string RawName { get { return this.AttributeDefinition.RawName; } }

        [Browsable(true)]
        [Description("determine whether the attribute is a identifier or not")]
        [DisplayName("Is Identifier")]
        [Category("Configuration")]
        public bool IsIdentifier
        {
            get
            {
                return this.AttributeDefinition.IsIdentifier;
            }
            set
            {
                this.AttributeDefinition.IsIdentifier = value;
            }
        }

        [Browsable(true)]
        [Description("determine whether the attribute is a decision attribute or not")]
        [DisplayName("Is Decision")]
        [Category("Configuration")]
        public bool IsDecision {
            get
            {
                return this.AttributeDefinition.IsDecision;
            }
            set
            {
                this.AttributeDefinition.IsDecision = value;
            }
        }

        [Browsable(true)]
        [Description("determine whether the attribute is auto encoding or not")]
        [DisplayName("Is Auto Encoding")]
        [Category("Configuration")]
        public bool IsAutoEncoding
        {
            get
            {
                return this.AttributeDefinition.IsAutoEncoding;
            }
            set
            {
                this.AttributeDefinition.IsAutoEncoding = value;
            }
        }        

        [Browsable(true)]
        [Description("description of this attribute")]
        [DisplayName("Description")]
        [Category("Configuration")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Description {
            get {
                return this.AttributeDefinition.Description;
            }
            set {
                this.AttributeDefinition.Description = value;
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
        public AttributeDefinition AttributeDefinition { get; set; }                 

        public AttributeDefinitionView(AttributeDefinition AttributeDefinition)
        {
            this.AttributeDefinition = AttributeDefinition;            
        }                
    }
}

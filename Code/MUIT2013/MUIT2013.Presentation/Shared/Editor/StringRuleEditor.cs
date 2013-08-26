using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using MUIT2013.Presentation.Shared.ViewData;
using System.Windows.Forms;
using MUIT2013.DataMining.AttributeRule;
using MUIT2013.Presentation.Shared.Collection;

namespace MUIT2013.Presentation.Shared.Editor
{
    public class StringRuleEditor : CollectionEditor
    {        
        public StringRuleEditor() : base(typeof(StringRuleCollection))
        {

        }       

        protected override Type[] CreateNewItemTypes()
        {            
            return new Type[] { typeof(SingleStringRule), typeof(RangeStringRule), typeof(RegexStringRule) };                                        
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return base.GetEditStyle(context);            
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            return base.EditValue(context, provider, value);
        }

        protected override object CreateInstance(Type itemType)
        {
            return base.CreateInstance(itemType);
        }
    }
}

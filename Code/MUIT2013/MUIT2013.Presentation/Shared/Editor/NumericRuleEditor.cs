using MUIT2013.DataMining.AttributeRule;
using MUIT2013.Presentation.Shared.Collection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Presentation.Shared.Editor
{
    public class NumericRuleEditor : CollectionEditor
    {
        public NumericRuleEditor()
            : base(typeof(NumericRuleCollection))
        {

        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(SingleNumericRule), typeof(RangeNumericRule), typeof(AddNumericRule), typeof(MultiplyNumericRule) };
        }

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return base.GetEditStyle(context);
        }

        
    }
}

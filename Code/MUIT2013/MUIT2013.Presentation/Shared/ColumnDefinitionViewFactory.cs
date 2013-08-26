using MUIT2013.Data.Models;
using MUIT2013.DataMining.AttributeRule;
using MUIT2013.Presentation.Shared.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Presentation.Shared
{
    public class ColumnDefinitionViewFactory
    {
        private Dictionary<KeyValuePair<long, string>, ColumnDefinitionView> container = new Dictionary<KeyValuePair<long,string>,ColumnDefinitionView>();
        public  ColumnDefinitionView Create(ColumnDefinition columnDefinition)
        {
            var cacheKey = new KeyValuePair<long, string>(columnDefinition.Id, columnDefinition.ColumnType);
            ColumnDefinitionView cache = null; ;
            if (container.ContainsKey(cacheKey)) cache = container[cacheKey];
            else
            {
                if (columnDefinition.ColumnType == "String")
                {
                    StringRuleColumnDefinitionView srcdv = new StringRuleColumnDefinitionView(columnDefinition);
                    StringRuleFactory factory = new StringRuleFactory();
                    columnDefinition.MapRules.ForEach(k => {
                        IStringRule rule = factory.CreateRule(k.RuleType, k.RuleContent);
                        if(rule!=null) srcdv.RuleCollection.Add(rule);
                    });
                    cache = container[cacheKey] = srcdv;
                    
                }
                else if (columnDefinition.ColumnType == "Numeric")
                {                    
                    NumericRuleColumnDefinitionView srcdv = new NumericRuleColumnDefinitionView(columnDefinition);
                    NumericRuleFactory factory = new NumericRuleFactory();
                    columnDefinition.MapRules.ForEach(k =>
                    {
                        INumericRule rule = factory.CreateRule(k.RuleType, k.RuleContent);
                        if (rule != null) srcdv.RuleCollection.Add(rule);                        
                    });
                    cache = container[cacheKey] = srcdv;
                }
            }            
            return cache;
        }
    }
}

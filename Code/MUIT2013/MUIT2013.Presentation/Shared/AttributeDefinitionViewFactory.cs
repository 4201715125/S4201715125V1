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
    public class AttributeDefinitionViewFactory
    {
        private Dictionary<KeyValuePair<long, string>, AttributeDefinitionView> container = new Dictionary<KeyValuePair<long,string>,AttributeDefinitionView>();
        public  AttributeDefinitionView Create(AttributeDefinition AttributeDefinition)
        {
            var cacheKey = new KeyValuePair<long, string>(AttributeDefinition.Id, AttributeDefinition.AttributeDataType);
            AttributeDefinitionView cache = null; ;
            if (container.ContainsKey(cacheKey)) cache = container[cacheKey];
            else
            {
                if (AttributeDefinition.AttributeDataType == "String")
                {
                    StringRuleAttributeDefinitionView srcdv = new StringRuleAttributeDefinitionView(AttributeDefinition);
                    StringRuleFactory factory = new StringRuleFactory();
                    AttributeDefinition.MapRules.ForEach(k => {
                        IStringRule rule = factory.CreateRule(k.RuleType, k.RuleContent);
                        if(rule!=null) srcdv.RuleCollection.Add(rule);
                    });
                    cache = container[cacheKey] = srcdv;
                    
                }
                else if (AttributeDefinition.AttributeDataType == "Numeric")
                {                    
                    NumericRuleAttributeDefinitionView srcdv = new NumericRuleAttributeDefinitionView(AttributeDefinition);
                    NumericRuleFactory factory = new NumericRuleFactory();
                    AttributeDefinition.MapRules.ForEach(k =>
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{
    public interface INumericRule : IRule
    {
        double? Apply(double source);
    }
}

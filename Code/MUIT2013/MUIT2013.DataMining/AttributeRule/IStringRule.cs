using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining.AttributeRule
{
    public interface IStringRule : IGeneralRule
    {
        int? Apply(string source);
    }
}

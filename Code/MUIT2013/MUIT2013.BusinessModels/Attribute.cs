using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.BusinessModels
{
    public class Attribute
    {
        public string Name { get; set; }
        public List<int> Values { get; set; }
        string _name;
        public string Name {
            get
            {
                return _name;
            }
            set {
                // logic
                _name = value;
            }
        }
     
    }
}

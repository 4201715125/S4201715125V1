using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining
{
    public static class IndiscernibilityRelation
    {
        public static Func<double?[], double?[], bool> Equivalence(IEnumerable<int> B)
        {
            return (x, y) => B.All(a => x[a] == y[a]);
        }
    }
}

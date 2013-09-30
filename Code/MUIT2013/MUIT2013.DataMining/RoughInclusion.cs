using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining
{
    public static class RoughInclusion
    {
        public static readonly Func<IEnumerable<double?[]>, IEnumerable<double?[]>, double> Standard = (
            (X, Y) => X.Any() ? ((double)X.Intersect(Y).Count() / X.Count()) : 1
        );

        public static Func<IEnumerable<double?[]>, IEnumerable<double?[]>, double> Threshold(double t){
            if (t <= 0d || 0.5 <= t) return Standard;
            return (X,Y) => {
                var std = Standard(X, Y);
                if (std <= t) return 0;
                if ((1 - t) <= std) return 1;
                return (std - t) / (1 - 2 * t);
            };
        }
    }
}

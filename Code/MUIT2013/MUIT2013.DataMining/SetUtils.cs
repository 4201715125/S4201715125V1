using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.DataMining
{
    public static class SetUtils
    {
        public static Random random = new Random();
        public static bool SubsetEq<T> (this IEnumerable<T> A, IEnumerable<T> B)
        {
            return B.All(x => A.Contains(x));
        }

        public static IEnumerable<T> HConcat<T>(this IEnumerable<IEnumerable<T>> ls)
        {
            return ls.Aggregate((l1, l2) => l1.Concat(l2));
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}

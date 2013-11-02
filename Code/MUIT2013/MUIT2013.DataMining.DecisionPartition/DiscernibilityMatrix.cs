using System.Collections.Generic;
using System.Linq;

namespace MUIT2013.DataMining.DecisionPartition
{
    public class DiscernibilityMatrix
    {
        public double RuleValue { get; private set; }
        //Mảng 3 chiều: 
        public List<List<List<pairID>>> Matrix { get; private set; }
        public List<List<pairID>> PrimeImplicants { get; private set; }

        public DiscernibilityMatrix(DecisionSystem ds,IEnumerable<int> reduct, double ruleValue)
        {
            var sa = new StandardApproximationSpace(ds, reduct);
            RuleValue = ruleValue;
            var columns = sa.LowerApproximation(RuleValue);
            //so sánh với các phần tử không thuộc lower approximation
            var rows =
                ds.Universe.Where(p => columns.All(p2 => p2[0] != p[0])).Where(p => p[ds.DecisionAttributes[0]] != RuleValue);
            Matrix = new List<List<List<pairID>>>();
            foreach (var column in columns)
            {
                var dataRow = new List<List<pairID>>();
                foreach (var row in rows)
                {
                    var dataInstances = new List<pairID>();
                    foreach (var header in reduct)
                    {
                        if (row[header] != column[header])
                        {
                            dataInstances.Add(new pairID(header, int.Parse(column[header].ToString())));
                        }
                    }
                    if(dataInstances.Count!=0)
                        dataRow.Add(dataInstances);
                }
                if(dataRow.Count!=0)
                Matrix.Add(dataRow);
            }
        }

        public void GenerateRules()
        {
            PrimeImplicants = new List<List<pairID>>();
            foreach (var r in Matrix)
            {
                if(r.Count==0) continue;
                var j = new Johnson(r);
                foreach (var val in j.DNF)
                {
                    //primeImplicants.Add(val);
                    var n = IsContainedPrimeImplicant(val);
                    switch (n)
                    {
                        case -1:
                            PrimeImplicants.Add(val);
                            break;
                        case -2:
                            break;
                        default:
                            PrimeImplicants[n] = val;
                            break;
                    }

                }
                // primeImplicants=primeImplicants.OrderBy(x => x.Count).ToList();
            }

        }


        //Kiểm tra để loại bỏ các Implicants dư thừa
        //-1: add
        //-2: exist (do nothing)
        //n: replace current prime implicant at index n
        private int IsContainedPrimeImplicant(List<pairID> val)
        {
            for (var index = 0; index < PrimeImplicants.Count; index++)
            {
                var primeImplicant = PrimeImplicants[index];
                //chỉ xem xét các primeImplicant có nhiều phần tử
                if (primeImplicant.Count < val.Count) continue;
                var n = 0; //số lượng prime Implicants trùng nhau
                for (var i = 0; i < primeImplicant.Count; i++)
                {
                    for (var j = 0; j < val.Count; j++)
                    {
                        if (primeImplicant[i].IsEqual(val[j]))
                        {
                            n++;
                            break;
                        }
                    }
                }
                if (n != 0)
                {
                    if (primeImplicant.Count == val.Count)
                    {
                        if (n == primeImplicant.Count)
                        {
                            //duplicate
                            return -2;
                        }
                    }
                    else
                    {
                        if (val.Count == n)
                            //replace
                            return index;
                    }

                }
            }
            //add
            return -1;
        }
    }
}

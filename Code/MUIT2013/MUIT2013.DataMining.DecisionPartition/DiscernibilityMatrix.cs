using System.Collections.Generic;
using System.Linq;

namespace MUIT2013.DataMining.DecisionPartition
{
    public class DiscernibilityMatrix
    {
        public double RuleValue { get; private set; }
        //Mảng 3 chiều: 
        public List<List<List<pairID>>> Matrix { get; private set; }
        public List<List<pairID>> primeImplicants { get; private set; }

        public DiscernibilityMatrix(DecisionSystem DS, double ruleValue)
        {
            RuleValue = ruleValue;
            //loại bỏ các giá trị trùng nhau.
            var columns =
                (from c in DS.Universe
                 where (c[DS.DecisionAttribute] == RuleValue)
                 select c).Distinct().ToList();
            var rows = (from c in DS.Universe
                        where (c[DS.DecisionAttribute] != RuleValue)
                        select c).Distinct().ToList();
            Matrix = new List<List<List<pairID>>>();
            foreach (var column in columns)
            {
                var dataRow = new List<List<pairID>>();
                foreach (var row in rows)
                {
                    var dataInstances = new List<pairID>();
                    foreach (var header in DS.ConditionAttributes)
                    {
                        if (row[header] != column[header])
                        {
                            //var instance = new Instance();
                            //instance.Header=new Header {Id = header.Id, Name = header.Name, Unit = header.Unit};
                            //instance.Value = column[header.Id].ToString();
                            dataInstances.Add(new pairID(header, double.Parse(column[header].ToString())));
                        }
                    }
                    dataRow.Add(dataInstances);
                }
                Matrix.Add(dataRow);
            }
        }

        public void generateRules()
        {
            primeImplicants = new List<List<pairID>>();
            foreach (var r in Matrix)
            {

                var j = new Johnson(r);
                foreach (var val in j.DNF)
                {
                    //primeImplicants.Add(val);
                    var n = IsContainedPrimeImplicant(val);
                    switch (n)
                    {
                        case -1: primeImplicants.Add(val);
                            break;
                        case -2:
                            break;
                        default:
                            primeImplicants[n] = val;
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
            for (var index = 0; index < primeImplicants.Count; index++)
            {
                var primeImplicant = primeImplicants[index];
                //chỉ xem xét các primeImplicant có nhiều phần tử
                if (primeImplicant.Count < val.Count) continue;
                var n = 0;//số lượng prime Implicants trùng nhau
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

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
                        if(row[header]!=column[header])
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
            primeImplicants=new List<List<pairID>>();
            foreach(var r in Matrix)
            {
                var j = new Johnson(r);
                foreach (var val in j.DNF)
                {
                    if (isContainedPrimeImplicant(val)) continue;
                    primeImplicants.Add(val);
                }
            }
        }
        //Kiểm tra để loại bỏ các Implicants dư thừa
        private bool isContainedPrimeImplicant(List<pairID> val)
        {
            foreach (var primeImplicant in primeImplicants)
            {
                if(val.Count==primeImplicant.Count)
                {
                    var n = 0;
                    for (int i = 0; i < primeImplicant.Count; i++)
                    {
                        if (primeImplicant[i].IsContained(val))
                            n++;
                    }
                    if(n==primeImplicant.Count)
                        return true;
                }
                
            }
            return false;
        }
    }
}

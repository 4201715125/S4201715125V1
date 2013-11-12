using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MUIT2013.DataMining.Discretization
{
    public class Discretization
    {
        //dữ liệu cần mapping
        public List<int> Data { get; private set; }
        public List<string> OriginalData { get; private set; }
        //Bảng các giá trị mapping
        public List<string> MappedTable { get; private set; }
        public bool IsDiscreted { get; private set; }
        public int Header { get; private set; }
        public Discretization(IEnumerable<object> data, int header, bool isDiscrete)
        {
            OriginalData = data.Select(i => i.ToString()).ToList();
            IsDiscreted = isDiscrete;
            Header = header;
            Data = new List<int>();
            Run(OriginalData);
        }
        //data is the list of all values in table column
        public Discretization(IEnumerable<object> data, int header)
        {
            OriginalData = data.Select(i=>i.ToString()).ToList();
            CheckDiscretization();
            Header = header;
            Data = new List<int>();
            Run(OriginalData);
        }
        private void Run(List<string> data)
        {
            if (IsDiscreted)
            {
                //Giá trị rời rạc 
                MappedTable = (from c in data
                               orderby c ascending
                               select c).Distinct().ToList();
                foreach (var v in data)
                {
                    Data.Add(MappedTable.IndexOf(v));
                }
            }
            else
            {
                //giá trị liên tục. Tạo nhát cắt
                var mappedTable = (from c in data
                                   orderby decimal.Parse(c) ascending
                                   select c).Select(decimal.Parse).Distinct().ToList();
                for (var i = 1; i < mappedTable.Count(); i++)
                {
                    mappedTable[i - 1] = Decimal.Round((mappedTable[i - 1] + mappedTable[i]) / 2,
                                                       Constant.DecimalPlace);
                }
                MappedTable = mappedTable.ConvertAll(x => x.ToString(CultureInfo.InvariantCulture));
                MappedTable.RemoveAt(MappedTable.Count - 1);
                foreach (var v in data)
                {
                    for (var i = 0; i < mappedTable.Count(); i++)
                    {
                        if ((decimal.Parse(v) < mappedTable[i]))
                        {
                            if (i == 0)
                            {
                                Data.Add(i);
                                break;
                            }
                            Data.Add(i);
                            break;
                        }
                        if (i == mappedTable.Count - 1)
                        {
                            Data.Add(i);
                        }
                    }
                }
            }

        }
        private void CheckDiscretization()
        {
            //Nếu không phải kiểu decimal
            decimal n;
            var b = decimal.TryParse(OriginalData[0], out n);
            if (!b)
            {
                IsDiscreted = true;
                return;
            }
            var count = (from c in OriginalData
                         select c).Distinct().Count();
            IsDiscreted = count <= Constant.DiscreteThreshold;
        }
        //http://stackoverflow.com/questions/13610833/remove-multi-indexes-from-linq-list
        public void RemoveAllExcept(List<int> id)
        {
            var toRemove = new HashSet<int>(id);
            var item = MappedTable.Where((x, i) => toRemove.Contains(i)).ToList();
            MappedTable = new List<string>(item);


            Data = new List<int>();
            foreach (var d in OriginalData)
            {
                for (var i = 0; i < MappedTable.Count; i++)
                {
                    if (decimal.Parse(d) < decimal.Parse(MappedTable[i]))
                    {
                        Data.Add(i);
                        break;
                    }
                    if (i==MappedTable.Count-1)
                    {
                        Data.Add(i+1);
                    }
                }
            }
        }
    }
}

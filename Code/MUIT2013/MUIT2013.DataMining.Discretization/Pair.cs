using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MUIT2013.DataMining.Discretization
{
    class Pair
    {
        private string id1;
        private string id2;
        private string rawdata;
        public List<bool> dataList { get; private set; }
        public List<List<bool>> data { get; private set; }
        public Pair(double?[] dtr1, double?[] dtr2, DecisionSystem ds,int idIndex,List<Discretization> DisList)
        {
            dataList = new List<bool>();
            data = new List<List<bool>>();
            rawdata = "";
            for (var i = 0; i < dtr1.Count(); i++)
            {
                var tempData = new List<bool>();
                if (i == idIndex)
                {
                    id1 = dtr1[i].ToString();
                    id2 = dtr2[i].ToString();
                    continue;
                }
                if (i == ds.DecisionAttributes.ElementAt(0)) continue;
                var location = -1;
                int min, max;
                var bmin = int.TryParse(dtr1[i].ToString(), out min);
                if (!bmin) min = 0;
                var bmax = int.TryParse(dtr2[i].ToString(), out max);
                if (!bmax) max = 0;
                if (min > max)
                {
                    var temp = min;
                    min = max;
                    max = temp;
                }
                for (var j = 0; j < DisList.Count(); j++)
                {
                    if (j == i - 1) //current attribute
                    {
                        rawdata += new string('0', DisList[j].MappedTable.Count);
                        for (var k = 0; k < DisList[j].MappedTable.Count; k++)
                        {
                            location++;
                            if (DisList[j].IsDiscreted)
                            {
                                if (dtr1[i].ToString() == dtr2[i].ToString())
                                {
                                    tempData.Add(false);
                                    dataList.Add(false);
                                    continue;
                                }
                                var sb = new StringBuilder(rawdata);
                                sb[location] = '1';
                                rawdata = sb.ToString();
                                tempData.Add(true);
                                dataList.Add(true);
                                continue;
                            }
                            if (min <= k)
                            {
                                if (k >= max)
                                {
                                    tempData.Add(false);
                                    dataList.Add(false);
                                    continue;
                                }
                                var sb = new StringBuilder(rawdata);
                                sb[location] = '1';
                                rawdata = sb.ToString();
                                tempData.Add(true);
                                dataList.Add(true);
                            }
                            else
                            {
                                tempData.Add(false);
                                dataList.Add(false);
                            }
                        }
                    }
                    if (j < i - 1)
                    {
                        location += DisList[j].MappedTable.Count;
                        //skip
                    }
                    else
                    {
                        break;
                    }
                }
                data.Add(tempData);
            }
        }

        public bool IsZero()
        {
            return !rawdata.Contains("1");
        }

        public bool RemoveColumn(int id)
        {
            if ((dataList.Count == 0)||(dataList.Count<=id))
                return false;
            dataList.RemoveAt(id);
            rawdata=rawdata.Remove(id, 1);
            return true;
        }
    }
}

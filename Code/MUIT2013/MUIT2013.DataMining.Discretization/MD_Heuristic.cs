using System;
using System.Collections.Generic;
using System.Linq;
using MUIT2013.DataMining.DecisionPartition;

namespace MUIT2013.DataMining.Discretization
{
    
    public class MD_Heuristic
    {
        private readonly List<Pair> _pairs;
        private readonly List<List<string>> _cuts;//theo thứ tự các thuộc tính (danh sách các cột)
        private readonly List<List<int>> _counts;
        private List<pairID> _solution;
        public List<Discretization> ConditionList { get; private set; }
        public List<Discretization> DecisionList { get; private set; }
        private DecisionSystem ds;
        public MD_Heuristic( object[][] originalUniverse, int idIndex, int[] conditionAttributes,int[] decisionAttributes)
        {
            Numericalization(originalUniverse, idIndex, conditionAttributes, decisionAttributes);
            _pairs=new List<Pair>();
            var values = GetDecisionValues(ds);
            new List<string>();
            var equvalentClass = new List<IEnumerable<double?[]>>();
            foreach (var value in values)
            {
                equvalentClass.Add(GetEquivalentClass(value));
            }
            for (int i = 0; i < equvalentClass.Count-1; i++)
            {
               // Console.WriteLine("I: "+i.ToString());
                for (int j = i+1; j < equvalentClass.Count; j++)
                {
                    //Console.WriteLine("\tJ: "+j.ToString());
                    FillUp(equvalentClass[i],equvalentClass[j],idIndex);
                }
            }
            _cuts=new List<List<string>>();
            _counts=new List<List<int>>();
            for (int i = 0; i < ConditionList.Count; i++)
            {
                var tempCuts = new List<string>();
                var tempCounts = new List<int>();
                for (var j = 0; j < ConditionList[i].MappedTable.Count; j++)
                {
                    if(ConditionList[i].IsDiscreted)
                    {
                        tempCuts.Add(ConditionList[i].MappedTable[j]);
                        tempCounts.Add(-1);
                        continue;
                    }
                    tempCuts.Add(ConditionList[i].MappedTable[j]);
                    tempCounts.Add(GetCount(i,j));
                }
                _cuts.Add(tempCuts);
                _counts.Add(tempCounts);
            }
            RemoveDiscretedItems();
            run();
            UpdateDiscretization(_solution, idIndex);

        }
        private void Numericalization(object[][] originalUniverse, int idIndex, int[] conditionAttributes,int[] decisionAttributes)
        {
            ds=new DecisionSystem {ConditionAttributes = new int[conditionAttributes.Count()]};
            Array.Copy(conditionAttributes,ds.ConditionAttributes,conditionAttributes.Count());
            ds.DecisionAttributes = new int[decisionAttributes.Count()]; Array.Copy(decisionAttributes, ds.DecisionAttributes, decisionAttributes.Count());
            ds.Universe=new double?[originalUniverse.Count()][];
            ConditionList = new List<Discretization>();
            DecisionList = new List<Discretization>();
            var nCondition = 0;
            for (var i = 0; i < originalUniverse.Count(); i++)
            {
                ds.Universe[i] = new double?[originalUniverse[0].Count()];
                ds.Universe[i][idIndex] = double.Parse(originalUniverse[i][idIndex].ToString());
            }
            for (var i = 0; i < originalUniverse[0].Count(); i++)
            {
                
                if (i == idIndex)
                {
                    continue;
                }
                var column = (from row in originalUniverse.AsEnumerable()
                              select row[i]).ToList();
                Discretization dis;
                if (i == decisionAttributes[0])
                {
                    dis = new Discretization(column, ds.DecisionAttributes[0], true);
                    DecisionList.Add(dis);
                }
                else
                {
                    dis = new Discretization(column, ds.ConditionAttributes[nCondition]);
                    ConditionList.Add(dis);
                    nCondition++;
                }
                for (var j = 0; j < originalUniverse.Count(); j++)
                {
                    
                    //ds.Universe[j][i] = double.Parse(OriginalUniverse[j][i]);
                    ds.Universe[j][i] = dis.Data[j];
                }
            }
            UpdateAttributeDomain(idIndex);
        }
        public DecisionSystem UpdateDiscretization(List<pairID> p,int idIndex)
        {
            for (var i = 0; i < ConditionList.Count; i++)
            {
                var newDiscreted = (from c in p where c.AID == i orderby c.VID ascending select int.Parse(c.VID.ToString())).ToList();
               // Console.WriteLine("Dicrete " + i.ToString() + " " + newDiscreted.Count);
                //Không có nhát cắt nào bị loại bỏ. Giữ lại tất cả.
                if (newDiscreted.Count == 0) continue;
                //Tất cả nhát cắt bị loại bỏ -> Đúng: loại thuộc tính này khỏi bảng. Now (tạm): bỏ qua thuộc tính này
                if (newDiscreted.Count == ConditionList[i].MappedTable.Count) continue;
                ConditionList[i].RemoveAllExcept(newDiscreted);
            }
            for (var i = 0; i < ds.Universe[0].Count(); i++)
            {
                if ((i == idIndex) || (i == ds.DecisionAttributes.ElementAt(0)))
                {
                    continue;
                }
                foreach (var discretization in ConditionList)
                {
                    if (discretization.Header == i)
                    {
                        for (var j = 0; j < discretization.Data.Count; j++)
                        {
                            ds.Universe[j][i] = discretization.Data[j];
                        }
                        break;
                    }
                }
            }
            UpdateAttributeDomain(idIndex);
            return ds;
        }
        void UpdateAttributeDomain(int idIndex)
        {
            ds.AttributesDomain = new double[ds.Universe[0].Count()][];
            for (var i = 0; i < ds.Universe[0].Count(); i++)
            {
                if (i == idIndex)
                {
                    ds.AttributesDomain[i] = null;
                    continue;
                }
                ds.AttributesDomain[i] = (from row in ds.Universe.AsEnumerable()
                                          select double.Parse(row[i].ToString())).Distinct().OrderBy(x => x).ToArray();
            }

        }
        //Xóa các pair phân biệt được bởi các thuộc tính rời rạc
        void RemoveDiscretedItems()
        {
            for (int x = 0; x < ConditionList.Count; x++)
            {
                for (var j = 0; j < ConditionList[x].MappedTable.Count; j++)
                {
                    if (ConditionList[x].IsDiscreted)
                    {
                        var col = new pairID(x, j);
                        for (var i = 0; i < _pairs.Count; i++)
                        {
                            if (_pairs[i].data[(int)col.AID][(int)col.VID])
                            {
                                UpdateCounts(col, i);
                                _pairs.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
            }
        }

        void run()
        {
            _solution=new List<pairID>();
            while (_pairs.Count != 0)
            {
               // Console.WriteLine("Pair: "+_pairs.Count);
                int AID=0, VID=0;
                FindColumn(ref AID, ref VID);
                var col = new pairID(AID, VID);
                for (var i = 0; i < _pairs.Count; i++)
                {
                    if (_pairs[i].data[(int)col.AID][(int)col.VID])
                    {
                        UpdateCounts(col, i);
                        _pairs.RemoveAt(i);
                        i--;
                    }
                }
                _solution.Add(col);
            }
        }
        void UpdateCounts(pairID col, int location)
        {
            for (var i = 0; i < _counts.Count; i++)
            {
                for (var j = 0; j < _counts[i].Count; j++)
                {
                    if ((i == col.AID) && (j == col.VID))
                    {
                        //Khi remove thì count=-1
                        _counts[i][j] = -1;
                        continue;
                    }
                    if (_pairs[location].data[i][j])
                    {
                        _counts[i][j]--;
                    }
                }
            }
        }

        //Tìm chọn cột phù hợp
        void FindColumn(ref int AID, ref int VID)
        {
            AID = VID = 0;
            for (var i = 0; i < _counts.Count; i++)
            {
                for (var j = 0; j < _counts[i].Count; j++)
                {
                    if ((i == j) && (i == 0)) continue;
                    if(_counts[i][j]<=-1) continue;

                    if (_counts[AID][VID] < _counts[i][j])
                    {
                        AID = i;
                        VID = j;
                        continue;
                    }
                    //Chọn attibute có ít nhát cắt nhất.
                    if (_counts[AID][VID] == _counts[i][j])
                    {
                        var pairTemp = Select(new pairID (AID, VID ), new pairID(i,j));
                        AID = (int)pairTemp.AID;
                        VID = (int)pairTemp.VID;
                        //if (counts[AID].Count > counts[i].Count)
                        //{
                        //    AID = i;
                        //    VID = j;
                        //}
                    }

                }
            }
        }
        //Chọn ra thuộc tính còn nhiều nhát cắt nhất
        private pairID Select(pairID a,pairID b)
        {
            var counta = _counts[(int)a.AID].Count;
            var countb = _counts[(int)b.AID].Count;
            foreach (var c in _counts[(int)a.AID])
            {
                if (c <= -1) counta--;
            }
            foreach (var c in _counts[(int)b.AID])
            {
                if (c <= -1) countb--;
            }
            //<: chọn thuộc tính có nhiều nhát cắt nhất
            //>: chọn thuộc tính có ít nhát cắt nhất
            if (counta < countb)
                return b;
            return a;
        }
        private int GetCount(int AID,int VID)
        {
            return _pairs.Count(pair => pair.data[AID][VID]);
        }

        private IEnumerable<double> GetDecisionValues(DecisionSystem des)
        {
          return (from row in des.Universe.AsEnumerable() 
           select double.Parse(row[des.DecisionAttributes[0]].ToString())).Distinct().ToList();

        }
        private IEnumerable<double?[]> GetEquivalentClass(double decisionValue)
        {
            return (from r in ds.Universe.AsEnumerable()
                       where r[ds.DecisionAttributes.ElementAt(0)] == decisionValue
                       select r).ToArray();
        }
        private void FillUp(IEnumerable<double?[]> dtr1, IEnumerable<double?[]> dtr2,int idIndex)
        {
            foreach (var j in dtr1)
            {
                foreach (var r in dtr2)
                {
                    var pair = new Pair(j, r, ds,idIndex,ConditionList);
                    if(!pair.IsZero()) _pairs.Add(pair);
                }
            }
        }
    }
}

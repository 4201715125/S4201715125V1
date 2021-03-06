﻿using System.Collections.Generic;
using System.Linq;

namespace MUIT2013.DataMining.DecisionPartition
{
    internal class Johnson
    {
        public List<List<pairID>> DNF { get; private set; }
        //CNF: AND<OR>
        //DNF: OR<AND>
        //tranfer: AND<OR>
        public Johnson(IList<List<pairID>> CNF)
        {
            var list = new List<pairID>();
            var count = new List<int>();
            DNF = new List<List<pairID>>();
            var tranfer = new List<List<pairID>>();
            //get the full list
            for (int index = 0; index < CNF.Count; index++)
            {
                if (isExisted(index, CNF))
                {
                    //Remove duplicated items in CNF
                    CNF.RemoveAt(index);
                    index--;
                    continue;
                }
                var val = CNF[index];
                foreach (var item in val)
                {
                    if (list.Count == 0)
                    {
                        list.Add(item);
                        count.Add(1);
                        continue;
                    }
                    var added = false;
                    for (var i = 0; i < list.Count; i++)
                    {
                        if (list[i].IsEqual(item))
                        {
                            added = true;
                            count[i]++;
                            break;
                        }
                    }
                    if (!added)
                    {
                        list.Add(item);
                        count.Add(1);
                    }
                }
            }

            //run and remove
            while (CNF.Count != 0)
            {
                var max = 0;
                for (var i = 1; i < count.Count; i++)
                {
                    if (count[max] < count[i])
                    {
                        max = i;
                    }
                }
                //danh sách các phần tử bằng với phần tử lớn nhất.
                var equalList = new List<int>();
                for (var i = 0; i < count.Count; i++)
                {
                    if ((count[max] == count[i]) && (max != i))
                    {
                        equalList.Add(i);
                    }
                }
                for (var i = 0; i < CNF.Count; i++)
                {
                    for (var j = 0; j < CNF[i].Count; j++)
                    {
                        if (CNF[i][j].IsEqual(list[max]))
                        {
                            //loại bỏ các phần tử ko đi chung với phần tử lớn nhất
                            for (var k = 0; k < equalList.Count; k++)
                            {
                                if (!list[equalList[k]].IsContained(CNF[i]))
                                {
                                    equalList.RemoveAt(k);
                                    k--;
                                }

                            }

                            //Cập nhật count
                            for (var l = 0; l < count.Count; l++)
                            {
                                if (l == max)
                                {
                                    count[l] = -1;
                                    continue;
                                }
                                if (list[l].IsContained(CNF[i]))
                                {
                                    count[l]--;
                                }
                            }
                            CNF.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
                var temp = new List<pairID> {list[max]};
                //count[max] = -1;
                foreach (var e in equalList)
                {
                    temp.Add(list[e]);
                    //count[e] = -1;
                }
                tranfer.Add(temp);
            }
            //chuyển tranfer dạng AND<OR> thành DNF dạng OR<AND>
            DNF = retrieve(0, tranfer);
            SortDNF();
        }

        private bool isExisted(int i, IList<List<pairID>> CNF)
        {
            for (var j = 0; j < i; j++)
            {
                if (CNF[i].Count != CNF[j].Count) continue;
                var isDuplicated = false;
                for (var k = 0; k < CNF[j].Count; k++)
                {
                    if (!CNF[i][k].IsEqual(CNF[j][k]))
                    {
                        isDuplicated = false;
                        break;
                    }
                    isDuplicated = true;
                }
                if (isDuplicated) 
                    return true;
            }
            return false;
        }
        private void SortDNF()
        {
            for (var i = 0; i < DNF.Count; i++)
            {
                DNF[i] = DNF[i].OrderBy(x => x.AID).ToList();
            }
            //DNF = DNF.OrderBy(x => x.Count).ToList();
        }
        private List<List<pairID>> retrieve(int id,List<List<pairID>> tranfer)
        {
            var fulllist = new List<List<pairID>>();
            if (id == tranfer.Count)
                return fulllist;
            for(var i=0;i<tranfer[id].Count;i++)
            {
                var rs = retrieve(id + 1, tranfer);
                if(rs.Count==0)
                {
                    var temp = new List<pairID> { tranfer[id][i] };
                    fulllist.Add(temp);
                    continue;
                }
                foreach (var r in rs)
                {
                    var temp = new List<pairID> { tranfer[id][i] };
                    temp.AddRange(r);
                    fulllist.Add(temp);
                }
            }
            return fulllist;
        }
    }
}
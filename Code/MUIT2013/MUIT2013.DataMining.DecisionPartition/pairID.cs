using System.Collections.Generic;

namespace MUIT2013.DataMining.DecisionPartition
{
    public class pairID
    {
        public double AID;
        public double VID;
        public pairID(double aid, double vid)
        {
            AID = aid;
            VID = vid;
        }
        public bool IsEqual(pairID p)
        {
            return (p.AID == AID) && (p.VID == VID);
        }
        public bool IsContained(List<pairID> p)
        {
            foreach (var pairId in p)
            {
                if ((pairId.AID == AID) && (pairId.VID == VID))
                    return true;
            }
            return false;
        }
    }
}

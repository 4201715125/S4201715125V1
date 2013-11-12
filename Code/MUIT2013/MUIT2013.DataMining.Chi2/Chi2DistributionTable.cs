using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.Utils;

namespace MUIT2013.DataMining.Chi2
{
    public static class Chi2DistributionTable
    {
        private static Double[][] Chi2DataTable = LoadChi2Table();
        public static List<Double> Pvalues { get; private set; }
        public static List<Double> DFs { get; private set; }

        private static Double[][] LoadChi2Table()
        {
            // use Chi2DistributionTable_2 because it has Pvalue 0.5
            string[] lines = MUIT2013.DataMining.Chi2.Properties.Resources
                .Chi2DistributionTable_2.Split('\n');
            List<double[]> table = new List<double[]>();
            DFs = new List<double>();

            // get Pvalues
            Pvalues = lines[0].Split(',')
                .SubArray(1) // ignore elements at index 0
                .Select(x => Double.Parse(x))
                .ToList();

            foreach(string line in lines.SubArray(1))
            {
                string[] data = line.Split(',');
                DFs.Add(Double.Parse(data[0]));
                table.Add(data.SubArray(1).Select(x => Double.Parse(x)).ToArray());
            }
            return table.ToArray();
        }

        public static double GetValueAt(double Pvalue, double DF){
            int PvalueIdx = Pvalues.IndexOf(Pvalue);
            int dfIdx = DFs.IndexOf(DF);
            return Chi2DataTable[dfIdx][PvalueIdx];
        }
    }
}

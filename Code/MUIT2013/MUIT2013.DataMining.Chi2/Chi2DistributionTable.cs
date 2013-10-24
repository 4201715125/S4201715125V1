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
        private static Double[][] Chi2DataTable;
        private static List<Double> Pvalues;
        private static List<Double> DFs;

        private static void LoadChi2Table()
        {
            string[] lines = MUIT2013.DataMining.Chi2.Properties.Resources
                .ChiSquareDistributionTable.Split('\n');
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
            Chi2DataTable = table.ToArray();
        }

        public static double GetValueAt(double Pvalue, double DF){
            if (Chi2DataTable == null)
                LoadChi2Table();
            int PvalueIdx = Pvalues.IndexOf(Pvalue);
            int dfIdx = DFs.IndexOf(DF);
            return Chi2DataTable[dfIdx][PvalueIdx];
        }
    }
}

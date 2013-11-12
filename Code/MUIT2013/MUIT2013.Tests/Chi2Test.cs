using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.DataMining;
using MUIT2013.DataMining.Chi2;

namespace MUIT2013.Tests
{
    public class Chi2Test
    {

        public static void perfom() {
            //var chi2val = SpecialFunction.chisq(1, 0.5);
            //return;

            // load file
            var infSys = new DecisionSystem();
            string filename = @"data/iris.csv";
            //string filename = @"data/input1.csv";
            StreamReader reader = new StreamReader(filename);
            infSys.ConditionAttributes = new int[] { 1, 2, 3, 4 };
            infSys.DecisionAttribute = 5;

            // Universe
            reader.ReadLine(); // ignore header line
            List<double?[]> objs = new List<double?[]>();
            List<double?> obj;
            int idTracker = 0;
            Dictionary<int, List<double>> attrDomain = new Dictionary<int,List<double>>();
            for(int i = 0; i < 6; i ++)
                attrDomain.Add(i, new List<double>());
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                obj = new List<double?>();
                obj.Add(++idTracker);
                var numbers = line.Split(',')
                    .Select((x, i) => new {str = x, idx = i})
                    .Where(o => o.idx < 5)
                    .Select(t => (double?)Double.Parse(t.str))
                    .ToList();

                obj.AddRange(numbers);
                objs.Add(obj.ToArray());
                // get value domain of all attrs
                // ignore the id (i = 0) of object
                for (int i = 1; i < 6; i++) {
                    if(obj[i] != null && !attrDomain[i].Contains((double)obj[i]) )
                        attrDomain[i].Add((double)obj[i]);
                }
            }
            infSys.Universe = objs.ToArray();
            infSys.AttributesDomain = attrDomain
                .OrderBy(p => p.Key)
                .Select(p => p.Value.ToArray())
                .ToArray();
            reader.Close();
            printDS(infSys, @"data/inputDS.txt");

            // get begin inconsistency rate
            var apprSpace = new StandardApproximationSpace(infSys, infSys.ConditionAttributes);
            double rate = apprSpace.InConsistencyRate();
            
            // Process data
            Chi2Discretizer dis = new Chi2Discretizer(infSys);
            dis.Process();

            // Get resulted DS
            var processedDS = dis.BuildProcessedDS();
            printDS(processedDS, @"data/processedDS.txt");
            
            // get rate after processed
            var apprSpace2 = new StandardApproximationSpace(processedDS, processedDS.ConditionAttributes);
            double rate2 = apprSpace2.InConsistencyRate();
            Console.WriteLine(rate2);


            // Print report
            StreamWriter writer = new StreamWriter(@"data/report.txt", false);

            writer.WriteLine("Data file: " + filename);
            writer.WriteLine("- Item Count: " + infSys.Universe.Length);
            writer.WriteLine("- Original condition attributes: " + string.Join(",", infSys.ConditionAttributes));
            writer.WriteLine("- Selected attributes (reduct): " + string.Join(",", dis.SelectedConditionAttrs));
            writer.WriteLine("- Original domain count: ");
            foreach (int attr in infSys.ConditionAttributes) {
                writer.Write(attr + ": ");
                writer.WriteLine(infSys.AttributesDomain[attr].Length.ToString());
            }
            writer.WriteLine("- Processed domain count: ");
            foreach (int attr in processedDS.ConditionAttributes)
            {
                writer.Write(attr + ": ");
                writer.WriteLine(processedDS.AttributesDomain[attr].Length.ToString());
            }
            writer.WriteLine("- Removed attributes: " + string.Join(",", dis.RemovedConditionAttrs));
            writer.WriteLine("- Original in-consistency rate: " + rate);
            writer.WriteLine("- Processed in-consistency rate: " + rate2);
            if (rate2 > 0)
            { 
                var inConClasses = apprSpace2.IndiscernibilityClasses()
                    .Where(X => ApproximationSpace.GetAllDecisionClasses(X, processedDS.DecisionAttribute).Count() > 1);
                writer.WriteLine("- All in-consistent classes:");
                int count = 0;
                foreach(var klass in inConClasses){
                    count++;
                    writer.WriteLine("\t+ Class: " + count);
                    foreach (var o in klass) { 
                        writer.WriteLine("\t\t" + string.Join(",", o));
                    }
                }
            }
            writer.Close();
        }

        public static void printDS(DecisionSystem ds, string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            foreach (var obj in ds.Universe) {
                var line = obj
                    .Select(x => x.ToString())
                    .Aggregate((joinstr, str) => joinstr + "\t" + str);
                writer.WriteLine(line);
            }
            writer.Close();
        }
    }
}

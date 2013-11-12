using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.DataMining;
using MUIT2013.Utils;
using Newtonsoft.Json;

namespace MUIT2013.DataMining.Chi2
{
    public class Chi2Discretizer
    {
        #region Properties

        public DecisionSystem DS { get; private set; }
        
        protected ApproximationSpace apprSpace;
        public Double Delta { get; private set; }
        public Double StartSigLevel = 0.5;

        public List<double> DecisionClasses { get; private set; }
        public int DegreeOfFreedom { get; private set; }

        // -- this is instance cached variables --
        private double?[][] crnUniverse;
        private Dictionary<int, List<double>> processedAttrDomains = new Dictionary<int, List<double>>();
        // removed interval (attribute value) => replaced interval (final one)
        private Dictionary<int, Dictionary<double, double>> intervalMapper = new Dictionary<int, Dictionary<double, double>>();
        // Dictionary<Attr, Aij table of Attr>
        private Dictionary<int, int[][]> CachedAijs = new Dictionary<int, int[][]>();

        // Current Condition Attributes
        public int[] SelectedConditionAttrs { 
            get {
                return this.DS.ConditionAttributes
                    .Where(attr => this.processedAttrDomains[attr].Count > 1) // only attrs that have more than one interval
                    .ToArray();
            }
        }

        public IEnumerable<int> RemovedConditionAttrs {
            get {
                return this.DS.ConditionAttributes.Except(this.SelectedConditionAttrs);
            }
        }

        // Dictionary<Attr, List of Chi2 statistics of Attr's intervals>
        private Dictionary<int, List<double>> Chi2Statistics = new Dictionary<int, List<double>>();

        #endregion

        #region Methods
        
        public Chi2Discretizer(DecisionSystem _DS) {
            this.DS = _DS;
            this.DecisionClasses = DS.AttributesDomain[DS.DecisionAttribute].OrderBy(x => x).ToList();
            this.DegreeOfFreedom = DecisionClasses.Count - 1;

            this.InitCache();

            this.Delta = this.CalcCurrentInconsistencyRate();
            //TODO: If Ei < 5 , raise an exception
        }

        private void InitCache() {
            this.crnUniverse = this.DS.Universe.Select(obj => (double?[])(obj.Clone())).ToArray();

            this.processedAttrDomains.Clear();
            for (int attrIdx = 0; attrIdx < DS.AttributesDomain.Length; attrIdx++)
                this.processedAttrDomains.Add(attrIdx, DS.AttributesDomain[attrIdx].OrderBy(x => x).ToList());

            this.intervalMapper.Clear();
            foreach (int Attr in DS.ConditionAttributes.OrderBy(x => x))
                this.intervalMapper.Add(Attr, new Dictionary<double, double>());

            this.CachedAijs.Clear();
        }

        protected virtual double CalcCurrentInconsistencyRate() {
            return new StandardApproximationSpace(BuildProcessedDS(), this.DS.ConditionAttributes)
                .InConsistencyRate();
        }

        public DecisionSystem BuildProcessedDS()
        {
            DecisionSystem newDS = new DecisionSystem();
            newDS.AttributesDomain = this.processedAttrDomains
                .OrderBy(pair => pair.Key)
                .Select(pair => pair.Value.ToArray())
                .ToArray();
            newDS.ConditionAttributes = this.SelectedConditionAttrs;
            newDS.DecisionAttribute = this.DS.DecisionAttribute;
            newDS.DecisionAttributes = this.DS.DecisionAttributes;
            newDS.Universe = this.crnUniverse;

            return newDS;
        }

        // ith interval, jth class
        protected int[][] GetAijsOf(int Attr)
        {
            if (!CachedAijs.ContainsKey(Attr)) {
                List<double> AttrDomain = processedAttrDomains[Attr];
                int attrValCount = AttrDomain.Count;
                int classCount = DecisionClasses.Count;
                // init counter
                int[][] Aij = new int[attrValCount][];
                for (int i = 0; i < attrValCount; i++)
                {
                    Aij[i] = new int[classCount];
                    for (int j = 0; j < classCount; j++)
                        Aij[i][j] = 0;
                }
                int attrIdx, classIdx;
                foreach (var obj in DS.Universe)
                {
                    attrIdx = AttrDomain.IndexOf((double)obj[Attr]);
                    classIdx = DecisionClasses.IndexOf((double)obj[DS.DecisionAttribute]);
                    Aij[attrIdx][classIdx] += 1;
                }
                CachedAijs.Add(Attr, Aij);
            }
            return CachedAijs[Attr];
        }

        private double CalcChi2Value(int Attr, int firstIntervalIdx)
        {
            int[][] Aijs = GetAijsOf(Attr);
            int domainCount = processedAttrDomains[Attr].Count;
            int[] Cjs = new int[DecisionClasses.Count]; // idx: j => value: ith count
            int[] Ris = new int[2];
            int N = 0;
            int i, j, intervalIdx;
            double chi2Value;
            double[,] Eijs;

            // FIXME: test
            //logLine("\n\n=====\nInterval pair:" + firstIntervalIdx + " - Values: " +
            //    processedAttrDomains[Attr][firstIntervalIdx] + ", " +
            //    processedAttrDomains[Attr][firstIntervalIdx + 1]
            //    );
            //logLine("\n-Aijs:");
            for (i = 0; i < 2; i++)
            {
                intervalIdx = firstIntervalIdx + i; 
                Ris[i] = Aijs[intervalIdx].Sum();
                N += Ris[i];
                for (j = 0; j < DecisionClasses.Count; j++)
                {
                    Cjs[j] += Aijs[intervalIdx][j];

                    //log(Aijs[intervalIdx][j] + "\t");
                }
                //logLine("");
            }

            //logLine("\n-Ri:");
            //foreach (var r in Ris)
            //    log(r + "\n");

            //logLine("\n-Cj:");
            //foreach (var c in Cjs)
            //    log(c + "\n");

            //logLine("\n-Eijs:");

            // Calc Eij and Chi2Values
            Eijs = new double[2, DecisionClasses.Count];
            chi2Value = 0;
            for (i = 0; i < 2; i++)
            {
                intervalIdx = firstIntervalIdx + i; 
                for (j = 0; j < DecisionClasses.Count; j++)
                {
                    Eijs[i, j] = (Ris[i] * Cjs[j]) / (Double)N;
                    if (Eijs[i, j] == 0) Eijs[i, j] = 0.1;
                    chi2Value += Math.Pow((Aijs[intervalIdx][j] - Eijs[i, j]), 2) / Eijs[i, j];

                    //log(Eijs[i, j] + "\t");
                }
                //logLine("");
            }

            //logLine("\n-X: " + chi2Value);
            return chi2Value;
        }

        private List<double> MergeIntervals(int Attr, int firstIntervalIdx, List<double> chi2Values)
        {
            int secondIntervalIdx = firstIntervalIdx + 1;
            // Merge Aijs
            List<int[]> Aijs = GetAijsOf(Attr).ToList(); // use linq
            for (int j = 0; j < DecisionClasses.Count; j++)
            {
                Aijs[firstIntervalIdx][j] += Aijs[secondIntervalIdx][j];
            }
            Aijs.RemoveAt(secondIntervalIdx);
            this.CachedAijs[Attr] = Aijs.ToArray();

            // update intervalMapper
            double removedInterval = this.processedAttrDomains[Attr][secondIntervalIdx];
            double replacedInterval = this.processedAttrDomains[Attr][firstIntervalIdx];
            this.intervalMapper[Attr].Add(removedInterval, replacedInterval);

            // Update crnUniverse: replace intervals of Attr in each obj
            foreach (var obj in this.crnUniverse) {
                if (obj[Attr] == removedInterval)
                    obj[Attr] = replacedInterval;
            }

            // update processedAttrDomains
            this.processedAttrDomains[Attr].RemoveAt(secondIntervalIdx);

            // update chi2Values
            chi2Values.RemoveAt(firstIntervalIdx);
            if (firstIntervalIdx > 0)
                chi2Values[firstIntervalIdx - 1] = CalcChi2Value(Attr, firstIntervalIdx - 1);

            if (firstIntervalIdx < this.processedAttrDomains[Attr].Count - 1)
                chi2Values[firstIntervalIdx] = CalcChi2Value(Attr, firstIntervalIdx);

            return chi2Values;
        }

        public List<double> MergeAllPossibleIntervals(int Attr, List<double> chi2Values, double sigLevel)
        {
            while (chi2Values.Count > 0)
            {
                double minChi2Val = chi2Values.Min();
                int minIdx = chi2Values.IndexOf(minChi2Val);
                if (this.IsExceedThreshold(minChi2Val, sigLevel))
                    break;

                chi2Values = this.MergeIntervals(Attr, minIdx, chi2Values);
            }
            return chi2Values;
        }


        // return -1 for no more descese Significant level
        private double DecreSigLevel(double sigLevel)
        {
            double deltaSigLevel = 0.01;
            if (sigLevel <= 0)
                return -1;
            else
            {
                double newSig = sigLevel - deltaSigLevel;
                return (newSig < 0) ? 0 : newSig;
            }
        }

        // prepare list of chi2Value and return a copy of this list for further process
        private List<double> InitChi2ValuesFor(int Attr)
        {
            List<double> chi2Values = new List<double>();
            for (int firstIdx = 0; firstIdx < processedAttrDomains[Attr].Count - 1; firstIdx++)
            {
                chi2Values.Add(CalcChi2Value(Attr, firstIdx));
            }
            return chi2Values;
        }

        private bool IsExceedThreshold(double chi2Value, double targetSigLevel)
        {
            double targetPvalue = 1 - targetSigLevel;
            double Pvalue = SpecialFunction.chisq(this.DegreeOfFreedom, chi2Value); // this function produce Pvalue
            return (Pvalue > targetPvalue);
        }

        // check whether new inconsistency rate will exceed the original rate
        private bool WillNextMergedIntervalIncreaseInConsisRate(int targetAttr, List<double> chi2Values)
        {
            int firstIntervalIdx = chi2Values.IndexOf(chi2Values.Min());
            double replacedInterval = this.processedAttrDomains[targetAttr][firstIntervalIdx];
            double removedInterval = this.processedAttrDomains[targetAttr][firstIntervalIdx + 1];
            var newUniverse = this.crnUniverse.Select(x => (double?[])x.Clone()).ToArray();
            for (int i = 0; i < newUniverse.Count();  i++)
            {
                var newObj = newUniverse[i];
                //FIXME: for test
                if (newObj[0] == 77)
                    Console.WriteLine();
                if (newObj[targetAttr] == removedInterval)
                    newObj[targetAttr] = replacedInterval;
            }

            var newDS = new DecisionSystem(newUniverse.ToArray(), this.SelectedConditionAttrs, this.DS.DecisionAttribute);
            var newApprSpace = new StandardApproximationSpace(newDS, this.SelectedConditionAttrs);
            var newRate = newApprSpace.InConsistencyRate();

            return (newRate > this.Delta);
        }

        public DecisionSystem Process() { 
            double sigLevel = this.StartSigLevel;
            List<double> chi2Values;
            double sigLevel0 = Double.MaxValue;
            
            //FIXME
            logLine("\n\n\n========NEW TEST=======");
            // Phrase 1:
            logLine("=== Phrase 1: ===");
            do
            {
                foreach (int Attr in this.SelectedConditionAttrs) // TODO: should be only numeric attributes
                {
                    // Init chi2Value for all interval pairs, 
                    chi2Values = InitChi2ValuesFor(Attr);

                    //FIXME: only for test
                    int count = chi2Values.Count;

                    chi2Values = MergeAllPossibleIntervals(Attr, chi2Values, sigLevel);

                    // Log result of this step
                    if (chi2Values.Count < count)
                    {
                        logLine("\n----- After merge -----");
                        LogState(Attr, chi2Values, sigLevel);
                    }
                }

                if(this.CalcCurrentInconsistencyRate() <= this.Delta)
                    sigLevel0 = sigLevel;
                else
                    break;

                // sigLevel is -1 when can't find the lower sigLevel (Pvalue) in Chi2DistributionTable
                sigLevel = DecreSigLevel(sigLevel);
            } while (sigLevel != -1);

            // Refresh cached data
            this.InitCache();

            // Phrase 2:
            logLine("\n\n##########=== Phrase 2: ===########");
            List<int> mergableAttrs = new List<int>(this.SelectedConditionAttrs);
            Dictionary<int, List<double>> chi2ValueTracker = new Dictionary<int, List<double>>();
            double[] sigLevels = this.SelectedConditionAttrs.Select(x => sigLevel0).ToArray();
            for (int i = 0; i < mergableAttrs.Count; i++)
            {
                int attr = mergableAttrs[i];
                chi2Values = InitChi2ValuesFor(attr);
                // Come back the good state from phrase 1
                chi2Values = MergeAllPossibleIntervals(attr, chi2Values, sigLevels[i]);
                logLine("\n----- After merge -----");
                LogState(attr, chi2Values, sigLevels[i]);
                chi2ValueTracker.Add(attr, chi2Values);
            }
            int tryTime = 2;
            while (--tryTime > 0)
            {
                for (int i = 0; i < mergableAttrs.Count; i++)
                {
                    int attr = mergableAttrs[i];
                    chi2Values = chi2ValueTracker[attr];
                    logLine("\n\n ===== *** Refine for attr: " + attr + "*** ====");
                    while (sigLevels[i] != -1 && chi2Values.Count > 0)
                    {
                        //FIXME: only for test
                        int count = chi2Values.Count;

                        double minChi2Val = chi2Values.Min();
                        int minIdx = chi2Values.IndexOf(minChi2Val);

                        if (this.WillNextMergedIntervalIncreaseInConsisRate(attr, chi2Values))
                        {
                            logLine("Breaking cause by In-consistency");
                            logLine("> Current In-consistency: " + this.CalcCurrentInconsistencyRate());
                            break;
                        }

                        if (this.IsExceedThreshold(minChi2Val, sigLevels[i]))
                        {
                            logLine("ExceedThreshold");
                            sigLevels[i] = this.DecreSigLevel(sigLevels[i]);
                            continue;
                        }

                        chi2Values = this.MergeIntervals(attr, minIdx, chi2Values);

                        // Log result of this step
                        logLine("\n----- After merge -----");
                        LogState(attr, chi2Values, sigLevels[i]);

                    } 
                }
            }
           
            //FIXME
            writer.Close();

            // finalize
            return this.BuildProcessedDS();
        }

        #endregion

        #region Logging
        private static StreamWriter writer = new StreamWriter(@"data/output.log");
        public static void log(string msg)
        {
            Console.Write(msg);
            writer.Write(msg);
        }
        public static void logLine(string msg) {
            Console.WriteLine(msg);
            writer.WriteLine(msg);
        }

        public void LogState(int Attr, List<double> chi2Values, double sigLevel){
            // Calc Cjs for each intervals
            // print as a table: interval, C1, C2, ..., Cj, chi2Value
            List<double> domain = this.processedAttrDomains[Attr];
            int[][] Aijs = this.GetAijsOf(Attr);
            List<string> msgEl = new List<string>();
            logLine("\n\n=== Attribute: " + Attr + " at SigLevel: " + sigLevel + " => P-value: " + (1 - sigLevel) + "===");
            logLine("===Domain count: " + domain.Count);
            for (int i = 0; i < domain.Count; i++) {
                msgEl.Clear();
                foreach (var classCount in Aijs[i]) {
                    msgEl.Add(classCount.ToString());
                }
                if (i < domain.Count - 1)
                {
                    msgEl.Add(Math.Round((decimal)chi2Values[i], 4).ToString() + "\t");
                    //Decimal Pvalue = Math.Round((decimal)SpecialFunction.chisq(this.DegreeOfFreedom, chi2Values[i]), 4);
                    double Pvalue = SpecialFunction.chisq(this.DegreeOfFreedom, chi2Values[i]);
                    msgEl.Add(Pvalue.ToString());
                }

                logLine(msgEl.Aggregate(domain[i].ToString(),(joinStr, msg) => joinStr + "\t" + msg));
            }
            logLine("*** In-consistency rate: " + this.CalcCurrentInconsistencyRate());
        }

        #endregion
    }
}

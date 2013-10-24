using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.DataMining;
using MUIT2013.Utils;

namespace MUIT2013.DataMining.Chi2
{
    public class Chi2Discretizer
    {
        /* TODO:
         * Dictionary<Double, Double[]> for mapping discreted values with removed values
         * Double SigLevel0
         * Double[] SigLevels
         * private Double[] ChiSqStatistics
         * void Sort(attr){} --> sort DS
         * void ChiSqInitialize(attr){} --> init ChiSqStatistics
         * Bool Merge(attr){}
         * void ChiSqUpdate(attr){} --> update ChiSqStatistics
         * void process() {} --> main function
         * String State --> init, processing, finish, updated
         * int[] SelectedAttrs
         * int[] RemovedAttrs
         */
        #region Properties

        public DecisionSystem DS { get; private set; }
        private double?[][] crnUniverse;
        protected ApproximationSpace apprSpace;
        public Double Delta { get; private set; }
        public Double StartSigLevel = 0.5;
        public int DegreeOfFreedom {
            get {
                return DS.AttributesDomain[DS.DecisionAttribute].Length - 1;
            }
        }
        private List<double> decisionClasses;
        public List<double> DecisionClasses {
            get {
                if (decisionClasses == null) {
                    decisionClasses = this.DS.AttributesDomain[DS.DecisionAttribute].OrderBy(x => x).ToList();
                }
                return decisionClasses;
            }
        }
        public int DecisionClassCount {
            get {
                return this.DecisionClasses.Count;
            }
        }
        private Dictionary<int, List<double>> processedAttrDomains = new Dictionary<int, List<double>>();
        // removed interval (attribute value) => replaced interval (final one)
        private List<Dictionary<double, double>> intervalMapper = new List<Dictionary<double, double>>();

        // Dictionary<Attr, Aij table of Attr>
        private Dictionary<int, int[][]> CachedAijs = new Dictionary<int, int[][]>();

        // Current Condition Attributes
        private int[] CurrentConditionAttrs { 
            get {
                return processedAttrDomains
                    .Where(pair => pair.Value.Count > 1) // only attr have more than one interval
                    .Select(pair => pair.Key) // get attr 
                    .ToArray();
            }
        }

        // Dictionary<Attr, List of Chi2 statistics of Attr's intervals>
        private Dictionary<int, List<double>> Chi2Statistics = new Dictionary<int, List<double>>();

        

        #endregion

        #region Methods
        
        public Chi2Discretizer(DecisionSystem _DS) {
            this.DS = _DS;
            crnUniverse = _DS.Universe.Select(obj => (double?[])(obj.Clone())).ToArray();
            Delta = this.CalcCurrentInconsistencyRate();
            for(int attrIdx = 0; attrIdx < DS.AttributesDomain.Length; attrIdx++)
            {
                processedAttrDomains.Add(attrIdx, DS.AttributesDomain[attrIdx].OrderBy(x => x).ToList());
            }
            
            //TODO: If Ei < 5 , raise an exception
        }

        protected virtual double CalcCurrentInconsistencyRate() {
            return new StandardApproximationSpace(BuildProcessedDS(), CurrentConditionAttrs)
                .InConsistencyRate();
        }

        public DecisionSystem BuildProcessedDS()
        {
            DecisionSystem newDS = new DecisionSystem();
            newDS.AttributesDomain = this.processedAttrDomains
                .OrderBy(pair => pair.Key)
                .Select(pair => pair.Value.ToArray())
                .ToArray();
            newDS.ConditionAttributes = this.CurrentConditionAttrs;
            newDS.DecisionAttribute = this.DS.DecisionAttribute;
            newDS.DecisionAttributes = this.DS.DecisionAttributes;
            newDS.Universe = this.crnUniverse;

            return newDS;
        }

        // ith interval, jth class
        protected int[][] GetAijsOf(int Attr)
        {
            if (CachedAijs[Attr] == null) {
                List<double> AttrDomain = processedAttrDomains[Attr];
                int attrValCount = AttrDomain.Count;
                int classCount = DecisionClasses.Count;
                // init counter
                int[][] Aij = new int[attrValCount][];
                for (int i = 0; i < attrValCount; i++)
                {
                    Aij[i] = new int[classCount];
                    for (int j = 0; j < classCount; j++)
                    {
                        Aij[i][j] = 0;
                    }
                }
                int attrIdx, classIdx;
                foreach (var obj in DS.Universe)
                {
                    attrIdx = AttrDomain.IndexOf((double)obj[Attr]);
                    classIdx = DecisionClasses.IndexOf((double)obj[DS.DecisionAttribute]);
                    Aij[attrIdx][classIdx]++;
                }
                CachedAijs.Add(Attr, Aij);
            }
            return CachedAijs[Attr];
        }

        private double CalcChi2Value(int Attr, int firstIntervalIdx)
        {
            int[][] Aijs = GetAijsOf(Attr);
            int domainCount = processedAttrDomains[Attr].Count;
            int[] Cjs = new int[DecisionClassCount]; // idx: j => value: ith count
            int[] Ris = new int[2];
            int N = 0;
            int i, j, intervalIdx;
            double chi2Value;
            int[,] Eijs;
            for (i = 0; i < 2; i++)
            {
                intervalIdx = firstIntervalIdx + i; 
                Ris[i] = Aijs[intervalIdx].Sum();
                N += Ris[i];
                for (j = 0; j < DecisionClassCount; j++)
                {
                    Cjs[j] = Aijs[intervalIdx][j];
                }
            }
            // Calc Eij and Chi2Values
            Eijs = new int[2, DecisionClassCount];
            chi2Value = 0;
            for (i = 0; i < 2; i++)
            {
                for (j = 0; j < DecisionClassCount; j++)
                {
                    Eijs[i, j] = Ris[i] * Cjs[j] / N;
                    chi2Value += (Aijs[i][j] - Eijs[i, j]) ^ 2 / Eijs[i, j];
                }
            }
            return chi2Value;
        }

        // merge two adjacent intervals => new Aijs, new AttrDomain, update intervalMapper
        private int[][] MergeIntervals(int Attr, int firstIntervalIdx)
        {
            int secondIntervalIdx = firstIntervalIdx + 1;
            // Merge Aijs
            List<int[]> Aijs = GetAijsOf(Attr).ToList(); // use linq
            for (int j = 0; j < DecisionClassCount; j++) 
            {
                Aijs[firstIntervalIdx][j] += Aijs[secondIntervalIdx][j];
            }
            Aijs.RemoveAt(secondIntervalIdx);
            this.CachedAijs[Attr] = Aijs.ToArray();

            // map intervals
            if(this.intervalMapper[Attr] == null)
                this.intervalMapper[Attr] = new Dictionary<double,double>();
            double removedInterval = this.processedAttrDomains[Attr][secondIntervalIdx];
            double replacedInterval = this.processedAttrDomains[Attr][firstIntervalIdx];
            this.intervalMapper[Attr].Add(removedInterval, replacedInterval);
            foreach (var upair in intervalMapper[Attr].Where(pair => pair.Value == removedInterval)) 
            {
                this.intervalMapper[Attr][upair.Key] = replacedInterval;
            }
            // Update universe
            foreach (var obj in this.crnUniverse) {
                if (obj[Attr] == removedInterval)
                    obj[Attr] = replacedInterval;
            }

            // update AttrDomain
            this.processedAttrDomains[Attr].RemoveAt(secondIntervalIdx);
            return this.CachedAijs[Attr];
        }

        private double DecreSigLevel(double sigLevel)
        {
            return sigLevel - 0.01; //FIXME need reference to Distribution table 
        }

        private double GetChi2Threshold(double signLevel)
        {
            return Chi2DistributionTable.GetValueAt(signLevel, this.DegreeOfFreedom);
        }

        private List<double> InitChi2ValuesFor(int Attr)
        { 
            List<double> chi2Values = new List<double>();
            // each pair will get the index of the first interval as it's index
            // and be saved into chi2Values list
            for (int firstIdx = 0; firstIdx < processedAttrDomains[Attr].Count - 1; firstIdx++)
            {
                chi2Values.Add(CalcChi2Value(Attr, firstIdx));
            }
            return chi2Values;
        }

        private List<double> FindAndMergeIntervals(int Attr, List<double> chi2Values, double chi2Threshold) 
        {
            while (chi2Values.Count > 0)
            {
                double minChi2Val = chi2Values.Min();
                if (minChi2Val < chi2Threshold)
                {
                    int minIdx = chi2Values.IndexOf(chi2Values.Min());
                    this.MergeIntervals(Attr, minIdx);
                    chi2Values.RemoveAt(minIdx);
                    if (minIdx > 0)
                        chi2Values[minIdx - 1] = CalcChi2Value(Attr, minIdx - 1);

                    if (minIdx < this.processedAttrDomains[Attr].Count - 1)
                        chi2Values[minIdx] = CalcChi2Value(Attr, minIdx);
                }
                else
                {
                    break; // goto the next attribute
                }
            }
            return chi2Values;
        }

        public DecisionSystem Process() { 
            double signLevel = this.StartSigLevel;
            double chi2Threshold = GetChi2Threshold(signLevel);
            List<double> chi2Values;
            double signLevel0;

            // Phrase 1:
            while (true) 
            {
                foreach (int Attr in DS.ConditionAttributes) // TODO: should be only numeric attribute
                {
                    // Init chi2Value for all interval pairs, 
                    chi2Values = InitChi2ValuesFor(Attr);
                    //find the min interval pair and merge it, also recalculate chi2Value for new merged interval
                    FindAndMergeIntervals(Attr, chi2Values, chi2Threshold);
                }
                signLevel0 = signLevel;
                signLevel = DecreSigLevel(signLevel);
                if (this.CalcCurrentInconsistencyRate() < this.Delta)
                    continue;
                else
                    break;
            }
            // Phrase 2:
            double[] sigLevels = this.CurrentConditionAttrs.Select(x => signLevel0).ToArray();
            foreach (int Attr in this.CurrentConditionAttrs) {
                chi2Threshold = GetChi2Threshold(sigLevels[Attr]);
                chi2Values = InitChi2ValuesFor(Attr);

                while (true)
                {
                    chi2Values = this.FindAndMergeIntervals(Attr, chi2Values, chi2Threshold);
                    if (this.CalcCurrentInconsistencyRate() < this.Delta && chi2Values.Count > 0)
                    {
                        sigLevels[Attr] = this.DecreSigLevel(sigLevels[Attr]);
                        continue;
                    }
                    else
                        break;
                }
            }

            // finalize
            return this.BuildProcessedDS();
        }

        #endregion
    }
}

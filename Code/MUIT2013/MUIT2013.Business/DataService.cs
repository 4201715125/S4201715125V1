using MUIT2013.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUIT2013.Utils;
using MUIT2013.Data.Models;
using MUIT2013.DataMining.AttributeRule;
using MUIT2013.Data.ViewModels;

namespace MUIT2013.Business
{
    public class DataService: ServiceBase
    {
        public void UploadData(string fileName, string projectName)
        {
            var absoluteProjectPath = DataRepository.GetAbsoluteProjectPath(projectName);
            var relativeProjectPath = DataRepository.GetRelativeProjectPath(projectName);
            var fileInfo = new FileInfo(fileName);
            var newName = string.Format("{0}{1}", fileInfo.Name, DateTime.Now.ToTimeStamps());
            var absoluteFilePath = Path.Combine(absoluteProjectPath, newName);
            var relativeFilePath = Path.Combine(relativeProjectPath, newName);
            // copy file with new timestamp name
            File.Copy(fileName, absoluteFilePath, true);
            DataRepository.UploadData(fileInfo.Name, absoluteFilePath, relativeFilePath);            
        }

        public void CreateMapTable(DataFile dataFile, List<ColumnDefinition> columnDefinitions)
        {
            StringRuleFactory stringRuleFactory = new StringRuleFactory();
            NumericRuleFactory numericRuleFactory = new NumericRuleFactory();
            var rows = DataRepository.GetDataForTable(dataFile.RawTableName);            
            var records = new List<string[]>();
            foreach (var row in rows)
            {
                var rowSqls = new List<string>();
                rowSqls.Add(row.Id.ToString());
                foreach (var columnDefinition in columnDefinitions)
                {
                    string sValue = ((IDictionary<string, object>)row)[columnDefinition.Name].ToString();                                       
                    if (columnDefinition.ColumnType == "String")
                    {
                        if (columnDefinition.ValidationStatus == "Valid" && columnDefinition.MapRules.Count !=0)
                        {                            
                            var iValue = ApplyStringRule(columnDefinition, sValue, stringRuleFactory);
                            rowSqls.Add(iValue.Value.ToString());
                        }
                        else
                        {
                            rowSqls.Add(sValue);
                        }
                        
                        
                    }
                    else
                    {
                        rowSqls.Add(ApplyNumericRule(columnDefinition, sValue, numericRuleFactory).ToString());
                    }
                }

                records.Add(rowSqls.ToArray());
            }

            DataRepository.GenerateMapTable(dataFile, columnDefinitions, records);
        }

        public List<dynamic> GetViewData(string tableName)
        {
            return DataRepository.GetDataForTable(tableName).ToList();
        }

        public bool CheckStringRuleInValidationStatus(DataFile dataFile, ColumnDefinition columnDefinition, List<IStringRule> rules)
        {
            IEnumerable<string> charValues = DataRepository.GetDistinctValues(columnDefinition, dataFile.RawTableName);
            if (rules.Count == 0) return true;
            foreach (var charValue in charValues)
            {
                var isApplied = false;                
                foreach (var rule in rules)
                {
                    var result = rule.Apply(charValue);
                    if (result.HasValue)
                    {
                        isApplied = true;
                        break;
                    }
                }
                if (!isApplied) return false;                
            }

            return true;
        }

        public bool CheckNumericRuleInValidationStatus(DataFile dataFile, ColumnDefinition columnDefinition, List<INumericRule> rules)
        {
            IEnumerable<string> charValues = DataRepository.GetDistinctValues(columnDefinition, dataFile.RawTableName);
            if (rules.Count == 0) return true;
            foreach (var charValue in charValues)
            {
                var isApplied = false;
                try
                {
                    foreach (var rule in rules)
                    {
                        var result = rule.Apply(float.Parse(charValue));                        
                    }
                    isApplied = true;
                }catch(Exception e){
                    isApplied = false;
                }
                if (!isApplied) return false;
            }

            return true;
        }

        public IEnumerable<AppliedRuleValue> GetValuesWithNoRuleAppliedInStringRule(DataFile dataFile, ColumnDefinition columnDefinition, List<IStringRule> rules)
        {
            IEnumerable<string> charValues = DataRepository.GetDistinctValues(columnDefinition, dataFile.RawTableName);
            List<AppliedRuleValue> appliedRuleValues = new List<AppliedRuleValue>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var charValue in charValues)
            {
                if (rules.Count == 0)
                {
                    appliedRuleValues.Add(new AppliedRuleValue { IsApplied = false, Value = charValue });
                }
                else
                {
                    var appliedRuleValue = new AppliedRuleValue { Value = charValue, IsApplied = false };
                    foreach (var rule in rules)
                    {

                        var result = rule.Apply(charValue);
                        if (result.HasValue)
                        {
                            appliedRuleValue.IsApplied = true;                            
                            break;
                        }
                       
                    }
                    appliedRuleValues.Add(appliedRuleValue);
                }
            }
            List<int> removedIndices = new List<int>(); 
            // remove duplicate applied rule values
            for (int i = appliedRuleValues.Count - 1; i >= 0 ; i--)
            {
                var appliedRuleValue = appliedRuleValues[i];
                if (dict.ContainsKey(appliedRuleValue.Value))
                {
                    removedIndices.Add(i);                    
                }
                else 
                {
                    dict[appliedRuleValue.Value] = i;
                }
            }

            foreach (var removedIndex in removedIndices)
            {
                appliedRuleValues.RemoveAt(removedIndex);
            }
            return appliedRuleValues;
        }

        public IEnumerable<AppliedRuleValue> GetValuesWithNoRuleAppliedInNumericRule(DataFile dataFile, ColumnDefinition columnDefinition, List<INumericRule> rules)
        {
            IEnumerable<string> charValues = DataRepository.GetDistinctValues(columnDefinition, dataFile.RawTableName);
            List<AppliedRuleValue> appliedRuleValues = new List<AppliedRuleValue>();
            bool isError = false;
            foreach (var charValue in charValues)
            {
                if (rules.Count == 0 || isError)
                {
                    appliedRuleValues.Add(new AppliedRuleValue {IsApplied = false, Value = charValue });
                }
                else
                {
                    try
                    {
                        foreach (var rule in rules)
                        {
                            var result = rule.Apply(float.Parse(charValue));
                        }
                        appliedRuleValues.Add(new AppliedRuleValue
                        {
                            IsApplied = true,
                            Value = charValue
                        });
                    }
                    catch (FormatException e)
                    {
                        appliedRuleValues.Add(new AppliedRuleValue
                        {
                            IsApplied = false,
                            Value = charValue
                        });
                        isError = true;                        
                    }
                }
            }
            return appliedRuleValues;
        }

        public float ApplyNumericRule(ColumnDefinition columnDefinition, string sValue, NumericRuleFactory numericRuleFactory)
        {
            float fValue = float.Parse(sValue);
            INumericRule rule = null;
            foreach (var mapRule in columnDefinition.MapRules)
            {
                rule = numericRuleFactory.CreateRule(mapRule.RuleType, mapRule.RuleContent);
                var result = rule.Apply(fValue);
                if (result.HasValue)
                {
                    fValue = result.Value;
                }                
            }

            return fValue;
        }

        public int? ApplyStringRule(ColumnDefinition columnDefinition, string sValue, StringRuleFactory stringRuleFactory)
        {            
            IStringRule rule = null;
            foreach (var mapRule in columnDefinition.MapRules)
            {
                rule = stringRuleFactory.CreateRule(mapRule.RuleType, mapRule.RuleContent);
                var result = rule.Apply(sValue);
                if (result.HasValue)
                {
                    return result.Value;
                }
            }

            return null;
        }
    }
}

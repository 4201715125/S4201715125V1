using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class Digitizer
    {
        public DecisionTable DecisionTable { get; set; }
        public string[] AttributeNames { get; set; }
        public Digitizer(DecisionTable dt)
        {
            var dataFile = DataFileRepository.GetList().FirstOrDefault(p => p.MapTableName == dt.TableName || p.RawTableName == dt.TableName);
            this.DecisionTable = dt;            
            this.AttributeNames = AttributeDefinitionRepository.GetList(dataFile.Id).OrderBy(x => x.Id).Select(x => x.RawName).ToArray();
        }
        public IEnumerable<string> Translate(IEnumerable<int> attributeIndices)
        {
            return attributeIndices.Select(i => this.AttributeNames[i]);
        }
    }
}

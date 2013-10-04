using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class AttributeDefinitionService : ServiceBase
    {
        public List<AttributeDefinition> GetList(long dataFileId)
        {
            return AttributeDefinitionRepository.GetList(dataFileId).ToList();
        }

        public void BulkUpdate(List<AttributeDefinition> models)
        {
            AttributeDefinitionRepository.BulkUpdate(models);
        }
    }
}

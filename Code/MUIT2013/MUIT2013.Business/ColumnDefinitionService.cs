using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class ColumnDefinitionService : ServiceBase
    {
        public List<ColumnDefinition> GetList(long dataFileId)
        {
            return ColumnDefinitionRepository.GetList(dataFileId).ToList();
        }

        public void BulkUpdate(List<ColumnDefinition> models)
        {
            ColumnDefinitionRepository.BulkUpdate(models);
        }
    }
}

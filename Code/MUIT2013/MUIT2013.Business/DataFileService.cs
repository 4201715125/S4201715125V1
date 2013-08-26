using MUIT2013.Data.Models;
using MUIT2013.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class DataFileService : ServiceBase
    {
        public List<DataFile> GetList()
        {
            return DataFileRepository.GetList().ToList();
        }

        public void Active(DataFile dataFile)
        {
            DataFileRepository.Activate(dataFile);
        }

        public DataFile GetActivedDataFile()
        {
            return DataFileRepository.GetActivatedDateFile();
        }

        public void BulkUpdate(List<DataFile> models)
        {
            DataFileRepository.BulkUpdate(models);
        }
    }
}

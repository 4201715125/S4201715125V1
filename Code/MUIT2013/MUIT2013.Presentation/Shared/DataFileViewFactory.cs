using MUIT2013.Data.Models;
using MUIT2013.Presentation.Shared.ViewData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Presentation.Shared
{
    public class DataFileViewFactory
    {
        private Dictionary<long, DataFileView> container = new Dictionary<long, DataFileView>();
        public DataFileView Create(DataFile dataFile)
        {
            var cacheKey = dataFile.Id;
            DataFileView cache = null; ;
            if (container.ContainsKey(cacheKey)) cache = container[cacheKey];
            else
            {
                cache = container[cacheKey] = new DataFileView(dataFile);
            }
            return cache;
        }
    }
}

using MUIT2013.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Business
{
    public class ServiceBase
    {
        public static string GetDataPath()
        {
            return DataRepository.GetAbsoluteDataPath();
        }
    }
}

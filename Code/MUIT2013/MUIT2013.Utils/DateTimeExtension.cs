using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Utils
{
    public static class DateTimeExtension
    {
        public static string ToTimeStamps(this DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public static string ToNormalDateTimeString(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}

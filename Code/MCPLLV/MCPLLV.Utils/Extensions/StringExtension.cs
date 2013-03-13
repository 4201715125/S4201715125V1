using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCPLLV.Utils.Extensions
{
    public static class StringExtension
    {
        public static string ToDBConventionName(this string input)
        {
            // TODO: normalize input to table name
            return input;
        }

        public static string ToRemovedUnicode(this string input)
        { 
            // TODO: remove and replace Unicode characters
            return input;
        }
    }
}

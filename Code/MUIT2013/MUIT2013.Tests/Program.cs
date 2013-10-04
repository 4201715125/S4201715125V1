using System.Diagnostics;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUIT2013.Tests
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //ASTest.TestAprroximationSpace();
            ReductTest.TestReduct();
            //Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }
    }
}

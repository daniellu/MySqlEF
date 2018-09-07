using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hatfield.AnalyteManagement.Domain;
using Hatfield.AnalyteManagement.Data;

namespace Hatfield.AnalyteManagement.Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new DbEntities();

            var allGuidelines = dbContext.Guidelines;

            Console.WriteLine(allGuidelines.Count() + " found in the database.");
            Console.ReadLine();
        }
    }
}

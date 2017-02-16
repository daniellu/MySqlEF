using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;
using Data;

namespace Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new DbEntities();

            var allPersons = dbContext.persons;

            Console.WriteLine(allPersons.Count() + " found in the database.");
            Console.ReadLine();
        }
    }
}

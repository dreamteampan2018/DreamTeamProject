using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeLibrary.WebApp;
using HomeLibrary.DatabaseModel;

namespace CheckResults
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(StatisticRepository.GetAuthorsCount());
        }
    }
}

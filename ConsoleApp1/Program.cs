using HomeLibrary.WebApp.Repository;
using System;
using HomeLibrary.WebApp.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            StatisticRepository newRepo = new StatisticRepository(new ApplicationDbContext(options =>
                options.UseSqlServer("")));
            
        }
    }
}

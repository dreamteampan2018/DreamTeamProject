using HomeLibrary.WebApp.Repository;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            StatisticRepository rep = new StatisticRepository();
            rep.GetAuthorsCount();
        }
    }
}

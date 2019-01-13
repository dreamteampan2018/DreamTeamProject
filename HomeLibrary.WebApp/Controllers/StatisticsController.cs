using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.HelperClass;
using HomeLibrary.WebApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HomeLibrary.WebApp.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly StatisticRepository statisticRepository;
        public StatisticsController(ApplicationDbContext context)
        {

            statisticRepository = new StatisticRepository(context);
        }
        public IActionResult Index()
        {
            int authorsCount = statisticRepository.GetAuthorsCount();
            ViewData["AuthorsCount"] = authorsCount;


            int borrowedCountItem = statisticRepository.GetBorrowedCountItem(4);
            ViewData["BorrowedCountItem"] = borrowedCountItem;

            // int ilosc = statisticRepository.GetItemsCount(id);
            // ViewData["TotalCountItem"] = ilosc;
            ViewBag.Data = "1,8,2,12"; //list of strings that you need to show on the chart. as mentioned in the example from c-sharpcorner
            ViewBag.ObjectName = "a,b,c,d";
            List<MostTypeStatistics> tempList = statisticRepository.GetMostTypeStatistics();
            ViewData["MostTypeStat"] = tempList;

            return View();
        }
        public IActionResult GetAuthorsCount()
        {
           int ilosc= statisticRepository.GetAuthorsCount();
            ViewData["AuthorsCount"] = ilosc;
            return View();
        }

        //Statistics/GetBoorowdItems/1
        public IActionResult GetBoorowdItems(int id)
        {
            int borrowedCountItem = statisticRepository.GetBorrowedCountItem(id);
            ViewData["BorrowedCountItem"] = borrowedCountItem;
            return View();

        }
        
        public IActionResult GetItemsCount (int typeID)
        {
            int ilosc = statisticRepository.GetItemsCount(typeID);
            ViewData["TotalCountItem"] = ilosc;
            return View();
        }

        public IActionResult GetMostTypeStatistic()
        {
            List<MostTypeStatistics> tempList = statisticRepository.GetMostTypeStatistics();
            ViewData["MostTypeStat"] = tempList;
            return View();
        }
    }
}
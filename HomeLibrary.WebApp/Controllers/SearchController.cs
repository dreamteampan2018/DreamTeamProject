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
    public class SearchController : Controller
    {
        private readonly ItemRepository repository;

        public SearchController(ApplicationDbContext context)
        {
            repository = new ItemRepository(context);
        }
        public IActionResult Index()
        {
          return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Result([Bind("TypeId,Title,Author")] SearchPosition item)
        {
            
            if (ModelState.IsValid)
            {
                var itemList=repository.Search(item.TypeId, item.Title, item.Author);
                return View(itemList.ToList());
            }
            return View();
        }
    }
}
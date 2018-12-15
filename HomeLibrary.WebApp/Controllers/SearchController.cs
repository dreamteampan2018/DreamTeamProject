using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.HelperClass;
using HomeLibrary.WebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeLibrary.WebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly ItemRepository repository;
        private readonly ItemTypeRepository repositoryType;
        public SearchController(ApplicationDbContext context)
        {
            repository = new ItemRepository(context);
            repositoryType = new ItemTypeRepository(context);
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ItemType> typeList = await repositoryType.GetItemTypesAsync();
            ViewData["TypeList"] = new SelectList(typeList, "ItemTypeId","Name");

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
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
            ViewData["TypeList"] = new SelectList(typeList, "ItemTypeId", "Name");
            return View();
        }

        public async Task<IActionResult> SearchTitle()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchTitle([Bind("Title")] SearchPosition itemTitle)
        {
            var itemList = repository.TitleSearch(itemTitle.Title);
            return View("Result",itemList.ToList());
        }


        public async Task<IActionResult> SearchAuthor()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchAuthor([Bind("Author")] SearchPosition itemAuthor)
        {
            var itemList = repository.AuthorSearch(itemAuthor.Author);
            return View("Result",itemList.ToList());
        }


        public async Task<IActionResult> SearchType()
        {
            IEnumerable<ItemType> typeList = await repositoryType.GetItemTypesAsync();
            ViewData["TypeList"] = new SelectList(typeList, "ItemTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchType([Bind("TypeId")] SearchPosition itemType)
        {
            var itemList = repository.TypeSearch(itemType.TypeId);
            return View("Result", itemList.ToList());
        }


    }
}
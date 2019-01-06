using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using HomeLibrary.WebApp.Repository;
using HomeLibrary.WebApp.Repository.Interface;

namespace HomeLibrary.WebApp.Controllers
{
    public class ItemTypesController : Controller
    {
        private readonly ItemTypeRepository repository;

        public ItemTypesController(ApplicationDbContext context)
        {
            repository = new ItemTypeRepository(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await repository.GetItemTypesAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var itemType = await repository.GetItemTypeByIdAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }
            return View(itemType);
        }
  
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ItemTypeId,Name,CreatorDescription")] ItemType itemType)
        {
            
                repository.InsertItemType(itemType);
                await repository.SaveAsync();
                return RedirectToAction(nameof(Index));
          }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var itemType = await repository.GetItemTypeByIdAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }
            return View(itemType);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit([Bind("ItemTypeId,Name,CreatorDescription")] ItemType itemType)
        {

            repository.UpdateItemType(itemType);
            await repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public async Task<IActionResult> Delete(int id)
            {

            var itemType = await repository.GetItemTypeByIdAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }

            return View(itemType);
            }
     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            repository.DeleteItemType(id);
            await repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
      
        [Authorize]
        public async Task<IActionResult> Borrow(int id)
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Return(int id)
        {

            return View();
        }

    }
}

using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.HelperClass;
using HomeLibrary.WebApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemRepository repository;
        private readonly ItemTypeRepository typeRepository;
        private readonly AuthorRepository authorRepository;

        public ItemsController(ApplicationDbContext context)
        {

            repository = new ItemRepository(context);
            typeRepository = new ItemTypeRepository(context);
            authorRepository = new AuthorRepository(context);
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await repository.GetItemsAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int id)
        {


            var item = await repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Author> authorList = await authorRepository.GetAuthorsAsync();
            ViewData["AuthorList"] = new SelectList(authorList, "AuthorId", "AuthorName");
            IEnumerable<ItemType> typeList = await typeRepository.GetItemTypesAsync();
            ViewData["TypeList"] = new SelectList(typeList, "ItemTypeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Title,ItemTypeId,AuthorId,Description,YearPublishment,Borrowed,CoverGuid")] Item item)
        {

            repository.InsertItem(item);
            await repository.SaveAsync();
            return RedirectToAction(nameof(Index));


        }

        // GET: Items/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            IEnumerable<Author> authorList = await authorRepository.GetAuthorsAsync();
            ViewData["AuthorList"] = new SelectList(authorList, "AuthorId", "AuthorName");
            IEnumerable<ItemType> typeList = await typeRepository.GetItemTypesAsync();
            ViewData["TypeList"] = new SelectList(typeList, "ItemTypeId", "Name");

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,Title,ItemTypeId,AuthorId,Description,YearPublishment,Borrowed,CoverGuid")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repository.UpdateItem(item);
                    await repository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        // GET: Items/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {


            var item = await repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await repository.GetItemByIdAsync(id);
            if (item != null)
            {
                repository.DeleteItem(id);
                await repository.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public async Task<IActionResult> Borrow(int id)
        {
            var item = await repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Borrow(int id, [Bind("ItemId,BorrowedPerson")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }
            try
            {
                Item itemToBorrow = await repository.GetItemByIdAsync(id);
                var currentUser = HttpContext.User.Identity;
                await repository.SetAsBorrowed(item.BorrowedPerson, itemToBorrow, currentUser.Name);
             
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.ItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Return(int id)
        {
            Item toReturn = await repository.GetItemByIdAsync(id);


            await repository.SetAsReturned(toReturn);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost("UploadAzureFiles")]
        public async Task<IActionResult> PostCoverToAzure(List<IFormFile> files)
        {
            Item item = new Item();
            var uploadSuccess = false;
            ViewData["OCRDetail"] = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length <= 0)
                {
                    continue;
                }
                var stream = formFile.OpenReadStream();
                 List<string> tempList= await CognitiveServiceHelper.MakeOCRRequest(stream);
                ViewData["OCRDetail"] = tempList;
                
                item.Title = tempList[1];
                item.ItemTypeId = 0;
                item.AuthorId = authorRepository.SearchAuthorId(tempList[0]);
            }
            // return View("ShowAzureDetail");
            IEnumerable<Author> authorList = await authorRepository.GetAuthorsAsync();
            ViewData["AuthorList"] = new SelectList(authorList, "AuthorId", "AuthorName");
            IEnumerable<ItemType> typeList = await typeRepository.GetItemTypesAsync();
            ViewData["TypeList"] = new SelectList(typeList, "ItemTypeId", "Name");
            return View("Create",item);
        }


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> PostFile(List<IFormFile> files, int itemId)
        {
            var uploadSuccess = false;

            foreach (var formFile in files)
            {
                if (formFile.Length <= 0)
                {
                    continue;
                }
                using (var stream = formFile.OpenReadStream())
                {
                    uploadSuccess = await UploadToBlob(formFile.FileName, itemId, null, stream);
                }
            }
            Item toRedirect = await repository.GetItemByIdAsync(itemId);
            IEnumerable<Author> authorList = await authorRepository.GetAuthorsAsync();
            ViewData["AuthorList"] = new SelectList(authorList, "AuthorId", "AuthorName");
            IEnumerable<ItemType> typeList = await typeRepository.GetItemTypesAsync();
            ViewData["TypeList"] = new SelectList(typeList, "ItemTypeId", "Name");
            return View("Edit",toRedirect);
        }

        private bool ItemExists(int id)
        {
            if (repository.GetItemByIdAsync(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> UploadToBlob(string filename, int itemId, byte[] imageBuffer = null, Stream stream = null)
        {
            AzureRepository azure = new AzureRepository();
            var uri = await azure.AddBlobToStorage(filename, imageBuffer, stream);
            Item toChange = await repository.GetItemByIdAsync(itemId);
            toChange.CoverGuid = uri;
            repository.UpdateItem(toChange);
            await repository.SaveAsync();
            return true;
        }

    }
}
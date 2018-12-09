using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.Repository;
using Microsoft.AspNetCore.Authorization;

namespace HomeLibrary.WebApp.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorRepository repository;

        public AuthorsController(ApplicationDbContext context)
        {
            repository =  new AuthorRepository(context);
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await repository.GetAuthorsAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var author = await repository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("AuthorId,Name,Surname")] Author author)
        {
           // if (ModelState.IsValid)
           // {
           //     _context.Add(author);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
           // }
            return View(author);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
           var author = await repository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

          [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,Name,Surname")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
               //     _context.Update(author);
                    await repository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorId))
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
            return View(author);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await repository.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await repository.GetAuthorByIdAsync(id);

            if (author != null)
            {
                repository.DeleteAuthor(id);
                await repository.SaveAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            if (repository.GetAuthorByIdAsync(id).Result!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

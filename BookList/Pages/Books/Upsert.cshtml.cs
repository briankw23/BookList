using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookList.Pages.Books
{
    public class UpsertModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null )
            {
                //CREATE
                return Page();
            }
            //UPDATE
            Book = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else
                {
                    _db.Book.Update(Book);
                }
            }
            return RedirectToPage();
        }
    }
}

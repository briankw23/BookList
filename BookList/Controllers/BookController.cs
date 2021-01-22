using BookList.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookList.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDB = await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDB == null)
            {
                return Json(new { success = false, message = "error while deleting" });
            }
            _db.Book.Remove(bookFromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "delete was successfull" });
        }
    }
}

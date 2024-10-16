using book.Data;
using book.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace book.Controllers
{
    [Authorize(Roles = $"{ClassRoles.roleUser},{ClassRoles.roleAdmin}")]
    public class CRUDController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CRUDController( ApplicationDbContext context)
        {
            
            _context = context;
        }
        public IActionResult Delete(int id)
        {
            var book = _context.books.FirstOrDefault(b => b.id == id);
            _context.books.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("AllBooks");
        }
        public IActionResult Update(int id)
        {
            var book = _context.books.FirstOrDefault(b => b.id == id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Update(Book model)
        {
            _context.books.Update(model);
            _context.SaveChanges();
            return RedirectToAction("AllBooks");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Book model)
        {
            _context.books.Update(model);
            _context.SaveChanges();
            return RedirectToAction("AllBooks");
        }
        public IActionResult AllType(string type)
        {
            var booklist = _context.books.Where(b => b.gener == type).ToList();
            return View(booklist);
        }

    }
}

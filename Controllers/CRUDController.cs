using book.Data;
using book.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace book.Controllers
{
    [Authorize(Roles = $"{ClassRoles.roleAdmin}")]
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
            return RedirectToAction("AllBooks" , "Home");
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
            return RedirectToAction("AllBooks" , "Home");
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
            return RedirectToAction("AllBooks" , "Home");
        }
        public IActionResult AdminBorrowedBooks()
        {
            var borrowedBooks = _context.Borrows
                                        .Include(b => b.Book)
                                        .Include(b => b.User)
                                        .ToList();

            return View(borrowedBooks);
        }


        public IActionResult AdminBoughtBooks()
        {
            var boughtBooks = _context.Buys
                                      .Include(b => b.Book)
                                      .Include(b => b.User)
                                      .ToList();

            return View(boughtBooks);
        }

    }
}

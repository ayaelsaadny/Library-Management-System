using book.Data;
using book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using static Azure.Core.HttpHeader;
using static System.Reflection.Metadata.BlobBuilder;


using book.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace book.Controllers
{
    //[Authorize(Roles = $"{ClassRoles.roleUser},{ClassRoles.roleAdmin}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

       
        public IActionResult AllBooks()
        {
            var bookList = _context.books.ToList();

            return View(bookList);
        }
        public IActionResult Details(int id)
        {
            var book = _context.books.FirstOrDefault(b => b.id == id);
            return View(book);
        }
        public IActionResult AllType(string type)
        {
            var booklist = _context.books.Where(b=> b.gener == type).ToList();
            return View(booklist);
        }

        public IActionResult search(string inpt)
        {
            var matchingBooks = _context.books.Where(p=> p.name.Contains(inpt)).ToList();

            return View(matchingBooks);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

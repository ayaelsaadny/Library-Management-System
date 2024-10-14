using book.Data;
using book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using book.ViewModel;
namespace book.Controllers
{
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
		public async Task<IActionResult> Borrow(int id)
		{
			var book = await _context.books.FirstOrDefaultAsync(b => b.id == id);

			if (book == null)
			{
				TempData["Error"] = "Book not found.";
				return RedirectToAction("AllBooks");
			}

			var borrowViewModel = new BorrowVM
			{
				BookId = book.id,
				BookName = book.name,
				Author = book.Author,
				availablity = book.avalibilty,
				BorrowDate = DateTime.Now
			};

			return View(borrowViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> BorrowConfirm(BorrowVM borrowViewModel)
		{
			var book = await _context.books.FirstOrDefaultAsync(b => b.id == borrowViewModel.BookId);

			if (book == null)
			{
				TempData["Error"] = "Book not found.";
				return RedirectToAction("AllBooks");
			}

			if (book.avalibilty)
			{
				book.avalibilty = false;

				_context.books.Update(book);
				await _context.SaveChangesAsync();
				TempData["Message"] = "Borrowing complete! Thank you.";
			}
			else
			{
				ModelState.AddModelError("", "Book is not available for borrowing.");
			}


			return RedirectToAction("BorrowConfirm", borrowViewModel);
		}



		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
        public IActionResult Buy(int id)
        {
            var book = _context.books.FirstOrDefault(b => b.id == id);

            if (book == null)
            {
                TempData["Error"] = "Book not found.";
                return RedirectToAction("AllBooks");
            }

            var buyViewModel = new BuyVM
            {
                BookId = book.id,
                BookName = book.name,
                Author = book.Author,
                Availability = book.avalibilty
            };

            return View(buyViewModel);
        }

        [HttpPost]
        public IActionResult BuyConfirm(BuyVM buyViewModel)
        {
            var book = _context.books.FirstOrDefault(b => b.id == buyViewModel.BookId);

            if (book == null)
            {
                TempData["Error"] = "Book not found.";
                return RedirectToAction("Buy", new { id = buyViewModel.BookId });
            }

            if (book.avalibilty)
            {
                book.avalibilty = false;
                _context.books.Update(book);
                _context.SaveChanges(); 
                TempData["Message"] = "Purchase complete! Thank you for your order.";
            }
            else
            {
                TempData["Error"] = "Book is not available for purchase.";
            }

            return RedirectToAction("Buy", new { id = buyViewModel.BookId });
        }

        public IActionResult Borrow(int id)
        {
            var book = _context.books.FirstOrDefault(b => b.id == id);

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
        public IActionResult BorrowConfirm(BorrowVM borrowViewModel)
        {
            var book = _context.books.FirstOrDefault(b => b.id == borrowViewModel.BookId);

            if (book == null)
            {
                TempData["Error"] = "Book not found.";
                return RedirectToAction("AllBooks");
            }

            if (book.avalibilty)
            {
                book.avalibilty = false;
                _context.books.Update(book);
                _context.SaveChanges();
                TempData["Message"] = "Borrowing complete! Thank you.";
            }
            else
            {
                TempData["Error"] = "Book not available for borrowing.";
            }

            return RedirectToAction("Borrow", new { id = borrowViewModel.BookId });
        }

        public IActionResult Return(int id)
        {
            var borrow = _context.Borrows
                .Include(c => c.Book)
                .FirstOrDefault(c => c.BookId == id && c.ReturnDate == null);

            if (borrow == null)
            {
                TempData["Error"] = "No active borrow found for this book.";
                return RedirectToAction("AllBooks");
            }

            var borrowDate = borrow.BorrowDate;
            var returnDate = DateTime.Now;
            var dueDate = borrow.EndDate;

            int penalty = 0;
            if (returnDate > dueDate)
            {
                TimeSpan lateDays = returnDate - dueDate;
                penalty = (int)lateDays.TotalDays * 5;
            }

            var returnViewModel = new BorrowVM
            {
                BookId = borrow.BookId,
                BookName = borrow.Book.name,
                Author = borrow.Book.Author,
                BorrowDate = borrowDate,
                Penality = penalty
            };

            return View(returnViewModel);
        }

        [HttpPost]
        public IActionResult ReturnConfirm(BorrowVM returnViewModel)
        {
            var checkout = _context.Borrows.FirstOrDefault(c => c.BookId == returnViewModel.BookId && c.ReturnDate == null);

            if (checkout == null)
            {
                TempData["Error"] = "No active borrow found for this book.";
                return RedirectToAction("AllBooks");
            }

            checkout.ReturnDate = DateTime.Now;
            checkout.Book.avalibilty = true;
            _context.Borrows.Update(checkout);
            _context.SaveChanges(); 

            TempData["Message"] = "Book returned successfully!";

            return RedirectToAction("Return", new { id = returnViewModel.BookId });
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

using book.Data;
using book.Models;
using book.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace book.Controllers
{
    [Authorize(Roles = $"{ClassRoles.roleUser},{ClassRoles.roleAdmin}")]
    public class BorrowController : Controller
    {

        private readonly ApplicationDbContext _context;

        public BorrowController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Buy(int id)
        {
            var book = await _context.books.FirstOrDefaultAsync(b => b.id == id);

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

            ViewBag.Message = TempData["Message"];
            ViewBag.Error = TempData["Error"];

            return View(buyViewModel);
        }





        [HttpPost]
        public async Task<IActionResult> BuyConfirm(BuyVM buyViewModel)
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
                await _context.SaveChangesAsync();
                TempData["Message"] = "Purchase complete! Thank you for your order.";
            }
            else
            {
                ModelState.AddModelError("", "Book is not available for purchase.");
            }


            return RedirectToAction("Buy", new { id = buyViewModel.BookId });
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


            return RedirectToAction("Borrow", new { id = borrowViewModel.BookId });

        }

        public async Task<IActionResult> Return(int id)
        {
            var borrow = await _context.Borrows
                .Include(c => c.book)
                .FirstOrDefaultAsync(c => c.BookId == id && c.ReturnDate == null);

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
                BookName = borrow.book.name,
                Author = borrow.book.Author,
                BorrowDate = borrowDate,
                Penality = penalty
            };

            return View(returnViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnConfirm(BorrowVM returnViewModel)
        {
            var checkout = await _context.Borrows.FirstOrDefaultAsync(c => c.BookId == returnViewModel.BookId && c.ReturnDate == null);

            if (checkout == null)
            {
                TempData["Error"] = "No active borrow found for this book.";
                return RedirectToAction("AllBooks");
            }


            checkout.ReturnDate = DateTime.Now;
            checkout.book.avalibilty = true;
            _context.Borrows.Update(checkout);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Book returned successfully!";

            return RedirectToAction("Return", new { id = returnViewModel.BookId });
        }

    }
}

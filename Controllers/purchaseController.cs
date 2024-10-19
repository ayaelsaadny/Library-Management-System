using book.Data;
using book.Models;
using book.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace book.Controllers
{
    [Authorize(Roles = $"{ClassRoles.roleUser},{ClassRoles.roleAdmin}")]
    public class purchaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public purchaseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Buy(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the current user's ID
            
            var viewModel = new BuyConfirmationViewModel
            {
                UserId = userId,
                BookId = book.id,
                BookTitle = book.name,
                bookPrice = book.price
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> buy(BuyConfirmationViewModel model)
        {
            
            
                var buy = new Buy
                {
                    UserId = model.UserId,
                    BookId = model.BookId
                };

                _context.Buys.Add(buy);
                await _context.SaveChangesAsync();

                // Optionally, redirect to a success page or back to the book details
                return RedirectToAction("AllBooks", "Home"); // Adjust as necessary
          
        }
        public async Task<IActionResult> Borrow(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new BorrowViewModel
            {
                UserId = userId,
                BookId = book.id,
                BookTitle = book.name,
                borrowdate = DateTime.Now.AddDays(10),
            };

            return View(viewModel); // Create a new Borrow view
        }
        [HttpPost]
        public async Task<IActionResult> Borrow(BorrowViewModel model)
        {
           
                var borrow = new Borrow
                {
                    UserId = model.UserId,
                    BookId = model.BookId,
                    BorrowDate = DateTime.Now // Set the borrow date
                };

                _context.Borrows.Add(borrow);
                await _context.SaveChangesAsync();

                return RedirectToAction("AllBooks", "Home"); // Adjust as needed
           
          
        }
        public IActionResult MyBorrowedBooks()
        {
            var userId = _userManager.GetUserId(User);
            var borrowedBooks = _context.Borrows
                                        .Include(b => b.Book)
                                        .Where(b => b.UserId == userId)
                                        .ToList();

            return View(borrowedBooks);
        }

        [Authorize(Roles = "User")]
        public IActionResult MyBoughtBooks()
        {
            var userId = _userManager.GetUserId(User);
            var boughtBooks = _context.Buys
                                      .Include(b => b.Book)
                                      .Where(b => b.UserId == userId)
                                      .ToList();

            return View(boughtBooks);
        }
        public IActionResult returnBook(int id)
        {
            var book = _context.Borrows.FirstOrDefault(b => b.Id == id);
                _context.Borrows.Remove(book);
                _context.SaveChanges();
                return RedirectToAction("MyBorrowedBooks", "purchase");
            

        }

    }
}

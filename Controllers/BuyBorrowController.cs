using book.Data;
using book.Models;
using book.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace book.Controllers
{
    public class BuyBorrowController : Controller
    {
        private readonly ApplicationDbContext _context;

        //public BuyBorrowController(ApplicationDbContext context)
        //{

        //    _context = context;
        //}

        //public IActionResult buy(int id)
        //{
        //    var book = _context.books.FirstOrDefault(b => b.id == id);
        //    return View(book);
        //}
        //[HttpPost]
        //public IActionResult buy(BuyVM model)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var viewModel = new BuyVM
        //    {
        //        UserId = userId,
        //        BookId = book.id,
        //        BookName = book.name,
        //        UserName = User.Identity.Name
        //    };
        //    RedirectToAction("AllBooks", "Home");
        //}

        private readonly UserManager<ApplicationUser> _userManager;

        public BuyBorrowController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> buy(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("login", "Accounts");
            }

            var purchase = new Buy
            {
                UserId = user.Id,
                BookId = bookId
            };

            _context.buys.Add(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction("AllBooks", "Home");
        }
    }
}

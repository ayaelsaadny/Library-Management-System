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
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public IActionResult buy(int bookId)
        {
            var user =  _userManager.GetUserId(User);
            if (user == null)
            {
                return RedirectToAction("login", "Accounts");
            }

            var purchase = new Buy
            {
                UserId = user,
                BookId = bookId
            };

            _context.buys.Add(purchase);
             _context.SaveChanges();
            return RedirectToAction("AllBooks", "Home");
        }

        [Authorize(Roles = "User")] 
        [HttpPost] 
        public IActionResult Borrow(int bookId)
        {
            var user =  _userManager.GetUserId(User); 
            if (user == null)
            {
                return RedirectToAction("login", "Accounts"); 
            }

          
            var borrow = new Borrow
            {
                UserId = user, 
                BookId = bookId,  
                BorrowDate = DateTime.Now,  
                EndDate = DateTime.Now.AddDays(6) 
            };

            
            _context.Add(borrow); 
            _context.SaveChanges();

          
            return RedirectToAction("AllBooks", "Home");
        }




    }
}

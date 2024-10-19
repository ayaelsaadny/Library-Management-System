using Microsoft.AspNetCore.Identity;

namespace book.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Buy> Buys { get; set; }

        public ICollection<Borrow> Borrows { get; set; }
    }
}

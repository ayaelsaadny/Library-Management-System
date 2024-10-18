using Microsoft.AspNetCore.Identity;

namespace book.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Buy> UserBooks { get; set; } = new List<Buy>();
        public List<Borrow> Borrows { get; set; }
    }
}

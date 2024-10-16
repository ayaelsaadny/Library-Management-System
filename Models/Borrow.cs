using System.ComponentModel.DataAnnotations;

namespace book.Models
{
    public class Borrow
    {

        public int BookId { get; set; }
        public Book book { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Key]
        public int BorrowId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Penality { get; set; }
    }
}

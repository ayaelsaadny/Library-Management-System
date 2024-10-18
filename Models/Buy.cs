namespace book.Models
{
    public class Buy
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key for ApplicationUser
        public int BookId { get; set; } // Foreign key for Book

        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}

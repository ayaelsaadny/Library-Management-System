namespace book.Models
{
    public class Borrow
    {


        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; } 
        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime EndDate { get; set; } 




    }
}

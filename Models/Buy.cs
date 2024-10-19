namespace book.Models
{
    public class Buy
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }

        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
    }
}

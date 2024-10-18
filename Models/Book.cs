namespace book.Models
{
    public class Book
    {
        public  int id { get; set; }
        public  string name { get; set; }
        public  string gener { get; set; }
        public  bool avalibilty { get; set; }
        public  string photo { get; set; }
        public string Author { get; set; }
       
        public int CountNum { get; set; }
        public virtual ICollection<Buy> UserBooks { get; set; } = new List<Buy>();
        public List<Borrow> Borrows { get; set; }
        public  int price { get; set; }
    }
}

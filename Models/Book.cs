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
        
       
        public  int price { get; set; }

        public ICollection<Buy> Buys { get; set; }
        public ICollection<Borrow> Borrows { get; set; }
    }
}

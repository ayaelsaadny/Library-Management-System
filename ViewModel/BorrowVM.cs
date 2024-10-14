namespace book.ViewModel
{
	public class BorrowVM
	{

		public int BookId { get; set; }
		public string BookName { get; set; }
		public string Author { get; set; }
		public bool availablity { get; set; }
		public DateTime BorrowDate { get; set; }
		public DateTime? ReturnDate { get; set; }
	}
}

using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Author { get; set; }
		public string? Genre { get; set; }
		public string? Category { get; set; }
		public string? Language { get; set; }
		public int TotalPages { get; set; }
		public string? CoverImageUrl { get; set; }
		public string? BookPdfUrl { get; set; }
		public int UserId { get; set; }

		[ForeignKey("UserId")]
		public User? User { get; set; }
	}
}

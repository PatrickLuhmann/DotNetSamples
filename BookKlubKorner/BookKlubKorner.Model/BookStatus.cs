using System.ComponentModel.DataAnnotations;

namespace BookKlubKorner.Model;

public class BookStatus
{
	public int Id { get; set; }

	public int Rating { get; set; }
	public int Progress { get; set; }
	public bool Abandoned { get; set; }
	public string Review { get; set; } = string.Empty;

	// Foreign key for Reader
	public int ReaderId { get; set; }
	public Reader Reader { get; set; }

	// Foreign key for Book
	public int BookId { get; set; }
	public Book Book { get; set; }
}
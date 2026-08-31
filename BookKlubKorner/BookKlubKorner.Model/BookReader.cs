using System.ComponentModel.DataAnnotations;

namespace BookKlubKorner.Model;

public class BookReader
{
	public int Id { get; set; }

	// Foreign key for Book
	public int BookId { get; set; }
	public Book Book { get; set; }

	// Foreign key for Reader
	public int ReaderId { get; set; }
	public Reader Reader { get; set; }

	// Payload data
	[Required]
	[Range(0, 10)]
	public int Rating { get; set; }

	[Required]
	[Range(0, 100)]
	public int Progress { get; set; }
}
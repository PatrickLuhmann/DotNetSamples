namespace BookKlubKorner.Model;

public class Book
{
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;
	public string Author { get; set; } = string.Empty;
	public string Publisher { get; set; } = string.Empty;
	public int NumPages { get; set; }
	public int PublicationYear { get; set; }

	// Collection navigation
	// NOTE: No collection of Readers means this shouldn't be treated as a many-to-many relationship.
	public ICollection<BookStatus> BookStatuses { get; set; } = new List<BookStatus>();
}

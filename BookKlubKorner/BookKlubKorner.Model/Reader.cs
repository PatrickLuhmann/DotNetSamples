namespace BookKlubKorner.Model;

public class Reader
{
	public int Id { get; set; }

	public string Nickname { get; set; } = string.Empty;
	public string Biography { get; set; } = string.Empty;

	// Collection navigation
	// NOTE: No collection of Books means this shouldn't be treated as a many-to-many relationship.
	public ICollection<BookStatus> BookStatuses { get; set; } = new List<BookStatus>();
}
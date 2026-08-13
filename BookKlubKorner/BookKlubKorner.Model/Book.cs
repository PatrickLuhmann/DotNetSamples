using System.ComponentModel.DataAnnotations;

namespace BookKlubKorner.Model;

public class Book
{
	public int Id { get; set; }

	[Required]
	[StringLength(120)]
	[RegularExpression(@"^\S[\S\s]*$")]
	public string Title { get; set; } = string.Empty;

	[Required]
	[StringLength(60)]
	[RegularExpression(@"^\S[\S\s]*$")]
	public string Author { get; set; } = string.Empty;

	[Required]
	[StringLength(60)]
	[RegularExpression(@"^[A-Za-z]+[a-zA-Z()\s-&+]*$")]
	public string Publisher { get; set; } = string.Empty;

	[Required]
	[Range(1, 9999)]
	public int NumPages { get; set; }

	[Required]
	[Range(-4000, 3000)]
	public int PublicationYear { get; set; }

	// TODO: Is this a good way to do this? I don't want to duplicate this code
	// throughout the other layers.
	public void UpdateProperties(Book source)
	{
		Title = source.Title;
		Author = source.Author;
		Publisher = source.Publisher;
		NumPages = source.NumPages;
		PublicationYear = source.PublicationYear;
	}
}

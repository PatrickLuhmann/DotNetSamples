using System.ComponentModel.DataAnnotations;

namespace BookKlubKorner.Model;

public class Book
{
	public int Id { get; set; }

	[Required]
	[StringLength(120)]
	[RegularExpression(@"^\S[\S\s]*$")]
	public string Title { get; set; }

	[Required]
	[StringLength(60)]
	[RegularExpression(@"^[A-Za-z]+[a-zA-Z()\s-&+]*$")]
	public string Publisher { get; set; }

	[Required]
	[Range(1, 9999)]
	public int NumPages { get; set; }

	[Required]
	[Range(-4000, 3000)]
	public int PublicationYear { get; set; }
}

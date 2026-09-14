using System.ComponentModel.DataAnnotations;

namespace blazor_cc.ViewModels;

public class BookViewModel
{
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
	[RegularExpression(@"^\S[\S\s]*$")]
	public string Publisher { get; set; } = string.Empty;

	[Required]
	[Range(1, 9999)]
	public int NumPages { get; set; }

	[Required]
	[Range(-4000, 3000)]
	public int PublicationYear { get; set; }
}
using System.ComponentModel.DataAnnotations;
using BookKlubKorner.Model;

namespace blazor_cc.ViewModels;

public class BookViewModel
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
	[RegularExpression(@"^\S[\S\s]*$")]
	public string Publisher { get; set; } = string.Empty;

	[Required]
	[Range(1, 9999)]
	public int NumPages { get; set; }

	[Required]
	[Range(-4000, 3000)]
	public int PublicationYear { get; set; }

	[Required]
	public List<int> BookStatusIds { get; set; } = [];

	public BookViewModel() { }

	public BookViewModel(Book bookEntity)
	{
		Id = bookEntity.Id;
		Title = bookEntity.Title;
		Author = bookEntity.Author;
		Publisher = bookEntity.Publisher;
		NumPages = bookEntity.NumPages;
		PublicationYear = bookEntity.PublicationYear;
		BookStatusIds = [.. bookEntity.BookStatuses.Select(bs => bs.Id)];
	}
}
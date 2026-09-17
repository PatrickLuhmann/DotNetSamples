using System.ComponentModel.DataAnnotations;
using BookKlubKorner.Model;

namespace blazor_cc.ViewModels;

public class BookStatusViewModel
{
	public int Id { get; set; }

	[Required]
	[Range(0, 10)]
	public int Rating { get; set; }

	[Required]
	[Range(0, 100)]
	public int Progress { get; set; }

	[Required]
	public bool Abandoned { get; set; }

	[StringLength(256)]
	public string Review { get; set; } = string.Empty;

	//public BookViewModel? Book { get; set; }
	public int BookId { get; set; } // ID of the entity

	//public ReaderViewModel? Reader { get; set; }
	public int ReaderId { get; set; } // ID of the entity

	public BookStatusViewModel() { }

	public BookStatusViewModel(BookStatus bookStatusEntity)
	{
		Id = bookStatusEntity.Id;
		Rating = bookStatusEntity.Rating;
		Progress = bookStatusEntity.Progress;
		Abandoned = bookStatusEntity.Abandoned;
		Review = bookStatusEntity.Review;
		//Book = new BookViewModel(bookStatusEntity.Book);
		BookId = bookStatusEntity.BookId;
		//Reader = new ReaderViewModel(bookStatusEntity.Reader);
		ReaderId = bookStatusEntity.ReaderId;
	}
}
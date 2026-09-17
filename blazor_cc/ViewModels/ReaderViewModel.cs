using System.ComponentModel.DataAnnotations;
using BookKlubKorner.Model;

namespace blazor_cc.ViewModels;

public class ReaderViewModel
{
	public int Id { get; set; }

	[Required]
	[StringLength(30)]
	public string Nickname { get; set; } = string.Empty;

	public string Biography { get; set; } = string.Empty;

	//public List<BookStatusViewModel> BookStatuses { get; set; } = [];
	public List<int> BookStatusIds { get; set; } = [];

	public ReaderViewModel() { }

	public ReaderViewModel(Reader readerEntity)
	{
		Id = readerEntity.Id;
		Nickname = readerEntity.Nickname;
		Biography = readerEntity.Biography;

		BookStatusIds = [.. readerEntity.BookStatuses.Select(bs => bs.Id)];
/*
		BookStatuses =
		[
			.. readerEntity.BookStatuses
				.Select(bs => new BookStatusViewModel(bs, this))
		];
*/
	}
}
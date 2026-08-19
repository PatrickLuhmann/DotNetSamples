using BookKlubKorner.Model;

namespace BookKlubKorner.Repository.InMemory;

public class InMemoryBookKlubKornerRepo : IBookKlubKornerRepository
{
	#region IBookKlubKornerRepository

	// Sync

	public int GetCountOfBooks()
	{
		return _bookRepo.Count;
	}

	public IEnumerable<Book> GetAllBooks()
	{
		return _bookRepo.Select(b => new Book()
		{
			Id = b.Id,
			Title = b.Title,
			Author = b.Author,
			Publisher = b.Publisher,
			NumPages = b.NumPages,
			PublicationYear = b.PublicationYear,
		});
	}

	public void AddBook(Book book)
	{
		BookEntity newEntity = new()
		{
			Id = NextId,
			Title = book.Title,
			Author = book.Author,
			Publisher = book.Publisher,
			NumPages = book.NumPages,
			PublicationYear = book.PublicationYear,
		};
		_bookRepo.Add(newEntity);
	}

	public Book? GetBook(int id)
	{
		var entity = _bookRepo.FirstOrDefault(b => b.Id == id);
		if (entity is null)
			return null;
		Book book = new()
		{
			Id = entity.Id,
			Title = entity.Title,
			Author = entity.Author,
			Publisher = entity.Publisher,
			NumPages = entity.NumPages,
			PublicationYear = entity.PublicationYear,
		};
		return book;
	}

	public void UpdateBook(Book book)
	{
		var entity = _bookRepo.FirstOrDefault(b => b.Id == book.Id);
		if (entity is null)
			return;
		entity.Title = book.Title;
		entity.Author = book.Author;
		entity.Publisher = book.Publisher;
		entity.NumPages = book.NumPages;
		entity.PublicationYear = book.PublicationYear;
	}

	public void DeleteBook(int id)
	{
		_bookRepo.RemoveAll(b => b.Id == id);
	}

	// Async

	public async Task<int> GetCountOfBooksAsync()
	{
		await Task.Delay(_delay);
		return GetCountOfBooks();
	}

	public async Task<IEnumerable<Book>> GetAllBooksAsync()
	{
		await Task.Delay(_delay);
		return GetAllBooks();
	}

	public async Task AddBookAsync(Book book)
	{
		await Task.Delay(_delay);
		AddBook(book);
	}

	public async Task<Book?> GetBookAsync(int id)
	{
		await Task.Delay(_delay);
		return GetBook(id);
	}

	public async Task UpdateBookAsync(Book book)
	{
		await Task.Delay(_delay);
		UpdateBook(book);
	}

	public async Task DeleteBookAsync(int id)
	{
		await Task.Delay(_delay);
		DeleteBook(id);
	}

	#endregion

	public InMemoryBookKlubKornerRepo()
	{
		// We might as well start with something.
		_bookRepo.Add(new BookEntity()
		{
			Id = NextId, Title = "The Epic Of Gilgamesh", Author = "Unknown Sumerian",
			Publisher = "Old Babylon Press", NumPages = 5, PublicationYear = -1800,
		});
	}

	private readonly int _delay = 1; // in milliseconds

	private int NextId
	{
		get => field++;
	} = 1;

	private readonly List<BookEntity> _bookRepo = [];

	private class BookEntity
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public string Publisher { get; set; } = string.Empty;
		public int NumPages { get; set; }
		public int PublicationYear { get; set; }
	}
}
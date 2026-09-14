using BookKlubKorner.Model;

namespace BookKlubKorner.Repository.InMemory;

public class InMemoryBookKlubKornerRepo : IBookKlubKornerRepository
{
	#region IBookKlubKornerRepository

	//
	// BOOKS
	//

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

	//
	// READERS
	//

	public int GetCountOfReaders()
	{
		return _readerRepo.Count;
	}

	public async Task<int> GetCountOfReadersAsync()
	{
		await Task.Delay(_delay);
		return GetCountOfReaders();
	}

	public IEnumerable<Reader> GetAllReaders()
	{
		return _readerRepo.Select(r => new Reader()
		{
			Id = r.Id,
			Nickname = r.Nickname,
			Biography = r.Biography,
		});
	}

	public async Task<IEnumerable<Reader>> GetAllReadersAsync()
	{
		await Task.Delay(_delay);
		return GetAllReaders();
	}

	public void AddReader(Reader reader)
	{
		ReaderEntity entity = new()
		{
			Id = NextId,
			Nickname = reader.Nickname,
			Biography = reader.Biography,
		};
		_readerRepo.Add(entity);
	}

	public async Task AddReaderAsync(Reader reader)
	{
		await Task.Delay(_delay);
		AddReader(reader);
	}

	public Reader? GetReader(int id)
	{
		var entity = _readerRepo.FirstOrDefault(r => r.Id == id);
		if (entity is null)
			return null;
		Reader reader = new()
		{
			Id = entity.Id,
			Nickname = entity.Nickname,
			Biography = entity.Biography,
		};
		return reader;
	}

	public async Task<Reader?> GetReaderAsync(int id)
	{
		await Task.Delay(_delay);
		return GetReader(id);
	}

	public void UpdateReader(Reader reader)
	{
		var entity = _readerRepo.FirstOrDefault(r => r.Id == reader.Id);
		if (entity is null)
			return;
		entity.Nickname = reader.Nickname;
		entity.Biography = reader.Biography;
	}

	public async Task UpdateReaderAsync(Reader reader)
	{
		await Task.Delay(_delay);
		UpdateReader(reader);
	}

	public Task DeleteReaderAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<int> GetCountOfBookStatusesAsync()
	{
		throw new NotImplementedException();
	}

	public Task CreateBookStatusAsync(Reader reader, Book book)
	{
		throw new NotImplementedException();
	}

	public void CreateBookStatus(int readerId, int bookId)
	{
		throw new NotImplementedException();
	}

	public Task CreateBookStatusAsync(int readerId, int bookId)
	{
		throw new NotImplementedException();
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

		_readerRepo.Add(new ReaderEntity()
		{
			Id = NextId, Nickname = "BowlingBada$$", Biography = "I'm an air-conditioned gypsy.",
		});
	}

	private readonly int _delay = 1; // in milliseconds

	// We'e using a global ID pool for all entities.
	private int NextId
	{
		get => field++;
	} = 1;

	private readonly List<BookEntity> _bookRepo = [];
	private readonly List<ReaderEntity> _readerRepo = [];

	private class BookEntity
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public string Publisher { get; set; } = string.Empty;
		public int NumPages { get; set; }
		public int PublicationYear { get; set; }
	}

	private class ReaderEntity
	{
		public int Id { get; set; }
		public string Nickname { get; set; } = string.Empty;
		public string Biography { get; set; } = string.Empty;
	}
}
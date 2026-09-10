using BookKlubKorner.Model;
using Microsoft.EntityFrameworkCore;

namespace BookKlubKorner.Repository.EntityFrameworkCore;

public class EntityFrameworkCoreBookKlubKornerRepo : IBookKlubKornerRepository
{
	public int GetCountOfBooks()
	{
		using var context = _contextFactory.CreateDbContext();
		return context.Books.Count();
	}

	public async Task<int> GetCountOfBooksAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return context.Books.Count();
	}

	public IEnumerable<Book> GetAllBooks()
	{
		using var context = _contextFactory.CreateDbContext();
		return [.. context.Books];
	}

	public async Task<IEnumerable<Book>> GetAllBooksAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return [.. context.Books];
	}

	public void AddBook(Book book)
	{
		using var context = _contextFactory.CreateDbContext();
		context.Books.Add(book);
		context.SaveChanges();
	}

	public async Task AddBookAsync(Book book)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		context.Books.Add(book);
		await context.SaveChangesAsync();
	}

	public Book? GetBook(int id)
	{
		using var context = _contextFactory.CreateDbContext();
		return context.Books.Find(id);
	}

	public async Task<Book?> GetBookAsync(int id)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return await context.Books.FindAsync(id);
	}

	public void UpdateBook(Book book)
	{
		using var context = _contextFactory.CreateDbContext();
		var origBook = context.Books.Find(book.Id);
		if (origBook is null)
			throw new Exception($"Book with id={book.Id} not found in database.");
		origBook.Title = book.Title;
		origBook.Author = book.Author;
		origBook.Publisher = book.Publisher;
		origBook.NumPages = book.NumPages;
		origBook.PublicationYear = book.PublicationYear;
		context.SaveChanges();
	}

	public async Task UpdateBookAsync(Book book)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		var origBook = await context.Books.FindAsync(book.Id);
		if (origBook is null)
			throw new Exception($"Book with id={book.Id} not found in database.");
		origBook.Title = book.Title;
		origBook.Author = book.Author;
		origBook.Publisher = book.Publisher;
		origBook.NumPages = book.NumPages;
		origBook.PublicationYear = book.PublicationYear;
		await context.SaveChangesAsync();
	}

	public void DeleteBook(int id)
	{
		using var context = _contextFactory.CreateDbContext();
		var book = context.Books.Find(id);
		if (book is null)
			throw new Exception($"Book with id={id} not found in database.");
		context.Books.Remove(book);
		context.SaveChanges();
	}

	public async Task DeleteBookAsync(int id)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		var book = await context.Books.FindAsync(id);
		if (book is null)
			throw new Exception($"Book with id={id} not found in database.");
		context.Books.Remove(book);
		await context.SaveChangesAsync();
	}

	public int GetCountOfReaders()
	{
		using var context = _contextFactory.CreateDbContext();
		return context.Readers.Count();
	}

	public async Task<int> GetCountOfReadersAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return context.Readers.Count();
	}

	public IEnumerable<Reader> GetAllReaders()
	{
		using var context = _contextFactory.CreateDbContext();
		return [.. context.Readers];
	}

	public async Task<IEnumerable<Reader>> GetAllReadersAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return [.. context.Readers];
	}

	public void AddReader(Reader reader)
	{
		using var context = _contextFactory.CreateDbContext();
		context.Readers.Add(reader);
		context.SaveChanges();
	}

	public async Task AddReaderAsync(Reader reader)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		context.Readers.Add(reader);
		await context.SaveChangesAsync();
	}

	public Reader? GetReader(int id)
	{
		using var context = _contextFactory.CreateDbContext();
		return context.Readers.Find(id);
	}

	public async Task<Reader?> GetReaderAsync(int id)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return await context.Readers.FindAsync(id);
	}

	public void UpdateReader(Reader reader)
	{
		using var context = _contextFactory.CreateDbContext();
		var origReader = context.Readers.Find(reader.Id);
		if (origReader is null)
			throw new Exception($"Reader with id={reader.Id} not found in database.");
		origReader.Nickname = reader.Nickname;
		origReader.Biography = reader.Biography;
		context.SaveChanges();
	}

	public async Task UpdateReaderAsync(Reader reader)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		var origReader = await context.Readers.FindAsync(reader.Id);
		if (origReader is null)
			throw new Exception($"Reader with id={reader.Id} not found in database.");
		origReader.Nickname = reader.Nickname;
		origReader.Biography = reader.Biography;
		await context.SaveChangesAsync();
	}

	public void CreateBookStatus(int readerId, int bookId)
	{
		using var context = _contextFactory.CreateDbContext();
		var reader = context.Readers.Find(readerId);
		if (reader is null)
			throw new Exception($"Reader with id={readerId} not found in database.");
		var book = context.Books.Find(bookId);
		if (book is null)
			throw new Exception($"Book with id={bookId} not found in database.");
		reader.BookStatuses.Add(new BookStatus());
		context.SaveChanges();
	}

	public async Task CreateBookStatusAsync(int readerId, int bookId)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		var reader = await context.Readers.FindAsync(readerId);
		if (reader is null)
			throw new Exception($"Reader with id={readerId} not found in database.");
		var book = await context.Books.FindAsync(bookId);
		if (book is null)
			throw new Exception($"Book with id={bookId} not found in database.");
		var bStatus = new BookStatus();
		reader.BookStatuses.Add(bStatus);
		book.BookStatuses.Add(bStatus);
		await context.SaveChangesAsync();
	}


	private readonly IDbContextFactory<BookKlubKornerContext> _contextFactory;

	public EntityFrameworkCoreBookKlubKornerRepo(IDbContextFactory<BookKlubKornerContext> dbFactory)
	{
		// Grab the factory.
		_contextFactory = dbFactory;

		// Create/Update the databse
		var context = _contextFactory.CreateDbContext();
		context.Database.Migrate();
	}
}
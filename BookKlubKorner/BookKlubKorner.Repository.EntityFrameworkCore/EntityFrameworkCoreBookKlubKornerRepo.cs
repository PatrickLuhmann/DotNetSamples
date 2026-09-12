using BookKlubKorner.Model;
using Microsoft.EntityFrameworkCore;

namespace BookKlubKorner.Repository.EntityFrameworkCore;

public class EntityFrameworkCoreBookKlubKornerRepo : IBookKlubKornerRepository
{
	public async Task<int> GetCountOfBooksAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return context.Books.Count();
	}

	public async Task<IEnumerable<Book>> GetAllBooksAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return [.. context.Books];
	}

	public async Task AddBookAsync(Book book)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		context.Books.Add(book);
		await context.SaveChangesAsync();
	}

	public async Task<Book?> GetBookAsync(int id)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return await context.Books.FindAsync(id);
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

	public async Task DeleteBookAsync(int id)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		var book = await context.Books.FindAsync(id);
		if (book is null)
			throw new Exception($"Book with id={id} not found in database.");
		context.Books.Remove(book);
		await context.SaveChangesAsync();
	}

	public async Task<int> GetCountOfReadersAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return context.Readers.Count();
	}

	public async Task<IEnumerable<Reader>> GetAllReadersAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return [.. context.Readers];
	}

	public async Task AddReaderAsync(Reader reader)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		context.Readers.Add(reader);
		await context.SaveChangesAsync();
	}

	public async Task<Reader?> GetReaderAsync(int id)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		var reader = await context.Readers.FindAsync(id);
		if (reader is null) return reader;
		await context.Entry(reader).Collection(r => r.BookStatuses).LoadAsync();
		foreach (var bookStatus in reader.BookStatuses)
		{
			await context.Entry(bookStatus).Reference(bs => bs.Book).LoadAsync();
		}

		return reader;
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

	public async Task DeleteReaderAsync(int id)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		var reader = await context.Readers.FindAsync(id);
		if (reader is null)
			throw new Exception($"Reader with id={id} not found in database.");
		context.Readers.Remove(reader);
		await context.SaveChangesAsync();
	}

	public async Task<int> GetCountOfBookStatusesAsync()
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		return context.BookStatuses.Count();
	}

	public async Task CreateBookStatusAsync(Reader reader, Book book)
	{
		await using var context = await _contextFactory.CreateDbContextAsync();
		context.Attach(reader);
		context.Attach(book);
		var bookStatus = new BookStatus();
		reader.BookStatuses.Add(bookStatus);
		book.BookStatuses.Add(bookStatus);
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
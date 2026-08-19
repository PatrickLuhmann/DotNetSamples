using BookKlubKorner.Model;
using BookKlubKorner.Repository;

namespace BookKlubKorner.Domain;

public class DesignTimeBookKlubKornerService : IBookKlubKornerService
{
	private readonly IBookKlubKornerRepository _repository;

	public DesignTimeBookKlubKornerService(IBookKlubKornerRepository repo)
	{
		_repository = repo;

		// For design purposes, we want several items already in the list.
		_repository.AddBook(new Book()
		{
			Title = "Book Title 1",
			Author = "John Doe",
			Publisher = "Seagull Home",
			NumPages = 357,
			PublicationYear = 1981,
		});

		_repository.AddBook(new Book()
		{
			Title = "Diamonds Shine Like Swine",
			Author = "Jane Doh",
			Publisher = "Seagull Home",
			NumPages = 1332,
			PublicationYear = 1975
		});
		_repository.AddBook(new Book()
		{
			Title = "An Annoyance Of Patricks",
			Author = "John Doe",
			Publisher = "Seagull Home",
			NumPages = 365,
			PublicationYear = 1970
		});

		// TODO: Add some Readers.
	}

	#region IBookKlubKornerService sync methods

	public List<Book> GetAllBooks()
	{
		return [.. _repository.GetAllBooks()];
	}

	#endregion

	#region IBookKlubKornerService async methods

	// BOOKS

	public async Task<int> GetCountOfBooksAsync()
	{
		return await _repository.GetCountOfBooksAsync();
	}

	public async Task<List<Book>> GetAllBooksAsync()
	{
		return [.. await _repository.GetAllBooksAsync()];
	}

	public async Task<Book?> GetBookByIdAsync(int id)
	{
		return await _repository.GetBookAsync(id);
	}

	public async Task UpdateBookAsync(Book source)
	{
		await _repository.UpdateBookAsync(source);
	}

	public async Task DeleteBookAsync(int id)
	{
		await _repository.DeleteBookAsync(id);
	}

	public async Task<Book> CreateBookAsync(Book source)
	{
		await _repository.AddBookAsync(source);
		return source;
	}

	// READERS

	public async Task<int> GetCountOfReadersAsync()
	{
		return await _repository.GetCountOfReadersAsync();
	}

	public async Task<List<Reader>> GetAllReadersAsync()
	{
		return [.. await _repository.GetAllReadersAsync()];
	}

	public async Task CreateReaderAsync(Reader reader)
	{
		await _repository.AddReaderAsync(reader);
	}

	public async Task<Reader?> GetReaderByIdAsync(int id)
	{
		return await _repository.GetReaderAsync(id);
	}

	public async Task UpdateReaderAsync(Reader reader)
	{
		await _repository.UpdateReaderAsync(reader);
	}

	#endregion
}
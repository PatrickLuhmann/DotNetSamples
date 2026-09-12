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
		if (GetCountOfBooks() == 0)
		{
			CreateBook(new Book()
			{
				Title = "Book Title 1",
				Author = "John Doe",
				Publisher = "Seagull Home",
				NumPages = 357,
				PublicationYear = 1981,
			});
			CreateBook(new Book()
			{
				Title = "Diamonds Shine Like Swine",
				Author = "Jane Doh",
				Publisher = "Seagull Home",
				NumPages = 1332,
				PublicationYear = 1975
			});
			CreateBook(new Book()
			{
				Title = "An Annoyance Of Patricks",
				Author = "John Doe",
				Publisher = "Seagull Home",
				NumPages = 365,
				PublicationYear = 1970
			});
		}

		// TODO: Add some Readers.
	}

	// BOOKS

	public async Task<int> GetCountOfBooksAsync()
	{
		return await _repository.GetCountOfBooksAsync();
	}

	public int GetCountOfBooks() { return GetCountOfBooksAsync().Result; }

	public async Task<List<Book>> GetAllBooksAsync()
	{
		return [.. await _repository.GetAllBooksAsync()];
	}

	public List<Book> GetAllBooks() { return GetAllBooksAsync().Result; }

	public async Task CreateBookAsync(Book source)
	{
		await _repository.AddBookAsync(source);
	}

	public void CreateBook(Book source) { CreateBookAsync(source).Wait(); }

	public async Task<Book?> GetBookByIdAsync(int id)
	{
		return await _repository.GetBookAsync(id);
	}

	public Book? GetBookById(int id) { return GetBookByIdAsync(id).Result; }

	public async Task UpdateBookAsync(Book source)
	{
		await _repository.UpdateBookAsync(source);
	}

	public void UpdateBook(Book source) { UpdateBookAsync(source).Wait(); }

	public async Task DeleteBookAsync(int id)
	{
		await _repository.DeleteBookAsync(id);
	}

	public void DeleteBook(int id) { DeleteBookAsync(id).Wait(); }

	// READERS

	public async Task<int> GetCountOfReadersAsync()
	{
		return await _repository.GetCountOfReadersAsync();
	}

	public int GetCountOfReaders() { return GetCountOfReadersAsync().Result; }

	public async Task<List<Reader>> GetAllReadersAsync()
	{
		return [.. await _repository.GetAllReadersAsync()];
	}

	public List<Reader> GetAllReaders() { return GetAllReadersAsync().Result; }

	public async Task CreateReaderAsync(Reader reader)
	{
		await _repository.AddReaderAsync(reader);
	}

	public void CreateReader(Reader reader) { CreateReaderAsync(reader).Wait(); }

	public async Task<Reader?> GetReaderByIdAsync(int id)
	{
		return await _repository.GetReaderAsync(id);
	}

	public Reader? GetReaderById(int id) { return GetReaderByIdAsync(id).Result; }

	public async Task UpdateReaderAsync(Reader reader)
	{
		await _repository.UpdateReaderAsync(reader);
	}

	public void UpdateReader(Reader reader) { UpdateReaderAsync(reader).Wait(); }

	public async Task DeleteReaderAsync(int id)
	{
		await _repository.DeleteReaderAsync(id);
	}

	public void DeleteReader(int id) { DeleteReaderAsync(id).Wait(); }

	// BOOK STATUSES

	public async Task<int> GetCountOfBookStatusesAsync()
	{
		return await _repository.GetCountOfBookStatusesAsync();
	}

	public int GetCountOfBookStatuses() { return GetCountOfBookStatusesAsync().Result; }

	public async Task CreateBookStatusAsync(Reader reader, Book book)
	{
		await _repository.CreateBookStatusAsync(reader, book);
	}

	public void CreateBookStatus(Reader reader, Book book) { CreateBookStatusAsync(reader, book).Wait(); }
}
using BookKlubKorner.Model;

namespace BookKlubKorner.Domain;

public interface IBookKlubKornerService
{
	// Sync
	List<Book> GetAllBooks();

	// Async
	Task<int> GetCountOfBooksAsync();
	Task<List<Book>> GetAllBooksAsync();
	Task<Book?> GetBookByIdAsync(int id);
	Task UpdateBookAsync(Book source);
	Task DeleteBookAsync(int id);
	Task<Book> CreateBookAsync(Book source);
}

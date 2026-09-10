using BookKlubKorner.Model;

namespace BookKlubKorner.Domain;

public interface IBookKlubKornerService
{
	//
	// BOOKS
	//

	// Sync
	List<Book> GetAllBooks();

	// Async
	Task<int> GetCountOfBooksAsync();
	Task<List<Book>> GetAllBooksAsync();
	Task<Book?> GetBookByIdAsync(int id);
	Task UpdateBookAsync(Book source);
	Task DeleteBookAsync(int id);
	Task<Book> CreateBookAsync(Book source);

	//
	// READERS
	//

	Task<int> GetCountOfReadersAsync();
	Task<List<Reader>> GetAllReadersAsync();
	Task CreateReaderAsync(Reader reader);
	Task<Reader?> GetReaderByIdAsync(int id);
	Task UpdateReaderAsync(Reader reader);

	//
	// BOOK STATUSES
	//

	//void CreateBookStatus(int readerId, int bookId);
	Task CreateBookStatusAsync(int readerId, int bookId);
}

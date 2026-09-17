using BookKlubKorner.Model;

namespace BookKlubKorner.Repository;

public interface IBookKlubKornerRepository
{
	//
	// BOOKS
	//

	Task<int> GetCountOfBooksAsync();
	Task<IEnumerable<Book>> GetAllBooksAsync();
	Task AddBookAsync(Book book);
	Task<Book?> GetBookAsync(int id);
	Task UpdateBookAsync(Book book);
	Task DeleteBookAsync(int id);

	//
	// READERS
	//

	Task<int> GetCountOfReadersAsync();
	Task<IEnumerable<Reader>> GetAllReadersAsync();
	Task AddReaderAsync(Reader reader);
	Task<Reader?> GetReaderAsync(int id);
	Task UpdateReaderAsync(Reader reader);
	Task DeleteReaderAsync(int id);

	//
	// BOOK STATUSES
	//

	Task<int> GetCountOfBookStatusesAsync();
	Task<List<BookStatus>> GetAllBookStatusesAsync();
	Task CreateBookStatusAsync(Reader reader, Book book);
	Task<BookStatus?> GetBookStatusAsync(int id);
}
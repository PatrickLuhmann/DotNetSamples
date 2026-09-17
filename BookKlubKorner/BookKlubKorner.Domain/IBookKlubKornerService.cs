using BookKlubKorner.Model;

namespace BookKlubKorner.Domain;

public interface IBookKlubKornerService
{
	//
	// BOOKS
	//

	int GetCountOfBooks();
	Task<int> GetCountOfBooksAsync();

	List<Book> GetAllBooks();
	Task<List<Book>> GetAllBooksAsync();

	void CreateBook(Book source);
	Task CreateBookAsync(Book source);

	Book? GetBookById(int id);
	Task<Book?> GetBookByIdAsync(int id);

	void UpdateBook(Book source);
	Task UpdateBookAsync(Book source);

	void DeleteBook(int id);
	Task DeleteBookAsync(int id);

	//
	// READERS
	//

	int GetCountOfReaders();
	Task<int> GetCountOfReadersAsync();

	List<Reader> GetAllReaders();
	Task<List<Reader>> GetAllReadersAsync();

	void CreateReader(Reader reader);
	Task CreateReaderAsync(Reader reader);

	Reader? GetReaderById(int id);
	Task<Reader?> GetReaderByIdAsync(int id);

	void UpdateReader(Reader reader);
	Task UpdateReaderAsync(Reader reader);

	void DeleteReader(int id);
	Task DeleteReaderAsync(int id);

	//
	// BOOK STATUSES
	//

	int GetCountOfBookStatuses();
	Task<int> GetCountOfBookStatusesAsync();

	List<BookStatus> GetAllBookStatuses();
	Task<List<BookStatus>> GetAllBookStatusesAsync();

	void CreateBookStatus(Reader reader, Book book);
	Task CreateBookStatusAsync(Reader reader, Book book);

	BookStatus? GetBookStatusById(int id);
	Task<BookStatus?> GetBookStatusByIdAsync(int id);
}

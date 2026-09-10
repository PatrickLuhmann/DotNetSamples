using BookKlubKorner.Model;

namespace BookKlubKorner.Repository;

public interface IBookKlubKornerRepository
{
	//
	// BOOKS
	//

	int GetCountOfBooks();
	IEnumerable<Book> GetAllBooks();
	void AddBook(Book book);
	Book? GetBook(int id);
	void UpdateBook(Book book);
	void DeleteBook(int id);

	Task<int> GetCountOfBooksAsync();
	Task<IEnumerable<Book>> GetAllBooksAsync();
	Task AddBookAsync(Book book);
	Task<Book?> GetBookAsync(int id);
	Task UpdateBookAsync(Book book);
	Task DeleteBookAsync(int id);

	//
	// READERS
	//

	int GetCountOfReaders();
	Task<int> GetCountOfReadersAsync();

	IEnumerable<Reader> GetAllReaders();
	Task<IEnumerable<Reader>> GetAllReadersAsync();

	void AddReader(Reader reader);
	Task AddReaderAsync(Reader reader);

	Reader? GetReader(int id);
	Task<Reader?> GetReaderAsync(int id);

	void UpdateReader(Reader reader);
	Task UpdateReaderAsync(Reader reader);

	//
	// BOOK STATUSES
	//

	void CreateBookStatus(int readerId, int bookId);
	Task CreateBookStatusAsync(int readerId, int bookId);
}
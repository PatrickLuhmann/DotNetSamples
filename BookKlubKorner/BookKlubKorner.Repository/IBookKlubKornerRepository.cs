using BookKlubKorner.Model;

namespace BookKlubKorner.Repository;

public interface IBookKlubKornerRepository
{
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
}
using BookKlubKorner.Model;

namespace BookKlubKorner.Domain;

public interface IBookKlubKornerService
{
	// Sync
	List<Book> GetAllBooks();
	void AddBook(string title, string author, string publisher, int pages, int year);

	// Async
	Task<List<Book>> GetAllBooksAsync();
	Task<Book?> GetBookByIdAsync(int id);
	Task UpdateBookAsync(Book source);
	Task DeleteBookAsync(int id);
}

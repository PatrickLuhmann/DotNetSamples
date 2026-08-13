using BookKlubKorner.Model;

namespace BookKlubKorner.Domain;

public interface IBookKlubKornerService
{
	// Sync
	List<Book> GetAllBooks();
	void AddBook(string title, string publisher, int pages, int year);

	// Async
	Task<List<Book>> GetAllBooksAsync();
	Task<Book?> GetBookByIdAsync(int id);
	Task UpdateBook(Book source);
}

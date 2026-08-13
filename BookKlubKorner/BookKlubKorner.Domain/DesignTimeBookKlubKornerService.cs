using BookKlubKorner.Model;

namespace BookKlubKorner.Domain;

public class DesignTimeBookKlubKornerService : IBookKlubKornerService
{
	private List<Book> _designTimeBooks = [];
	private int _nextId = 1;
	private int _designDelay = 1;

	public List<Book> GetAllBooks()
	{
		throw new NotImplementedException();
	}

	public void AddBook(string title, string publisher, int pages, int year)
	{
		_designTimeBooks.Add(new Book()
		{
			Id = _nextId++,
			Title = title,
			Publisher = publisher,
			NumPages = pages,
			PublicationYear = year,
		});
	}

	public async Task<List<Book>> GetAllBooksAsync()
	{
		await Task.Delay(_designDelay);
		if (_designTimeBooks.Count == 0)
			PopulateBookList();
		return _designTimeBooks;
	}

	public async Task<Book?> GetBookByIdAsync(int id)
	{
		await Task.Delay(_designDelay);
		Book? theBook = _designTimeBooks.FirstOrDefault(b => b.Id == id);
		return theBook;
	}

	public async Task UpdateBook(Book source)
	{
		await Task.Delay(_designDelay);
		Book? targetBook = _designTimeBooks.FirstOrDefault(b => b.Id == source.Id);
		if (targetBook is null)
			return; // TODO: Throw an exception instead?
		targetBook.Title = source.Title;
		targetBook.Publisher = source.Publisher;
		targetBook.NumPages = source.NumPages;
		targetBook.PublicationYear = source.PublicationYear;
	}

	private void PopulateBookList()
	{
		AddBook(title: "Book Title 1", publisher: "Seagull Home", pages: 357, year: 1981);
		AddBook(title: "Diamonds Shine Like Swine", publisher: "Seagull Home", pages: 1332, year: 1975);
		AddBook(title: "An Annoyance Of Patricks", publisher: "Seagull Home", pages: 365, year: 1970);
	}
}
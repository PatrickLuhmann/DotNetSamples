using BookKlubKorner.Model;

namespace BookKlubKorner.Domain;

public class DesignTimeBookKlubKornerService : IBookKlubKornerService
{
	private readonly List<Book> _designTimeBooks = [];
	private int _nextId = 1;
	private readonly int _designDelay = 1;

	public DesignTimeBookKlubKornerService()
	{
		PopulateBookList();
	}

	public List<Book> GetAllBooks()
	{
		throw new NotImplementedException();
	}

	#region IBookKlubKornerService async methods

	public async Task<int> GetCountOfBooksAsync()
	{
		await Task.Delay(_designDelay);
		return _designTimeBooks.Count;
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

	public async Task UpdateBookAsync(Book source)
	{
		await Task.Delay(_designDelay);
		Book? targetBook = _designTimeBooks.FirstOrDefault(b => b.Id == source.Id);
		if (targetBook is null)
			return; // TODO: Throw an exception instead?
		targetBook.UpdateProperties(source);
	}

	public async Task DeleteBookAsync(int id)
	{
		await Task.Delay(_designDelay);
		// TODO: Do we throw an exception if id isn't valid? Or just let the caller figure it out?
		_designTimeBooks.RemoveAll(b => b.Id == id);
	}

	public async Task<Book> CreateBookAsync(Book source)
	{
		await Task.Delay(_designDelay);
		Book book = new()
		{
			Title = source.Title,
			Author = source.Author,
			NumPages = source.NumPages,
			Publisher = source.Publisher,
			PublicationYear = source.PublicationYear,
			Id = _nextId++,
		};
		_designTimeBooks.Add(book);
		return book;
	}

	#endregion

	private void AddBook(string title, string author, string publisher, int pages, int year)
	{
		_designTimeBooks.Add(new Book()
		{
			Id = _nextId++,
			Title = title,
			Author = author,
			Publisher = publisher,
			NumPages = pages,
			PublicationYear = year,
		});
	}

	private void PopulateBookList()
	{
		AddBook(title: "Book Title 1", author: "John Doe", publisher: "Seagull Home", pages: 357, year: 1981);
		AddBook(title: "Diamonds Shine Like Swine", author: "Jane Doh", publisher: "Seagull Home", pages: 1332,
			year: 1975);
		AddBook(title: "An Annoyance Of Patricks", author: "John Doe", publisher: "Seagull Home", pages: 365,
			year: 1970);
	}
}
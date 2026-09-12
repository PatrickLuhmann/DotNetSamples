using BookKlubKorner.Domain;
using BookKlubKorner.Model;
using BookKlubKorner.Repository.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConsoleCC;

public class BookKlubKornerSample : IConsoleSample
{
	public void Run()
	{
		Console.WriteLine("This is the Book Klub Korner sample.");

		// We need a BookKlubKornerService to do actual work.

		// Use my local factory type because the design-time version
		// doesn't implement the correct interface.
		// TODO: Also use a service for this.
		var optBuilder = new DbContextOptionsBuilder<BookKlubKornerContext>();
		optBuilder.UseSqlite("Data Source=bkk-sample.db");
		var factory = new MyDbContextFactory(optBuilder.Options);
		var efRepo = new EntityFrameworkCoreBookKlubKornerRepo(factory);
		var bkkService = new DesignTimeBookKlubKornerService(efRepo);

		// Now access the database.
		Console.WriteLine($"Number of books in the database: {bkkService.GetCountOfBooks()}.");
		Console.WriteLine($"Number of readerss in the database: {bkkService.GetCountOfReaders()}.");
		Console.WriteLine($"Number of book statuses in the database: {bkkService.GetCountOfBookStatuses()}.");

		// Create three new books to work with.
		var book1 = new Book()
		{
			Title = "Sample Book 1",
			Author = "Sample Author",
			Publisher = "Sample Publisher",
			PublicationYear = 1972,
			NumPages = 311,
		};
		bkkService.CreateBook(book1);
		PrintBook(book1);
		Console.WriteLine();

		var book2 = new Book()
		{
			Title = "Sample Book 2",
			Author = "Sample Author",
			Publisher = "Sample Publisher",
			PublicationYear = 1973,
			NumPages = 112,
		};
		bkkService.CreateBook(book2);
		PrintBook(book2);
		Console.WriteLine();

		var book3 = new Book()
		{
			Title = "Sample Book 3",
			Author = "Sample Author",
			Publisher = "Sample Publisher",
			PublicationYear = 1974,
			NumPages = 722,
		};
		bkkService.CreateBook(book3);
		PrintBook(book3);
		Console.WriteLine();

		// Create a new reader to work with.
		var reader1 = new Reader()
		{
			Nickname = "Sample Reader 1",
			Biography = "This is my biography.",
		};
		bkkService.CreateReader(reader1);
		PrintReader(reader1);
		Console.WriteLine();

		// Create a status for the first book.
		Console.WriteLine("CreateBookStatus using IDs.");
		bkkService.CreateBookStatus(reader1, book1);
		PrintReader(reader1, true);
		Console.WriteLine();

		// Create a status for the second book.
		Console.WriteLine("CreateBookStatus using entities.");
		bkkService.CreateBookStatus(reader1, book2);
		PrintReader(reader1, true);
		Console.WriteLine();

		// Grab a new version of the reader to check that the collection is not empty.
		var reader1Clone = bkkService.GetReaderById(reader1.Id);
		Console.WriteLine("Here is a fresh copy of the reader.");
		PrintReader(reader1Clone, true);

		// Clean up after ourselves before we leave.
		// If cascade delete works correcly, the BookStatus entities will automatically be removed.
		bkkService.DeleteReader(reader1.Id);
		// I verified that at this point the BookStatus entities were removed from the database.
		bkkService.DeleteBook(book1.Id);
		bkkService.DeleteBook(book2.Id);
		bkkService.DeleteBook(book3.Id);
		// NOTE: Our objects are still populated, including the navigation properties.
		//       Removing from the database did nothing to what we have in memory.

		Console.WriteLine($"Number of books in the database: {bkkService.GetCountOfBooks()}.");
		Console.WriteLine($"Number of readers in the database: {bkkService.GetCountOfReaders()}.");
		Console.WriteLine($"Number of book statuses in the database: {bkkService.GetCountOfBookStatuses()}.");
	}

	private static void PrintBook(Book? book, bool printStatuses = false)
	{
		if (book is null)
		{
			Console.WriteLine("ERROR: book is NULL.");
			return;
		}

		Console.WriteLine($"Book [{book.Id}]");
		Console.WriteLine("--------");
		Console.WriteLine($"  Title: {book.Title}");
		Console.WriteLine($"  Author: {book.Author}");
		Console.WriteLine($"  NumPages: {book.NumPages}");
		Console.WriteLine($"  Publisher: {book.Publisher}");
		Console.WriteLine($"  PublicationYear: {book.PublicationYear}");
		Console.WriteLine($"  Number of book statuses: {book.BookStatuses.Count}");
		if (!printStatuses) return;
		foreach (var bookStatus in book.BookStatuses)
		{
			Console.WriteLine($"  - BookStatus [{bookStatus.Id}] " +
				$"Rating: {bookStatus.Rating} " +
				$"Progress: {bookStatus.Progress}% " +
				$"Abandoned: {bookStatus.Abandoned} " +
				$"Reader: {bookStatus.Reader.Nickname}");
		}
	}

	private static void PrintReader(Reader? reader, bool printStatuses = false)
	{
		if (reader is null)
		{
			Console.WriteLine("ERROR: reader is NULL.");
			return;
		}

		Console.WriteLine($"Reader [{reader.Id}]");
		Console.WriteLine("--------");
		Console.WriteLine($"  Nickname: {reader.Nickname}");
		Console.WriteLine($"  Biography: {reader.Biography}");
		Console.WriteLine($"  Number of book statuses: {reader.BookStatuses.Count}");
		if (!printStatuses) return;
		foreach (var bookStatus in reader.BookStatuses)
		{
			Console.WriteLine($"  - BookStatus [{bookStatus.Id}] " +
				$"Rating: {bookStatus.Rating} " +
				$"Progress: {bookStatus.Progress}% " +
				$"Abandoned: {bookStatus.Abandoned} " +
				$"Book: {bookStatus.Book.Title}");
		}
	}
}

public class MyDbContextFactory(DbContextOptions<BookKlubKornerContext> options)
	: IDbContextFactory<BookKlubKornerContext>
{
	public BookKlubKornerContext CreateDbContext()
	{
		return new BookKlubKornerContext(options);
	}
}
using BookKlubKorner.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookKlubKorner.Repository.EntityFrameworkCore;

public class BookKlubKornerContext(DbContextOptions<BookKlubKornerContext> options) : DbContext(options)
{
	public DbSet<Book> Books { get; set; }
	public DbSet<Reader> Readers { get; set; }
	public DbSet<BookStatus> BookStatuses { get; set; }
}

// This is so 'dotnet ef migrations' commands will work.
public class BookKlubKornerContextFactory : IDesignTimeDbContextFactory<BookKlubKornerContext>
{
	// First arg is database path.
	// TODO: Make this more formal if it actually gets used to any extent.
	public BookKlubKornerContext CreateDbContext(string[] args)
	{
		string databasePath = "bookklubkorner-dt.db";
		if (args.Length == 1)
			databasePath = args[0];
		var optionsBuilder = new DbContextOptionsBuilder<BookKlubKornerContext>();
		optionsBuilder.UseSqlite($"Data Source={databasePath}");

		return new BookKlubKornerContext(optionsBuilder.Options);
	}
}
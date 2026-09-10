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
	public BookKlubKornerContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<BookKlubKornerContext>();
		optionsBuilder.UseSqlite("Data Source=bookklubkorner-dt.db");

		return new BookKlubKornerContext(optionsBuilder.Options);
	}
}
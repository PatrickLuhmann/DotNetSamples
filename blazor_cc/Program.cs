using blazor_cc;
using blazor_cc.Components;
using BlazorWebAppMovies.Data;
using BookKlubKorner.Domain;
using BookKlubKorner.Repository;
using BookKlubKorner.Repository.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Create the app with preconfigured defaults.
var builder = WebApplication.CreateBuilder(args);

// Add factory for EF Core
builder.Services.AddDbContextFactory<BlazorWebAppMoviesContext>(options =>
	options.UseSqlite(
		builder.Configuration.GetConnectionString("BlazorWebAppMoviesContext") ??
		throw new InvalidOperationException("Connection string 'BlazorWebAppMoviesContext' not found.")));

builder.Services.AddDbContextFactory<BookKlubKornerContext>(options => options
	.UseSqlite(
		builder.Configuration.GetConnectionString("BookKlubKornerContext") ??
		throw new InvalidOperationException("Connection string 'BookKlubKornerContext' not found."))
	.EnableSensitiveDataLogging());

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

// TODO: A database service will use AddScoped instead so that a new instance is created for each web request.
builder.Services.AddSingleton<ITodoItemRepository, InMemoryTodoItemRepository>();
// TODO: Replace this with a "real" service for the published version.
builder.Services.AddSingleton<ITodoItemService, DesignTimeTodoItemService>();

//builder.Services.AddSingleton<IBookKlubKornerRepository, InMemoryBookKlubKornerRepo>();
builder.Services.AddSingleton<IBookKlubKornerRepository, EntityFrameworkCoreBookKlubKornerRepo>();
builder.Services.AddSingleton<IBookKlubKornerService, DesignTimeBookKlubKornerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();

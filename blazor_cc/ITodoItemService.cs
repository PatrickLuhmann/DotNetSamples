namespace blazor_cc;

public interface ITodoItemService
{
	// Basic fetch methods.
	List<TodoItem> GetCompleteItems();
	List<TodoItem> GetIncompleteItems();

	// Supposedly real database access will need to use async.
	Task<List<TodoItem>> GetCompleteItemsAsync();
	Task<List<TodoItem>> GetIncompleteItemsAsync();
}
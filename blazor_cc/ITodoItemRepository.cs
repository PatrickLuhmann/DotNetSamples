namespace blazor_cc;

public interface ITodoItemRepository
{
	void Add(TodoItem item);
	TodoItem Get(int id);
	IEnumerable<TodoItem> GetAll();
	IEnumerable<TodoItem> GetCompleteItems();
	IEnumerable<TodoItem> GetIncompleteItems();
	void Update(TodoItem item);
	void Delete(int id);
}
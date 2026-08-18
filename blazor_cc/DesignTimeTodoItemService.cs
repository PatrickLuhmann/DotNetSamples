namespace blazor_cc;

public class DesignTimeTodoItemService : ITodoItemService
{
	private readonly ITodoItemRepository _repository;

	public DesignTimeTodoItemService(ITodoItemRepository repo)
	{
		_repository = repo;

		// For design purposed, we want several items already in the list.
		_repository.Add(new TodoItem { Title = "Do the dishes", IsDone = false });
		_repository.Add(new TodoItem { Title = "Reshingle the roof", IsDone = false });
		_repository.Add(new TodoItem { Title = "Sand the floor", IsDone = true });
		_repository.Add(new TodoItem { Title = "Wash the car", IsDone = true });
	}

	public List<TodoItem> GetAllItems()
	{
		var items = _repository.GetAll().ToList();
		return items;
	}

	public List<TodoItem> GetCompleteItems()
	{
		var items = _repository.GetCompleteItems().ToList();
		return items;
	}

	public List<TodoItem> GetIncompleteItems()
	{
		var items = _repository.GetIncompleteItems().ToList();
		return items;
	}

	public async Task<List<TodoItem>> GetCompleteItemsAsync()
	{
		await Task.Delay(1);
		//List<TodoItem> items = [.. _designTimeData.Where(i => i.IsDone)];
		var items = _repository.GetCompleteItems().ToList();
		return items;
	}

	public async Task<List<TodoItem>> GetIncompleteItemsAsync()
	{
		await Task.Delay(1);
		//List<TodoItem> items = [.. _designTimeData.Where(i => !i.IsDone)];
		var items = _repository.GetIncompleteItems().ToList();
		return items;
	}
}
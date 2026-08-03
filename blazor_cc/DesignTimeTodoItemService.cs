namespace blazor_cc;

public class DesignTimeTodoItemService : ITodoItemService
{
	// Create a list to be used during design, before persistence is added.
	private readonly List<TodoItem> _designTimeData =
	[
		new() { Title = "Do the dishes", IsDone = false },
		new() { Title = "Reshingle the roof", IsDone = false },
		new() { Title = "Sand the floor", IsDone = true },
		new() { Title = "Wash the car", IsDone = true }
	];

	public List<TodoItem> GetCompleteItems()
	{
		List<TodoItem> items = [.. _designTimeData.Where(i => i.IsDone)];
		return items;
	}

	public List<TodoItem> GetIncompleteItems()
	{
		List<TodoItem> items = [.. _designTimeData.Where(i => !i.IsDone)];
		return items;
	}

	public async Task<List<TodoItem>> GetCompleteItemsAsync()
	{
		// Fake blocking to simulate database access.
		await Task.Delay(1);
		List<TodoItem> items = [.. _designTimeData.Where(i => i.IsDone)];
		return items;
	}

	public async Task<List<TodoItem>> GetIncompleteItemsAsync()
	{
		// Fake blocking to simulate database access.
		await Task.Delay(1);
		List<TodoItem> items = [.. _designTimeData.Where(i => !i.IsDone)];
		return items;
	}
}
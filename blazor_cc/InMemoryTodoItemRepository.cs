namespace blazor_cc;

public class InMemoryTodoItemRepository : ITodoItemRepository
{
	#region ITodoItemRepository

	// TODO: Should this method return the id, since it is used elsewhere?
	public void Add(TodoItem item)
	{
		// TODO: Do we have a rule about duplicates?
		var entity = new TodoItemEntity()
		{
			Id = NextId,
			Title = item.Title,
			IsDone = item.IsDone,
		};
		_dataRepo.Add(entity);
	}

	public TodoItem Get(int id)
	{
		TodoItemEntity? entity = _dataRepo.Find(ti => ti.Id == id);
		if (entity is null)
			throw new Exception();
		TodoItem item = new() { Title = entity.Title, IsDone = entity.IsDone, };
		return item;
	}

	public IEnumerable<TodoItem> GetAll()
	{
		List<TodoItem> temp1 =
		[
			..
			_dataRepo
				.Select(e => new TodoItem()
				{
					Title = e.Title,
					IsDone = e.IsDone,
				})
		];
		return temp1;
	}

	public IEnumerable<TodoItem> GetCompleteItems()
	{
		List<TodoItem> temp1 =
		[
			..
			_dataRepo
				.Where(e => e.IsDone)
				.Select(e => new TodoItem()
				{
					Title = e.Title,
					IsDone = e.IsDone,
				})
		];
		return temp1;
	}

	public IEnumerable<TodoItem> GetIncompleteItems()
	{
		List<TodoItem> temp1 =
		[
			..
			_dataRepo
				.Where(e => !e.IsDone)
				.Select(e => new TodoItem()
				{
					Title = e.Title,
					IsDone = e.IsDone,
				})
		];
		return temp1;
	}

	public void Update(TodoItem item)
	{
		throw new NotImplementedException();
	}

	public void Delete(int id)
	{
		throw new NotImplementedException();
	}

	#endregion

	public InMemoryTodoItemRepository()
	{
		_dataRepo.Add(new() { Id = NextId, Title = "Mark this item complete", IsDone = false, });
	}

	private int NextId
	{
		get => field++;
	} = 1;

	private List<TodoItemEntity> _dataRepo = [];

	private class TodoItemEntity
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool IsDone { get; set; }
	}
}
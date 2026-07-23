using Nutrition.ViewModels;

namespace ConsoleCC;

public class NutritionSample : IConsoleSample
{
	private readonly Nutrition_VM _nutritionVm = new();
	private int _nextId;

	public void Run()
	{
		Console.WriteLine("Welcome to the Nutrtion sample.");

		_nextId = 1;
		foreach (var item in _nutritionVm.FoodItems)
		{
			item.FoodRecord.Id = _nextId++;
		}

		bool quit = false;
		do
		{
			int itemId;
			FoodItem_VM? item;
			Console.WriteLine($"There are {_nutritionVm.FoodItems.Count} food items in the database.");
			Console.WriteLine();
			Console.WriteLine("Enter a command.");
			string? userInput = Console.ReadLine();
            if (userInput is null)
                continue;
			string[] tokens = userInput.Split();
			switch (tokens[0].ToLower())
			{
				case "quit":
					quit = true;
					break;
				case "list":
					ListFoodItems();
					break;
				case "add":
					AddFoodItem();
					break;
				case "edit":
					if (tokens.Length != 2)
					{
						Console.WriteLine("ERROR: Incorrect number of parameters! This command takes 1 parameter.");
						break;
					}
					itemId = Convert.ToInt32(tokens[1]);
					if (itemId <= 0)
					{
						Console.WriteLine("ERROR: The parameter must be a positive integer!");
						break;
					}
					item = _nutritionVm.FoodItems.FirstOrDefault(fi => fi.FoodRecord.Id == itemId);
					if (item is null)
					{
						Console.WriteLine($"ERROR: No food item exists with ID {itemId}!");
					}
					else
					{
						_nutritionVm.SelectedFoodItem = item;
						EditFoodItem_VM efidb = new("Edit Food Item", _nutritionVm.SelectedFoodItem);
						_nutritionVm.EditSelectedFoodItem(efidb);
					}
					break;
				case "del":
				case "delete":
					if (tokens.Length != 2)
					{
						Console.WriteLine("ERROR: Incorrect number of parameters! This command takes 1 parameter.");
						break;
					}
					itemId = Convert.ToInt32(tokens[1]);
					if (itemId <= 0)
					{
						Console.WriteLine("ERROR: The parameter must be a positive integer!");
						break;
					}
					item = _nutritionVm.FoodItems.FirstOrDefault(fi => fi.FoodRecord.Id == itemId);
					if (item is null)
					{
						Console.WriteLine($"ERROR: No food item exists with ID {itemId}!");
					}
					else
					{
						_nutritionVm.SelectedFoodItem = item;
						_nutritionVm.DeleteSelectedFoodItem();
					}
					break;
				case "help":
					PrintHelp();
					break;
				default:
					Console.Write("ERROR: Invalid option!");
					break;
			}

		} while (!quit);
	}

	public void AddFoodItem()
	{
		EditFoodItem_VM efi = new("Add Food Item", new FoodItem_VM("New Item", "New Brand"));
		_nutritionVm.AddNewFoodItem(efi);
		// TODO: We have to fake the ID because we don't have real persistence.
        // TODO: Also, this fails if AddNewFoodItem didn't succeed.
		_nutritionVm.SelectedFoodItem?.FoodRecord.Id = _nextId++;
	}

	public void ListFoodItems()
	{
		Console.WriteLine();
		Console.WriteLine("FoodItems database");
		Console.WriteLine("==================");
		Console.WriteLine(" ID   Name                 Brand");
		foreach (var item in _nutritionVm.FoodItems)
		{
			Console.WriteLine($"[{item.FoodRecord.Id}] {item.FoodRecord.Name} - {item.FoodRecord.Brand}");
		}
		Console.WriteLine();
	}

    // TODO: If this is only printing the selected item, shouldn't it be called PrintSelectedFoodItem?
    // TODO: Shouldn't there be a version that prints a given FoodItem?
    // TODO: Where is this going to be used?
	public void PrintFoodItem()
	{
		if (_nutritionVm.SelectedFoodItem is null)
			return;
		var rec = _nutritionVm.SelectedFoodItem.FoodRecord;
		Console.WriteLine($"{rec.Name}");
		Console.WriteLine($"{rec.Brand}");
		Console.WriteLine($"Nutrition Facts");
		Console.WriteLine($"---------------");
		Console.WriteLine($"Serving Size {rec.ServingSize} {rec.ServingUnit}");
		Console.WriteLine($"Total Fat {rec.TotalFat} g");
		Console.WriteLine($"  Saturated Fat {rec.SaturatedFat} g");
		Console.WriteLine($"  Trans Fat     {rec.TransFat} g");
		Console.WriteLine($"Cholesterol {rec.Cholesterol} g");
	}

	private void NutritionVM_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (sender is null)
		{
            Console.WriteLine($"Nutrition_PC: ERROR: sender is null. PropertyName is {e.PropertyName}");
            return;
		}
        
		Console.WriteLine($"NutritionVM_PC: property name is {e.PropertyName}");
		if ((sender as Nutrition_VM)!.EditFoodItem is not null && e.PropertyName == "EditFoodItem")
		{
			GetUserInputForFoodItem();
		}
	}

	private void GetUserInputForFoodItem()
	{
        // TODO: It feels like there should be a better way to do this.
        // TODO: Why wouldn't we create a new item here ourselves?
        if (_nutritionVm.EditFoodItem is null)
	        return;
        
		string? userInput;

		Console.Write("Enter name: ");
		userInput = Console.ReadLine();
		_nutritionVm.EditFoodItem.Name = userInput;

		Console.Write("Enter brand: ");
		userInput = Console.ReadLine();
		_nutritionVm.EditFoodItem.Brand = userInput;

		// Simulate the clicking of the OK button.
		// TODO: Add a way for the user to Cancel this edit.
		_nutritionVm.EditFoodItem.OnOk?.Invoke(_nutritionVm.EditFoodItem);
	}

	public void PrintHelp()
	{
		Console.WriteLine();
		Console.WriteLine("Command List");
		Console.WriteLine("============");
		Console.WriteLine();
		Console.WriteLine("list");
		Console.WriteLine("add");
		Console.WriteLine("edit {id}");
		Console.WriteLine("del[ete] {id}");
		Console.WriteLine("quit");
	}

	public NutritionSample()
	{
		_nutritionVm.PropertyChanged += NutritionVM_PropertyChanged;
	}
}
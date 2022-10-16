using Nutrition.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSamples.Nutrition_Sample
{
	public class NutritionSample : DotNetSamples.IConsoleSample
	{
		private Nutrition_VM NutritionVM = new();
		private int NextId;

		public void Run()
		{
			Console.WriteLine("Welcome to the Nutrtion sample.");

			NextId = 1;
			foreach (var item in NutritionVM.FoodItems)
			{
				item.FoodRecord.Id = NextId++;
			}

			bool quit = false;
			do
			{
				int itemId;
				FoodItem_VM? item;
				Console.WriteLine($"There are {NutritionVM.FoodItems.Count} food items in the database.");
				Console.WriteLine();
				Console.WriteLine("Enter a command.");
				string userInput = Console.ReadLine();
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
						item = NutritionVM.FoodItems.Where(fi => fi.FoodRecord.Id == itemId).FirstOrDefault();
						if (item is null)
						{
							Console.WriteLine($"ERROR: No food item exists with ID {itemId}!");
							break;
						}
						else
						{
							NutritionVM.SelectedFoodItem = item;
							EditFoodItem_VM efidb = new("Edit Food Item", NutritionVM.SelectedFoodItem);
							NutritionVM.EditSelectedFoodItem(efidb);
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
						item = NutritionVM.FoodItems.Where(fi => fi.FoodRecord.Id == itemId).FirstOrDefault();
						if (item is null)
						{
							Console.WriteLine($"ERROR: No food item exists with ID {itemId}!");
							break;
						}
						else
						{
							NutritionVM.SelectedFoodItem = item;
							NutritionVM.DeleteSelectedFoodItem();
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
			NutritionVM.AddNewFoodItem(efi);
			// We have to fake the ID because we don't have real persistence.
			NutritionVM.SelectedFoodItem.FoodRecord.Id = NextId++;
		}

		public void ListFoodItems()
		{
			Console.WriteLine();
			Console.WriteLine("FoodItems database");
			Console.WriteLine("==================");
			Console.WriteLine(" ID   Name                 Brand");
			foreach (var item in NutritionVM.FoodItems)
			{
				Console.WriteLine($"[{item.FoodRecord.Id}] {item.FoodRecord.Name} - {item.FoodRecord.Brand}");
			}
			Console.WriteLine();
		}

		public void PrintFoodItem()
		{
			var rec = NutritionVM.SelectedFoodItem.FoodRecord;
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
			Console.WriteLine($"NutritionVM_PC: property name is {e.PropertyName}");
			if ((sender as Nutrition_VM).EditFoodItem is not null && e.PropertyName == "EditFoodItem")
			{
				GetUserInputForFoodItem();
			}
		}

		private void GetUserInputForFoodItem()
		{
			string? userInput;

			Console.Write("Enter name: ");
			userInput = Console.ReadLine();
			NutritionVM.EditFoodItem.Name = userInput;

			Console.Write("Enter brand: ");
			userInput = Console.ReadLine();
			NutritionVM.EditFoodItem.Brand = userInput;

			// Simulate the clicking of the OK button.
			// TODO: Add a way for the user to Cancel this edit.
			NutritionVM.EditFoodItem.OnOk?.Invoke(NutritionVM.EditFoodItem);
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
			NutritionVM.PropertyChanged += NutritionVM_PropertyChanged;
		}
	}
}

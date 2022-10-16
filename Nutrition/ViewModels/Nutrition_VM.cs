using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition.ViewModels
{
	public partial class Nutrition_VM : ObservableObject
	{
		public string Salutation { get; set; } = "Welcome to Nutrition_VM";

		[ObservableProperty]
		private ObservableCollection<FoodItem_VM>? foodItems = new();

		[ObservableProperty]
		private FoodItem_VM? selectedFoodItem;

		// Attach a listener to this property to handle user input.
		[ObservableProperty]
		private EditFoodItem_VM? editFoodItem;

		// This triggers the gathering of the user input, via whatever means.
		public void InvokeEditFoodItem(EditFoodItem_VM userInput, FoodItem_VM target)
		{
			// This method is used when the user indicates that their editing
			// is complete and the input should be accepted (e.g. by clicking
			// the OK button on a dialog box in a GUI app). It will validate
			// the user input but will not process it; that is left to the caller.
			userInput.OnOk = (_edit) =>
			{
				_edit.Result = true;
				if (_edit.Name is null || _edit.Name.Length == 0 ||
					_edit.Brand is null || _edit.Brand.Length == 0)
					_edit.Result = false;
			};

			// This statement is blocking.
			EditFoodItem = userInput;
			// This might be needed to prevent a dialog box from reappearing.
			//EditFoodItem = null;

			// Process the result.
			if (userInput.Result)
			{
				// Update the Model record.
				target.FoodRecord.Name = userInput.Name;
				target.FoodRecord.Brand = userInput.Brand;
				target.FoodRecord.ServingSize = userInput.ServingSize;
				target.FoodRecord.ServingUnit = userInput.ServingUnit;
				target.FoodRecord.TotalFat = userInput.TotalFat;
				target.FoodRecord.SaturatedFat = userInput.SaturatedFat;
				target.FoodRecord.TransFat = userInput.TransFat;
				target.FoodRecord.Cholesterol = userInput.Cholesterol;
				target.FoodRecord.Sodium = userInput.Sodium;
				target.FoodRecord.TotalCarbs = userInput.TotalCarbs;
				target.FoodRecord.DietaryFiber = userInput.DietaryFiber;
				target.FoodRecord.SolubleFiber = userInput.SolubleFiber;
				target.FoodRecord.InsolubleFiber = userInput.InsolubleFiber;
				target.FoodRecord.Sugar = userInput.Sugar;
				target.FoodRecord.Protein = userInput.Protein;
				// Persistence would be done here as well? Or by the caller because we still don't know add or edit here?
			}
		}

		public void AddNewFoodItem(EditFoodItem_VM efi)
		{
			FoodItem_VM temp = new FoodItem_VM("New Item", "New Brand");
			InvokeEditFoodItem(efi, temp);
			if (efi.Result)
			{
				// Update our collection.
				FoodItems.Add(temp);
				SelectedFoodItem = temp;
			}
		}

		public void EditSelectedFoodItem(EditFoodItem_VM efidb)
		{
			if (SelectedFoodItem is not null)
			{
				//EditFoodItem_VM efidb = new("Edit Food Item", SelectedFoodItem);
				InvokeEditFoodItem(efidb, SelectedFoodItem);

				if (efidb.Result)
				{
					// Update our collection.
					// TODO: Is there really no better way to do this?
					FoodItem_VM tempSel = SelectedFoodItem;
					ObservableCollection<FoodItem_VM> tempOC = FoodItems;
					FoodItems = null;
					FoodItems = tempOC;
					SelectedFoodItem = tempSel;
				}
			}
		}

		public void DeleteSelectedFoodItem()
		{
			if (SelectedFoodItem is not null)
			{
				int idx = FoodItems.IndexOf(SelectedFoodItem);
				FoodItems.Remove(SelectedFoodItem);
				if (FoodItems.Count == 0)
					SelectedFoodItem = null;
				else if (idx < FoodItems.Count)
					SelectedFoodItem = FoodItems[idx];
				else
					SelectedFoodItem = FoodItems.Last();
			}
		}

		public Nutrition_VM()
		{
			foodItems.Add(new FoodItem_VM("Item 1", "Brand 1"));
			foodItems.Add(new FoodItem_VM("Item 2", "Brand 2"));
			foodItems.Add(new FoodItem_VM("Item 3", "Brand 3"));
			foodItems.Add(new FoodItem_VM());
		}
	}

}

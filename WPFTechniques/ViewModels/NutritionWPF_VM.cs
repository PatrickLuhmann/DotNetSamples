using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nutrition.Models;
using Nutrition.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WPFTechniques.ViewModels
{
	// TODO: Once this class gets filled out, look for things to move to a base VM class.
	public partial class NutritionWPF_VM : ObservableObject
	{
		public string Salutation { get; set; } = "Welcome to NutritionWPF_VM";

		[ObservableProperty]
		private ObservableCollection<FoodItem_VM> foodItems = new();

		[ObservableProperty]
		private FoodItem_VM? selectedFoodItem;

		[ObservableProperty]
		private EditFoodItemDialogBox_VM? editFoodItemDialogBox;

		[RelayCommand]
		public void AddFoodItem()
		{
			FoodItem_VM temp = new FoodItem_VM("New Item", "New Brand");

			EditFoodItemDialogBox_VM efidb = InvokeEditFoodItem(temp);

			if (efidb.Result)
			{
				// Update the Model record.
				// Persistence would be done here as well.
				temp.FoodRecord.Name = efidb.Name;
				temp.FoodRecord.Brand = efidb.Brand;
				temp.FoodRecord.ServingSize = efidb.ServingSize;
				temp.FoodRecord.ServingUnit = efidb.ServingUnit;
				temp.FoodRecord.TotalFat = efidb.TotalFat;
				temp.FoodRecord.SaturatedFat = efidb.SaturatedFat;
				temp.FoodRecord.TransFat = efidb.TransFat;
				temp.FoodRecord.Cholesterol = efidb.Cholesterol;
				temp.FoodRecord.Sodium = efidb.Sodium;
				temp.FoodRecord.TotalCarbs = efidb.TotalCarbs;
				temp.FoodRecord.DietaryFiber = efidb.DietaryFiber;
				temp.FoodRecord.SolubleFiber = efidb.SolubleFiber;
				temp.FoodRecord.InsolubleFiber = efidb.InsolubleFiber;
				temp.FoodRecord.Sugar = efidb.Sugar;
				temp.FoodRecord.Protein = efidb.Protein;

				// Update our collection.
				foodItems.Add(temp);
				SelectedFoodItem = temp;
			}
		}

		[RelayCommand]
		public void EditFoodItem()
		{
			if (SelectedFoodItem is not null)
			{
				EditFoodItemDialogBox_VM efidb = InvokeEditFoodItem(SelectedFoodItem);

				if (efidb.Result)
				{
					// Update the Model record.
					// Persistence would be done here as well.
					SelectedFoodItem.FoodRecord.Name = efidb.Name;
					SelectedFoodItem.FoodRecord.Brand = efidb.Brand;
					SelectedFoodItem.FoodRecord.ServingSize = efidb.ServingSize;
					SelectedFoodItem.FoodRecord.ServingUnit = efidb.ServingUnit;
					SelectedFoodItem.FoodRecord.TotalFat = efidb.TotalFat;
					SelectedFoodItem.FoodRecord.SaturatedFat = efidb.SaturatedFat;
					SelectedFoodItem.FoodRecord.TransFat = efidb.TransFat;
					SelectedFoodItem.FoodRecord.Cholesterol = efidb.Cholesterol;
					SelectedFoodItem.FoodRecord.Sodium = efidb.Sodium;
					SelectedFoodItem.FoodRecord.TotalCarbs = efidb.TotalCarbs;
					SelectedFoodItem.FoodRecord.DietaryFiber = efidb.DietaryFiber;
					SelectedFoodItem.FoodRecord.SolubleFiber = efidb.SolubleFiber;
					SelectedFoodItem.FoodRecord.InsolubleFiber = efidb.InsolubleFiber;
					SelectedFoodItem.FoodRecord.Sugar = efidb.Sugar;
					SelectedFoodItem.FoodRecord.Protein = efidb.Protein;

					// Update our collection.
					// TODO: Is there really no better way to do this?
					FoodItem_VM tempSel = selectedFoodItem;
					ObservableCollection<FoodItem_VM> tempOC = FoodItems;
					FoodItems = null;
					FoodItems = tempOC;
					SelectedFoodItem = tempSel;
				}
			}
		}

		[RelayCommand]
		public void DeleteFoodItem()
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

		public EditFoodItemDialogBox_VM InvokeEditFoodItem(FoodItem_VM food)
		{
			EditFoodItemDialogBox_VM efidbvm = new EditFoodItemDialogBox_VM("Edit Food Item", food);

			// TODO: Add OnOK/OnCancel/OnCloseRequest actions.
			efidbvm.OnOk = (_dbvm) =>
			{
				// Assuming the user input is valid, tell the caller that
				// the dialog box is done being used.
				_dbvm.Result = true;

				if (_dbvm.Name is null || _dbvm.Name.Length == 0 ||
					_dbvm.Brand is null || _dbvm.Brand.Length == 0)
					_dbvm.Result = false;
			};

			// This statement is blocking.
			EditFoodItemDialogBox = efidbvm;

			// Set this to null so that it doesn't pop up again unexpectedly.
			EditFoodItemDialogBox = null;

			// TODO: Process the results or let the caller do it.
			return efidbvm;
		}

		public NutritionWPF_VM()
		{
			foodItems.Add(new FoodItem_VM("Item 1", "Brand 1"));
			foodItems.Add(new FoodItem_VM("Item 2", "Brand 2"));
			foodItems.Add(new FoodItem_VM("Item 3", "Brand 3"));
			foodItems.Add(new FoodItem_VM());
		}
	}
}

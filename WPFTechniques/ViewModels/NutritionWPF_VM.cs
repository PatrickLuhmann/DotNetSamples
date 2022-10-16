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
	public partial class NutritionWPF_VM : Nutrition_VM
	{
		public new string Salutation { get; set; } = "Welcome to NutritionWPF_VM";

		[RelayCommand]
		public void AddFoodItem()
		{
			EditFoodItemDialogBox_VM efidb = new("Add Food Item", new FoodItem_VM("New Item", "New Brand"));
			AddNewFoodItem(efidb);
		}

		[RelayCommand]
		public void EditFoodItem()
		{
			if (SelectedFoodItem is not null)
			{
				// Allocate our WPF-specific edit VM.
				EditFoodItemDialogBox_VM efidb = new("Edit Food Item", SelectedFoodItem);
				EditSelectedFoodItem(efidb);
			}
		}

		[RelayCommand]
		public void DeleteFoodItem()
		{
			DeleteSelectedFoodItem();
		}

		public NutritionWPF_VM()
		{
			// NOTE: We have to use the public name because we can't access
			// private members of a subclass.
			FoodItems.Add(new FoodItem_VM("WPF Item 1", "WPF Brand 1"));
			FoodItems.Add(new FoodItem_VM("WPF Item 2", "WPF Brand 2"));
			FoodItems.Add(new FoodItem_VM("WPF Item 3", "WPF Brand 3"));
			FoodItems.Add(new FoodItem_VM());
		}
	}
}

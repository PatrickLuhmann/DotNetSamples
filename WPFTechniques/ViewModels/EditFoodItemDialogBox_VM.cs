using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nutrition.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTechniques.ViewModels
{
	public partial class EditFoodItemDialogBox_VM : ObservableObject
	{
		public string Salutation { get; set; } = "Welcome to EditFoodItemDialogBox_VM";
		public string Title { get; set; }

		public bool Result = false;

		// User Input Fields
		public string? Name { get; set; }
		public string? Brand { get; set; }

		public decimal ServingSize { get; set; }
		public string? ServingUnit { get; set; }

		public decimal TotalFat { get; set; }
		public decimal SaturatedFat { get; set; }
		public decimal TransFat { get; set; }
		public decimal Cholesterol { get; set; }
		public decimal Sodium { get; set; }
		public decimal TotalCarbs { get; set; }
		public decimal DietaryFiber { get; set; }
		public decimal SolubleFiber { get; set; }
		public decimal InsolubleFiber { get; set; }
		public decimal Sugar { get; set; }
		public decimal Protein { get; set; }

		public Action<EditFoodItemDialogBox_VM> OnOk { get; set; }

		public event EventHandler DialogClosing;

		[RelayCommand]
		private void Ok(object parameter)
		{
			System.Diagnostics.Debug.WriteLine("Enter: EditFoodItemDialogBox - Ok");
			if (parameter is EditFoodItemDialogBox_VM efidbvm)
			{
				// Trigger the action that the creator registered.
				efidbvm.OnOk?.Invoke(efidbvm);
				// Only close the dialog if the user input is valid.
				if (efidbvm.Result)
					efidbvm.DialogClosing(efidbvm, new EventArgs());
			}
		}

		[RelayCommand]
		private void Cancel(object parameter)
		{
			System.Diagnostics.Debug.WriteLine("Enter: EditFoodItemDialogBox - Cancel");
			if (parameter is EditFoodItemDialogBox_VM efidbvm)
			{
				// Any input provided by the user is discarded.
				//sdbvm.Name = null;
				//sdbvm.Symbol = null;

				efidbvm.DialogClosing(efidbvm, new EventArgs());
			}
		}

		public EditFoodItemDialogBox_VM(string title, FoodItem_VM food)
		{
			Title = title;

			Name = food.FoodRecord.Name;
			Brand = food.FoodRecord.Brand;
			ServingSize = food.FoodRecord.ServingSize;
			ServingUnit = food.FoodRecord.ServingUnit;
			TotalFat = food.FoodRecord.TotalFat;
			SaturatedFat = food.FoodRecord.SaturatedFat;
			TransFat = food.FoodRecord.TransFat;
			Cholesterol = food.FoodRecord.Cholesterol;
			Sodium = food.FoodRecord.Sodium;
			TotalCarbs = food.FoodRecord.TotalCarbs;
			DietaryFiber = food.FoodRecord.DietaryFiber;
			SolubleFiber = food.FoodRecord.SolubleFiber;
			InsolubleFiber = food.FoodRecord.InsolubleFiber;
			Sugar = food.FoodRecord.Sugar;
			Protein = food.FoodRecord.Protein;
		}

		public EditFoodItemDialogBox_VM()
		{
			Title = "[DT] EditFoodItemDialogBox_VM";
		}
	}
}

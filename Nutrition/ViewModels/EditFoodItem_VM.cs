using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition.ViewModels
{
	public class EditFoodItem_VM : ObservableObject
	{
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

		// Interface
		public Action<EditFoodItem_VM>? OnOk { get; set; }
		public bool Result { get; set; }

		// TODO: title is not needed here; that can be in an inheriting class.
		public EditFoodItem_VM(string title, FoodItem_VM food)
		{
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

		public EditFoodItem_VM()
		{
			// Does anything need to go here?
		}
	}
}

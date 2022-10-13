using CommunityToolkit.Mvvm.ComponentModel;
using Nutrition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition.ViewModels
{
	public partial class FoodItem_VM : ObservableObject
	{
		[ObservableProperty]
		private FoodItem foodRecord;

		public FoodItem_VM(string name, string brand)
		{
			foodRecord = new()
			{
				Name = name,
				Brand = brand,
			};
		}
		public FoodItem_VM()
		{
			foodRecord = new FoodItem()
			{
				Id = -1,
				Name = "Soylent Chartreuse",
				Brand = "Happy People Food Co.",
			};
		}
	}
}

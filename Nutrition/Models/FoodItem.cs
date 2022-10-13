using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutrition.Models
{
	public class FoodItem
	{
		public int Id { get; set; }

		public string? Name { get; set; }
		public string? Brand { get; set; }

		public decimal ServingSize { get; set; } = 0;
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
	}
}

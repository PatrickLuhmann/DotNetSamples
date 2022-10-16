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
	public partial class EditFoodItemDialogBox_VM : EditFoodItem_VM
	{
		public string Salutation { get; set; } = "Welcome to EditFoodItemDialogBox_VM";
		public string Title { get; set; }

		public event EventHandler? DialogClosing;

		// Commands that are specific to the dialog box, and thus don't go in the base class.
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

		public EditFoodItemDialogBox_VM(string title, FoodItem_VM food) : base(title, food)
		{
			Title = title;
		}

		public EditFoodItemDialogBox_VM()
		{
			Title = "[DT] EditFoodItemDialogBox_VM";
		}
	}
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTechniques_ViewModels
{
	// All the core stuff goes in here.
	public partial class Base_VM : ObservableObject
	{
		public string Salutation { get; set; } = "Welcome to Base_VM";

		[ObservableProperty]
		private ObservableCollection<int> baseNumbers = new();

		[RelayCommand]
		private void AddNumber()
		{
			var rng = new Random(DateTime.Now.Millisecond);
			int num = rng.Next(-1000, 1000);
			baseNumbers.Add(num);
		}

		public Base_VM()
		{
			BaseNumbers.Add(20);
			BaseNumbers.Add(45);
			BaseNumbers.Add(-448);
		}
	}
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace WPFTechniques.ViewModels
{
	// TODO: These MVVM Toolkit attributes expose a bug somewhere in VS or the SDK.
	// The code generator creates two copies of the property, which causes a compile
	// error. There is a workaround that goes in the project file.
	// See WPFTechniques.csproj
	// When new versions of VS or the SDK are released, try removing the workaround.

	// These attributes are from MVVM Toolkit. Apparently OO is a superset of INPC.
	// The docs say to inherit instead of using the attribute if possible. Since
	// my VM classes rarely inherit some other class, this is probably what I should do.
	//[INotifyPropertyChanged]
	//[ObservableObject]
	public partial class TopLevel_VM : ObservableObject
	{
		public string Salutation { get; set; } = "Welcome to TopLevel_VM";

		#region Trigger Sample items
		[ObservableProperty]
		private List<string> comboList1;
		[ObservableProperty]
		private string selectedCombo1;
		[ObservableProperty]
		private string cDate;
		[ObservableProperty]
		private string cQuantity;
		[ObservableProperty]
		private string cAmount;
		#endregion

		public ListSortingWPF_VM ListSortingSample { get; set; }
		public NutritionWPF_VM NutritionSample { get; set; }
		public ListProcessingWPF_VM ListProcessingSample { get; set; }

		// This attribute only generates the source for the property. It depends
		// on the INotifyPropertyChanged infrastructure. Provide that either
		// with one of the attributes on the class, or implement the interface
		// explicitly.
		[ObservableProperty]
		private int counter;

		// "Old" implementation of Counter to show use of OnPropertyChanged().
		private int _oldCounter;
		public int OldCounter
		{
			get => _oldCounter;
			set
			{
				_oldCounter = value;
				OnPropertyChanged("OldCounter");
			}
		}

		[ObservableProperty]
		private SimpleDialogBox_VM? simpleDialogBox;

		[ObservableProperty]
		private List<int>? numbers = new();
		
		private ObservableCollection<int> _iEnumNumbers = new();
		public IList<int> IEnumNumbers => _iEnumNumbers;

		[ObservableProperty]
		private int selectedNumber;

		// This attribute generates the code for XxxCommand, which
		// is what the XAML will bind to.
		[RelayCommand]
		private void IncrementCounter()
		{
			Counter++;
			OldCounter++;
		}

		[RelayCommand]
		private void DecrementCounter()
		{
			// If we use the "backing" object then we must explicitly notify.
			counter--;
			OnPropertyChanged("Counter");
			_oldCounter--;
			OnPropertyChanged("OldCounter");
		}

		[RelayCommand]
		private void ModifyByTen(string dir)
		{
			if (dir == "INC")
			{
				Counter += 10;
				OldCounter += 10;
			}
			else if (dir == "DEC")
			{
				Counter -= 10;
				OldCounter -= 10;
			}
			else
			{
				Counter = 0;
				OldCounter = 0;
			}
		}

		[RelayCommand]
		private void ShowSimpleDialogBox()
		{
			SimpleDialogBox_VM sdbvm = new SimpleDialogBox_VM("Simple Dialog Box");
			// TODO: Add OnOK/OnCancel/OnCloseRequest actions.
			sdbvm.OnOk = (_dbvm) =>
			{
				// Get any input the user provided and store it in this VM object.

				// Assuming the user input is valid, tell the caller that
				// the dialog box is done being used.
				_dbvm.Result = true;
			};

			// This statement is blocking.
			SimpleDialogBox = sdbvm;

			// TODO: Process the results.
			bool result = sdbvm.Result;

			// Set this to null so that it doesn't pop up again unexpectedly.
			SimpleDialogBox = null;
		}

		[RelayCommand]
		private void AddNumberToList(object param)
		{
			// This is what you have to deal with when using
			// something other than ObservableCollection.

			numbers?.Add(Counter);
			// This doesn't work as expected; I assume because the collection
			// reference hasn't changed (i.e. the "pointer address" is the same
			// value regardless of what has happened inside the collection) and
			// there is a check for such a change.
			//OnPropertyChanged("Numbers");
			
			// We need to change the collection reference itself. This works
			// just the same as if we had changed an int that was declared
			// with the ObservableProperty attribute.
			var temp = Numbers;
			Numbers = null;
			Numbers = temp;

			// Try something closer to a sample I found.
			IEnumNumbers.Add(Counter);
			// No need for explicit call when using ObservableCollection,
			// even if the public property is not explicitly OC.
			//OnPropertyChanged("IEnumNumbers");
		}

		public TopLevel_VM()
		{
			ListSortingSample = new();
			NutritionSample = new();
			ListProcessingSample = new();

			ComboList1 = new List<string>
			{
				"Item A",
				"Item B",
				"Item C",
			};
			SelectedCombo1 = ComboList1[0];

			Numbers?.Add(1);
			Numbers?.Add(2);
			Numbers?.Add(3);
			_iEnumNumbers.Add(10);
			_iEnumNumbers.Add(20);
			_iEnumNumbers.Add(30);

		}
	}
}

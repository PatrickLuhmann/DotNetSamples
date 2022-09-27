using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		// This attribute only generates the source for the property. It depends
		// on the INotifyPropertyChanged infrastructure. Provide that either
		// with one of the attributes on the class, or implement the interface
		// explicitly.
		[ObservableProperty]
		private int counter;

		[ObservableProperty]
		private SimpleDialogBox_VM? simpleDialogBox;

		// This attribute generates the code for XxxCommand, which
		// is what the XAML will bind to.
		[RelayCommand]
		private void IncrementCounter() => Counter++;

		[RelayCommand]
		private void DecrementCounter() => Counter--;

		[RelayCommand]
		private void ModifyByTen(string dir)
		{
			if (dir == "INC")
				Counter += 10;
			else if (dir == "DEC")
				Counter -= 10;
			else
				Counter = 0;
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
	}
}

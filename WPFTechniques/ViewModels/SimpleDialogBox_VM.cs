using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFTechniques.ViewModels
{
	public partial class SimpleDialogBox_VM
	{
		public string Salutation { get; set; } = "Welcome to SimpleDialogBox_VM";
		public string Title { get; set; }

		public bool Result = false;

		public Action<SimpleDialogBox_VM> OnOk { get; set; }

		public event EventHandler DialogClosing;

		#region Commands
		// This is the non-MVVM Toolkit way.
		public ICommand OkCmd { get { return new OkCommand(); } }
		private class OkCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is SimpleDialogBox_VM sdbvm)
				{
					// Trigger the action that the creator registered.
					sdbvm.OnOk?.Invoke(sdbvm);
					sdbvm.DialogClosing(sdbvm, new EventArgs());
				}
			}
		}

		public ICommand CancelCmd { get { return new CancelCommand(); } }
		private class CancelCommand : ICommand
		{
			public bool CanExecute(object parameter)
			{
				return true;
			}

			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				if (parameter is SimpleDialogBox_VM sdbvm)
				{
					// Any input provided by the user is discarded.
					//sdbvm.Name = null;
					//sdbvm.Symbol = null;

					sdbvm.DialogClosing(sdbvm, new EventArgs());
				}
			}
		}

		// There is nothing special about the command in the dialog box itself,
		// so MVVM Toolkit can be used as expected.
		[RelayCommand]
		private void AltOk(object parameter)
		{
			if (parameter is SimpleDialogBox_VM sdbvm)
			{
				// Trigger the action that the creator registered.
				sdbvm.OnOk?.Invoke(sdbvm);
				sdbvm.DialogClosing(sdbvm, new EventArgs());
			}
		}

		[RelayCommand]
		private void AltCancel(object parameter)
		{
			if (parameter is SimpleDialogBox_VM sdbvm)
			{
				// Any input provided by the user is discarded.
				//sdbvm.Name = null;
				//sdbvm.Symbol = null;

				sdbvm.DialogClosing(sdbvm, new EventArgs());
			}
		}
		#endregion

		public SimpleDialogBox_VM(string title)
		{
			Title = title;
		}

		public SimpleDialogBox_VM()
		{
			Title = "Simple Dialog Box [DT]";
		}
	}
}

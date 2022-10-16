using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFTechniques.ViewModels;

namespace WPFTechniques.Views
{
	public static class DialogBehavior
	{
		#region BaseEditFoodItemDialogBox Behavior
		private static EditFoodItemDialogBox_View CurrentBaseEditFoodItemDialogBoxView;

		public static readonly DependencyProperty BaseEditFoodItemDialogBoxProperty =
			DependencyProperty.RegisterAttached(
				"EditFoodItem", // the name of the property in the base class
				typeof(object),
				typeof(DialogBehavior),
				new PropertyMetadata(null, OnBaseEditFoodItemDialogBoxChanged));

		// Required Get for an attached property; name = "Get" + property name.
		// The return type is known to be object because that is how we defined the property, above.
		public static object GetEditFoodItem(DependencyObject source)
		{
			return (object)source.GetValue(BaseEditFoodItemDialogBoxProperty);
		}

		// Required Set for an attached property; name = "Set" + property name.
		// The type of 'value' is known to be object because that is how we defined the property, above.
		public static void SetEditFoodItem(DependencyObject source, object value)
		{
			source.SetValue(BaseEditFoodItemDialogBoxProperty, value);
		}

		static void OnBaseEditFoodItemDialogBoxChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
			// e.NewValue must be the ???EditFoodItemDialogBox_VM object that was created in SimpleCommand.Execute().
			if (e.NewValue == null)
				return;

			// Look in the resource dictionary for an item with the key of type ???EditFoodItemDialogBox_VM.
			// This will return us a new object of type EditFoodItemDialogBox_View because that is what
			// we put in the <Application.Resources> section of App.xaml.
			var resource = Application.Current.TryFindResource(e.NewValue.GetType());
			if (resource is EditFoodItemDialogBox_View dlg)
			{
				// Give the dialog its DataContext (the corresponding VM).
				EditFoodItemDialogBox_VM sdbvm = (EditFoodItemDialogBox_VM)e.NewValue;
				dlg.DataContext = sdbvm;

				// Define the callback for when the VM signals that the dialog is closing.
				sdbvm.DialogClosing += (sender, args) =>
				{
					// POC
					CurrentBaseEditFoodItemDialogBoxView.Close();
				};

				// Callback for the Window is closing.
				dlg.Closing += (sender, args) =>
				{
					// stuff
					System.Diagnostics.Debug.WriteLine("Callback: EditFoodItemDialogBox_View.Closing");
				};
				// Callback for the Window is closed.
				dlg.Closed += (sender, args) =>
				{
					// stuff
					System.Diagnostics.Debug.WriteLine("Callback: EditFoodItemDialogBox_View.Closed");
				};

				// POC: Get the dialog to close without the collection handling from the sample.
				//      This feels like a hack so even if it works, look for something better.
				CurrentBaseEditFoodItemDialogBoxView = dlg;

				// This assumes all dialogs are modal. Otherwise we would invoke Show().
				dlg.Owner = App.Current.MainWindow;
				dlg.ShowDialog();
			}
		}
		#endregion

		#region SimpleDialogBox Behavior
		private static SimpleDialogBox_View CurrentSimpleDialogBoxView;

		public static readonly DependencyProperty SimpleDialogBoxProperty =
			DependencyProperty.RegisterAttached(
				"SimpleDialogBox",
				typeof(object),
				typeof(DialogBehavior),
				new PropertyMetadata(null, OnSimpleDialogBoxChanged));

		// Required Get for an attached property; name = "Get" + property name.
		// The return type is known to be object because that is how we defined the property, above.
		public static object GetSimpleDialogBox(DependencyObject source)
		{
			return (object)source.GetValue(SimpleDialogBoxProperty);
		}

		// Required Set for an attached property; name = "Set" + property name.
		// The type of 'value' is known to be object because that is how we defined the property, above.
		public static void SetSimpleDialogBox(DependencyObject source, object value)
		{
			source.SetValue(SimpleDialogBoxProperty, value);
		}

		static void OnSimpleDialogBoxChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
		{
			// e.NewValue must be the SimpleDialogBox_VM object that was created in SimpleCommand.Execute().
			if (e.NewValue == null)
				return;

			// Look in the resource dictionary for an item with the key of type SimpleDialogBox_VM.
			// This will return us a new object of type SimpleDialogBox_View because that is what
			// we put in the <Application.Resources> section of App.xaml.
			var resource = Application.Current.TryFindResource(e.NewValue.GetType());
			if (resource is SimpleDialogBox_View dlg)
			{
				// Give the dialog its DataContext (the corresponding VM).
				SimpleDialogBox_VM sdbvm = (SimpleDialogBox_VM)e.NewValue;
				dlg.DataContext = sdbvm;

				// Define the callback for when the VM signals that the dialog is closing.
				sdbvm.DialogClosing += (sender, args) =>
				{
					// POC
					CurrentSimpleDialogBoxView.Close();
				};

				// Callback for the Window is closing.
				dlg.Closing += (sender, args) =>
				{
					// stuff
					System.Diagnostics.Debug.WriteLine("Callback: SimpleDailogBox_View.Closing");
				};
				// Callback for the Window is closed.
				dlg.Closed += (sender, args) =>
				{
					// stuff
					System.Diagnostics.Debug.WriteLine("Callback: SimpleDailogBox_View.Closed");
				};

				// POC: Get the dialog to close without the collection handling from the sample.
				//      This feels like a hack so even if it works, look for something better.
				CurrentSimpleDialogBoxView = dlg;

				// This assumes all dialogs are modal. Otherwise we would invoke Show().
				dlg.Owner = App.Current.MainWindow;
				dlg.ShowDialog();
			}
		}
		#endregion
	}
}

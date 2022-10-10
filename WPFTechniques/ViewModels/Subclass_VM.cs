using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFTechniques_ViewModels;

namespace WPFTechniques.ViewModels
{
	// Create a subclass in order to more easily access WPF stuff like CollectionViewSource.
	public partial class Subclass_VM : Base_VM
	{
		// Override the base class declaration with the 'new' keyword.
		public new string Salutation { get; set; } = "Welcome to Subclass_VM";

		[ObservableProperty]
		private CollectionView baseNumbersCV;

		public Subclass_VM()
		{
			System.Diagnostics.Debug.WriteLine($"{Salutation}");

			// Create our own CollectionViewSource. The CollectionView can be bound in place of the underlying collection.
			// TODO: With cvs declared locally, how does the CV persist after the xtor exits?
			CollectionViewSource cvs = new();
			cvs.Source = BaseNumbers;
			baseNumbersCV = cvs.View as CollectionView;
			baseNumbersCV.SortDescriptions.Add(new SortDescription(null, ListSortDirection.Ascending));
		}
	}
}

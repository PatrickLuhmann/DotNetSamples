using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFTechniques_ViewModels;

namespace WPFTechniques.ViewModels
{
	public partial class ListSortingWPF_VM : ListSorting_VM
	{
		// Override the base class declaration with the 'new' keyword.
		public new string Salutation { get; set; } = "Welcome to ListSortingWPF_VM";

		[ObservableProperty]
		private ObservableCollection<int> myNumbers = new();

		[ObservableProperty]
		private CollectionView baseNumbersCV;

		[ObservableProperty]
		private CollectionView myCollectionView;

		private Dictionary<string, int> LedgerSortState;

		[RelayCommand]
		private void MyAddNumber()
		{
			var rng = new Random(DateTime.Now.Millisecond);
			int num = rng.Next(-10000, 10000);
			myNumbers.Add(num);
		}

		[RelayCommand]
		private void SortLedger(string field)
		{
			ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(LedgerItems);

			if (LedgerSortState[field] == 0)
			{
				// Not currently being sorted by this field, so sort
				// it ASC and mark the others as being not sorted currently.
				foreach (var entry in LedgerSortState.Keys.OfType<object>().ToArray())
					LedgerSortState[(string)entry] = 0;
				LedgerSortState[field] = 1;
				lcv.CustomSort = new LedgerItemSorter(field, true);
			}
			else if (LedgerSortState[field] == 1)
			{
				// Currently being sorted ASC by this field, so sort it DESC.
				LedgerSortState[field] = -1;
				lcv.CustomSort = new LedgerItemSorter(field, false);
			}
			else if (LedgerSortState[field] == -1)
			{
				// Currently being sorted DESC by this field, so
				// remove sorting altogether.
				LedgerSortState[field] = 0;
				lcv.CustomSort = null;
			}
			else
				throw new ArgumentException("The value for this key is invalid.");
		}

		public ListSortingWPF_VM() : base()
		{
			System.Diagnostics.Debug.WriteLine($"{Salutation}");

			myNumbers.Add(1);
			myNumbers.Add(10);
			myNumbers.Add(-1);
			myNumbers.Add(123);

			// Configuring various views for the BASE CLASS numbers.

			// Create our own CollectionViewSource. The CollectionView can be bound in place of the underlying collection.
			// TODO: With cvs declared locally, how does the CV persist after the xtor exits?
			CollectionViewSource cvs = new();
			cvs.Source = BaseNumbers;
			baseNumbersCV = cvs.View as CollectionView;
			baseNumbersCV?.SortDescriptions.Add(new SortDescription(null, ListSortDirection.Descending));

			// Configuring various views for the SUBCLASS numbers.

			//ListCollectionView lcv = new();
			// Configure the default view for myNumbers to automatically sort ASC.
			var numDefView = (CollectionView)CollectionViewSource.GetDefaultView(myNumbers);
			numDefView.SortDescriptions.Add(new SortDescription(null, ListSortDirection.Ascending));

			// Create our own CollectionViewSource. The CollectionView can be bound in place of the underlying collection.
			CollectionViewSource cvs2 = new();
			cvs2.Source = myNumbers;
			myCollectionView = cvs2.View as CollectionView;
			myCollectionView.SortDescriptions.Add(new SortDescription(null, ListSortDirection.Descending));

			// Now for the LedgerItem collection. This is a more involved sorting sample.
			LedgerSortState = new Dictionary<string, int>();
			LedgerSortState.Add("Id", 0);
			LedgerSortState.Add("Name", 0);
			LedgerSortState.Add("Date", 0);
			LedgerSortState.Add("Value", 0);
			LedgerSortState.Add("Quantity", 0);
			SortLedger("Id");
		}
	}
}

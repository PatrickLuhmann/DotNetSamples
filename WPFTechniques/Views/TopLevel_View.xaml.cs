using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTechniques.Views
{
	/// <summary>
	/// Interaction logic for TopLevel_View.xaml
	/// </summary>
	public partial class TopLevel_View : UserControl
	{
		public TopLevel_View()
		{
			InitializeComponent();

			// Set the ListView to display the entries sorted DESC.
			// NOTE: This does not show up in the designer; I don't know why.
			//SecondListView.Items.SortDescriptions
			//	.Add(new System.ComponentModel.SortDescription(null, System.ComponentModel.ListSortDirection.Descending));

			// Set the CVS to display the entries sorted DESC.
			// NOTE: The online example I am using as a reference uses "listingDataView" directly. This does not
			// work for me; I have to look it up in the Resources dictionary.
			CollectionViewSource? xxx = Resources["listingDataView"] as CollectionViewSource;
			// Alternate way to get the CVS Resource. I do not know if there is a real difference between these methods.
			//CollectionViewSource? xxx = TryFindResource("listingDataView") as CollectionViewSource;
			xxx?.SortDescriptions
				.Add(new System.ComponentModel.SortDescription(null, System.ComponentModel.ListSortDirection.Descending));
		}
	}
}

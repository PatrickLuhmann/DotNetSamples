using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFTechniques.ViewModels
{
	public partial class ListProcessingWPF_VM : ObservableObject
	{
		[ObservableProperty]
		[NotifyPropertyChangedFor(nameof(TotalValue))]
		private ObservableCollection<ListProcItem> items;

		public decimal TotalValue
		{
			get => items.Sum(i => i.TheValue);
			set { }
		}

		public void PendingChange(ListProcItem changingItem, decimal newValue)
		{
			// Validate the "new" value.
			// Negative numbers are never allowed.
			if (newValue < 0)
			{
				changingItem.RestorePriorValue();
			}
			else
			{
				if (items.Contains(changingItem))
				{
					// It changed so recalculate the sum.
					decimal newSum = items.Sum(i => i.TheValue);

					// If the sum is > 10000 then don't allow the change.
					if (newSum > 10000)
					{
						changingItem.RestorePriorValue();
						// The value didn't actually change, so no need to notify.
					}
					else
						OnPropertyChanged("TotalValue");
				}
				else
				{
					// Value will be verified with the LPItem is added to the OC.
				}

			}
		}

		[RelayCommand]
		private void AddItem()
		{
			// Add a new LPItem to the OC. Give it a value that makes the sum 10000.
			items.Add(new ListProcItem(this)
			{
				Id = Guid.NewGuid(),
				Date = DateTime.Now,
				TheValue = 99999, // relies on ItemsChangedCallback to reduce to legal value
			});
		}

		private void ItemsChangedCallback(object? sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				// NOTE: This assumes that only one item is added at a time.

				// We can't allow an item to be added that would push the sum above 10000.
				// In that case, reduce the value of the item.
				if (TotalValue > 10000)
				{
					decimal delta = TotalValue - 10000;
					(e.NewItems[0] as ListProcItem).TheValue -= delta;
				}
			}
			OnPropertyChanged("TotalValue");
		}

		public ListProcessingWPF_VM()
		{
			// Create our items collection.
			items = new();
			items.CollectionChanged += ItemsChangedCallback;

			items.Add(new ListProcItem(this)
			{
				Id = Guid.NewGuid(),
				Date = new DateTime(2001, 01, 01),
				TheValue = 123.45m,
			});
			items.Add(new ListProcItem(this)
			{
				Id = Guid.NewGuid(),
				Date = new DateTime(2002, 01, 01),
				TheValue = 2.56m,
			});
			items.Add(new ListProcItem(this)
			{
				Id = Guid.NewGuid(),
				Date = new DateTime(2003, 01, 01),
				TheValue = 4105.115m,
			});
		}
	}

	public partial class ListProcItem : ObservableObject
	{
		public DateTime Date { get; set; }
		public Guid Id { get; set; }

		[ObservableProperty]
		private decimal theValue;

		private decimal? temp;

		private ListProcessingWPF_VM Parent;

		partial void OnTheValueChanging(decimal value)
		{
			// Store the "before" value in case the "new" value is invalid.
			temp = theValue;
		}
		partial void OnTheValueChanged(decimal value)
		{
			Parent.PendingChange(this, value);
		}

		public void RestorePriorValue()
		{
			// This is only meant to be used when TheValue is changed
			// but the parent needs to cancel the change.
			if (temp is not null)
			{
				theValue = (decimal)temp;
				temp = null;
			}
		}

		public ListProcItem(ListProcessingWPF_VM parent)
		{
			// This seems like a kluge, but I haven't learned a better
			// way to do this type of thing.
			Parent = parent;
		}
	}
}

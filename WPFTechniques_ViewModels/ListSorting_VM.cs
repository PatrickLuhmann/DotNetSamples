using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTechniques_ViewModels
{
	public partial class ListSorting_VM : ObservableObject
	{
		public string Salutation { get; set; } = "Welcome to ListSorting_VM";

		[ObservableProperty]
		private ObservableCollection<int> baseNumbers = new();

		[ObservableProperty]
		private ObservableCollection<LedgerItem> ledgerItems = new();

		[RelayCommand]
		private void AddNumber()
		{
			var rng = new Random(DateTime.Now.Millisecond);
			int num = rng.Next(-1000, 1000);
			baseNumbers.Add(num);
		}

		[RelayCommand]
		private void AddLedgerItem()
		{
			Random rng = new Random(DateTime.Now.Millisecond);
			LedgerItems.Add(new LedgerItem
			{
				Id = rng.Next(),
				Name = Guid.NewGuid().ToString(),
				Date = DateTime.Now - TimeSpan.FromDays(rng.Next(0, 1000)),
				Value = rng.Next(1, 10000) * (decimal)rng.NextDouble(),
				Quantity = rng.Next(1, 100),
			});
		}

		public ListSorting_VM()
		{
			System.Diagnostics.Debug.WriteLine($"{Salutation}");

			BaseNumbers.Add(20);
			BaseNumbers.Add(45);
			BaseNumbers.Add(-448);

			// Create 100 random ledger items for the collection.
			Random rng = new Random(DateTime.Now.Millisecond);
			for (int i = 0; i < 100; i++)
			{
				LedgerItems.Add(new LedgerItem
				{
					Id = i,
					Name = Guid.NewGuid().ToString(),
					Date = DateTime.Now - TimeSpan.FromDays(rng.Next(0, 1000)),
					Value = rng.Next(1, 10000) * (decimal)rng.NextDouble(),
					Quantity = rng.Next(1, 100),
				});
			}
		}
	}
	public class LedgerItem
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public DateTime Date { get; set; }
		public decimal Value { get; set; }
		public int Quantity { get; set; }
	}

	public class LedgerItemSorter : IComparer
	{
		private readonly string Field;
		private readonly bool Asc;

		public int Compare(object? x, object? y)
		{
			if (x is LedgerItem xx && y is LedgerItem yy)
			{
				switch (Field)
				{
					case "Id":
						if (xx.Id > yy.Id)
							return Asc ? 1 : -1;
						else if (xx.Id < yy.Id)
							return Asc ? -1 : 1;
						else
							return 0;
					case "Name":
						if (String.Compare(xx.Name, yy.Name) > 0)
							return Asc ? 1 : -1;
						else if (String.Compare(xx.Name, yy.Name) < 0)
							return Asc ? -1 : 1;
						else
							return 0;
					case "Date":
						if (xx.Date > yy.Date)
							return Asc ? 1 : -1;
						else if (xx.Date < yy.Date)
							return Asc ? -1 : 1;
						else
							return 0;
					case "Value":
						if (xx.Value > yy.Value)
							return Asc ? 1 : -1;
						else if (xx.Value < yy.Value)
							return Asc ? -1 : 1;
						else
							return 0;
					case "Quantity":
						if (xx.Quantity > yy.Quantity)
							return Asc ? 1 : -1;
						else if (xx.Quantity < yy.Quantity)
							return Asc ? -1 : 1;
						else
							return 0;
					default:
						throw new ArgumentException("Field not recognized.");
				}
			}

			throw new ArgumentException("This Compare requires two LedgerItem objects.");
		}

		public LedgerItemSorter(string field, bool asc)
		{
			Field = field;
			Asc = asc;
		}

		public LedgerItemSorter()
		{
			Field = "Id";
			Asc = true;
		}
	}
}

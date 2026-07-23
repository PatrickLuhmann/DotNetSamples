using TaxTools;

namespace ConsoleCC;

public class TaxHelperSample : IConsoleSample
{
	public void Run()
	{
		Console.WriteLine("This is the Tax Helper sample.");

		Console.WriteLine("Gathering inputs for the ESTIMATED qual div cap gain worksheet");
		Console.WriteLine();

		// Ask the user for the different types of income.
		Console.WriteLine("Enter the value for QUALIFIED dividends: ");
		decimal qualDiv = Convert.ToDecimal(Console.ReadLine());

		Console.WriteLine("Enter the value for LONG term capital gains: ");
		decimal longCapGains = Convert.ToDecimal(Console.ReadLine());

		Console.WriteLine("Enter the value for ORDINARY dividends: ");
		decimal ordDiv = Convert.ToDecimal(Console.ReadLine());

		Console.WriteLine("Enter the value for SHORT term capital gains: ");
		decimal shortCapGains = Convert.ToDecimal(Console.ReadLine());

		Console.WriteLine("Enter the value for other ordinary income: ");
		decimal otherOrdIncome = Convert.ToDecimal(Console.ReadLine());

		int taxPeriod = 0;
		bool validInput = false;
		while (!validInput)
		{
			Console.WriteLine("Enter the estimated tax period [1 - 4]: ");
			taxPeriod = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("You entered {0}", taxPeriod);
			if (taxPeriod >= 1 && taxPeriod <= 4)
				validInput = true;
		}

		// Now we figure out the Annualized Estimated Tax Worksheet through line 11.

		// For Line 1, assume that Lines 33 - 43 are 0 (i.e. no self-employment income).
		// NOTE: We are rounding before annualizing because we don't put decimal values on a worksheet.
		int aetwLine1 = TaxCalculations.Round(qualDiv + longCapGains + ordDiv + shortCapGains + otherOrdIncome);

		decimal annualizationFactor;
		switch (taxPeriod)
		{
			case 1:
				annualizationFactor = 4;
				break;
			case 2:
				annualizationFactor = 2.4m;
				break;
			case 3:
				annualizationFactor = 1.5m;
				break;
			case 4:
				annualizationFactor = 1;
				break;
			default:
				// This should never happen so use an invalid value;
				annualizationFactor = -1;
				break;
		}

		int aetwLine3 = TaxCalculations.Round(aetwLine1 * annualizationFactor);

		// TODO: Assume standard deduction for single filer; add support for other options.
		int aetwLine11 = aetwLine3 - 16100;

		int line1 = aetwLine11;
		int line2 = TaxCalculations.Round(qualDiv * annualizationFactor);
		// TODO: Assumes long-term gain, and long-term gain more than compensates for short-term loss.
		int line3 = (int)Math.Min(longCapGains * annualizationFactor,
			(shortCapGains + longCapGains) * annualizationFactor);

		// Use these values to get the Estimated Qualified Dividend and Capital Gains worksheet.
		Dictionary<string, int> estQualDivSheet = TaxForms.EstimatedQualDivCapGainWorksheet(line1, line2, line3);

		// Print Worksheet 2-9
		Console.WriteLine();
		Console.WriteLine("Annualized Estimated Tax Worksheet 2-9 (2026)");
		Console.WriteLine("=============================================================");
		Console.WriteLine();
		Console.WriteLine("Line {0, -3}: {1, 10}", 1, aetwLine1);
		Console.WriteLine("Line {0, -3}: {1, 10}", 2, annualizationFactor);
		Console.WriteLine("Line {0, -3}: {1, 10}", 3, aetwLine3);
		Console.WriteLine("Line {0, -3}: {1, 10}", 4, 0);
		Console.WriteLine("Line {0, -3}: {1, 10}", 5, annualizationFactor);
		Console.WriteLine("Line {0, -3}: {1, 10}", 6, 0);
		Console.WriteLine("Line {0, -3}: {1, 10}", 7, 16100);
		Console.WriteLine("Line {0, -3}: {1, 10}", 8, 16100);
		Console.WriteLine("Line {0, -3}: {1, 10}", "9a", 0);
		Console.WriteLine("Line {0, -3}: {1, 10}", "9b", 0);
		Console.WriteLine("Line {0, -3}: {1, 10}", 10, 16100);
		Console.WriteLine("Line {0, -3}: {1, 10}", 11, aetwLine11);
		Console.WriteLine("Line {0, -3}: {1, 10}", 12, estQualDivSheet["40"]);

		// Print the worksheet
		Console.WriteLine();
		Console.WriteLine("ESTIMATED Qualified Dividends and Capital Gains Tax Worksheet");
		Console.WriteLine("=============================================================");
		Console.WriteLine();
		foreach (var line in estQualDivSheet)
		{
			Console.WriteLine("Line {0, -3}: {1, 10}", line.Key, line.Value);
		}
	}
}
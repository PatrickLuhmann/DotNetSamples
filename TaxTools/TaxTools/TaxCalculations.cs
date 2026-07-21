namespace TaxTools;

public class TaxCalculations
{
	/// <summary>
	/// Calculates the tax on the given income, using the estimated tax formula.
	/// </summary>
	/// <returns>
	/// The amount of the tax.
	/// </returns>
	public static int CalculateEstimatedTax(int income)
	{
		decimal tax = income switch
		{
			<= 12400 => income * 0.10m,
			<= 50400 => 1240 + (income - 12400) * 0.12m,
			<= 105700 => 5800 + (income - 50400) * 0.22m,
			<= 201775 => 17966 + (income - 105700) * 0.24m,
			<= 256225 => 41024 + (income - 201775) * 0.32m,
			< 640600 => 58448 + (income - 256225) * 0.35m,
			_ => 192979.25m + (income - 640600) * 0.37m
		};

		return Round(tax);
	}

	public static int Round(decimal value)
	{
		// The tax code rounds .5 up, whereas the default rounding behavior
		// for Math.Round is to round to the even number. Thus, we need to
		// explicitly tell Round() to always round up.
		return (int)Math.Round(value, MidpointRounding.AwayFromZero);
	}
}

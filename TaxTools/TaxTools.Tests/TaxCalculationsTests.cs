namespace TaxTools.Tests;

public class TaxCalculationTests
{
	[Theory]
	[InlineData(0, 0)]
	[InlineData(12400, 1240)]
	[InlineData(12401, 1240)]
	[InlineData(50400, 5800)]
	[InlineData(50401, 5800)]
	[InlineData(105700, 17966)]
	[InlineData(105701, 17966)]
	[InlineData(201775, 41024)]
	[InlineData(201776, 41024)]
	[InlineData(256225, 58448)]
	[InlineData(256226, 58448)]
	[InlineData(640600, 192979)]
	[InlineData(640601, 192980)]
	[InlineData(999999, 325957)]
	public void CalculateEstimatedTax_BracketBoundaries(int input, int expValue)
	{
		Assert.Equal(expValue, TaxCalculations.CalculateEstimatedTax(input));
	}

	[Theory]
	[InlineData(5, 1)]            // 5 * 0.10 = 0.50
	[InlineData(12404, 1240)]     // 4 * 0.12 = 0.48
	[InlineData(12405, 1241)]     // 5 * 0.12 = 0.60
	[InlineData(50425, 5806)]    // 25 * 0.22 = 5.50
	[InlineData(256235, 58452)]  // 10 * 0.35 = 3.50
	[InlineData(640625, 192989)] // 25 * 0.37 = 9.25 + 0.25 = 9.50
	public void CalculateEstimatedTax_RoundingUp(int input, int expValue)
	{
		Assert.Equal(expValue, TaxCalculations.CalculateEstimatedTax(input));
	}
}

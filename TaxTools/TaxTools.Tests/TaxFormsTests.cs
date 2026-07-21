namespace TaxTools.Tests;

public class TaxFormsTests
{
	[Fact]
	public void EstimatedQDCG_Basic_15pc_1()
	{
		// ASSEMBLE

		// ACT
		Dictionary<string, int> actLines = TaxForms.EstimatedQualDivCapGainWorksheet(81610, 22500, 70000);

		// ASSERT
		// This worksheet has 42 lines (40 + 2 extra line 13s)
		Assert.Equal(42, actLines.Count);

		Assert.Equal(81610, actLines["1"]);
		Assert.Equal(22500, actLines["2"]);
		Assert.Equal(70000, actLines["3"]);
		Assert.Equal(92500, actLines["4"]);
		Assert.Equal(0, actLines["5"]);
		Assert.Equal(0, actLines["6"]);
		Assert.Equal(0, actLines["7"]);
		Assert.Equal(0, actLines["8"]);
		Assert.Equal(92500, actLines["9"]);
		Assert.Equal(0, actLines["10"]);
		Assert.Equal(49450, actLines["11"]);
		Assert.Equal(0, actLines["12"]);
		Assert.Equal(0, actLines["13a"]);
		Assert.Equal(81610, actLines["13b"]);
		Assert.Equal(0, actLines["13c"]);
		Assert.Equal(0, actLines["14"]);
		Assert.Equal(49450, actLines["15"]);
		Assert.Equal(81610, actLines["16"]);
		Assert.Equal(49450, actLines["17"]);
		Assert.Equal(32160, actLines["18"]);
		Assert.Equal(545500, actLines["19"]);
		Assert.Equal(81610, actLines["20"]);
		Assert.Equal(49450, actLines["21"]);
		Assert.Equal(32160, actLines["22"]);
		Assert.Equal(32160, actLines["23"]);
		Assert.Equal(4824, actLines["24"]);
		Assert.Equal(81610, actLines["25"]);
		Assert.Equal(-1, actLines["26"]);
		Assert.Equal(-1, actLines["27"]);
		Assert.Equal(-1, actLines["28"]);
		Assert.Equal(-1, actLines["29"]);
		Assert.Equal(-1, actLines["30"]);
		Assert.Equal(-1, actLines["31"]);
		Assert.Equal(-1, actLines["32"]);
		Assert.Equal(-1, actLines["33"]);
		Assert.Equal(-1, actLines["34"]);
		Assert.Equal(-1, actLines["35"]);
		Assert.Equal(-1, actLines["36"]);
		Assert.Equal(0, actLines["37"]);
		Assert.Equal(4824, actLines["38"]);
		Assert.Equal(12666, actLines["39"]);
		Assert.Equal(4824, actLines["40"]);
	}

	[Fact]
	public void EstimatedQDCG_Basic_15pc_2()
	{
		// ASSEMBLE
		int line1 = 78460;
		int line2 = 21555;
		int line3 = 68520;

		// ACT
		Dictionary<string, int> actLines = TaxForms.EstimatedQualDivCapGainWorksheet(line1, line2, line3);

		// ASSERT
		// This worksheet has 42 lines (40 + 2 extra line 13s)
		Assert.Equal(42, actLines.Count);

		Assert.Equal(line1, actLines["1"]);
		Assert.Equal(line2, actLines["2"]);
		Assert.Equal(line3, actLines["3"]);
		Assert.Equal(90075, actLines["4"]);
		Assert.Equal(0, actLines["5"]);
		Assert.Equal(0, actLines["6"]);
		Assert.Equal(0, actLines["7"]);
		Assert.Equal(0, actLines["8"]);
		Assert.Equal(90075, actLines["9"]);
		Assert.Equal(0, actLines["10"]);
		Assert.Equal(49450, actLines["11"]);
		Assert.Equal(0, actLines["12"]);
		Assert.Equal(0, actLines["13a"]);
		Assert.Equal(line1, actLines["13b"]);
		Assert.Equal(0, actLines["13c"]);
		Assert.Equal(0, actLines["14"]);
		Assert.Equal(49450, actLines["15"]);
		Assert.Equal(line1, actLines["16"]);
		Assert.Equal(49450, actLines["17"]);
		Assert.Equal(29010, actLines["18"]);
		Assert.Equal(545500, actLines["19"]);
		Assert.Equal(line1, actLines["20"]);
		Assert.Equal(49450, actLines["21"]);
		Assert.Equal(29010, actLines["22"]);
		Assert.Equal(29010, actLines["23"]);
		Assert.Equal(4352, actLines["24"]);
		Assert.Equal(78460, actLines["25"]);
		Assert.Equal(-1, actLines["26"]);
		Assert.Equal(-1, actLines["27"]);
		Assert.Equal(-1, actLines["28"]);
		Assert.Equal(-1, actLines["29"]);
		Assert.Equal(-1, actLines["30"]);
		Assert.Equal(-1, actLines["31"]);
		Assert.Equal(-1, actLines["32"]);
		Assert.Equal(-1, actLines["33"]);
		Assert.Equal(-1, actLines["34"]);
		Assert.Equal(-1, actLines["35"]);
		Assert.Equal(-1, actLines["36"]);
		Assert.Equal(0, actLines["37"]);
		Assert.Equal(4352, actLines["38"]);
		Assert.Equal(11973, actLines["39"]);
		Assert.Equal(4352, actLines["40"]);
	}

	[Fact]
	public void EstimatedQDCG_Basic_15pc_3()
	{
		// ASSEMBLE
		// TODO: In the future, lines 1 - 3 will come from other forms. Simulate that here.
		int adjustedGrossIncome = 36220;
		decimal annualizationFactor = 2.4m; // Second period, because why not?
		int taxableIncome = TaxCalculations.Round(adjustedGrossIncome * annualizationFactor) - 16100;
		int line1 = taxableIncome;

		decimal qualifiedDividends = 8190.37m;
		int line2 = TaxCalculations.Round(qualifiedDividends * annualizationFactor);

		int netShortTermCapitalGain = 0;
		int netLongTermCapitalGain = 62467;
		int line3 = Math.Min(netLongTermCapitalGain, netShortTermCapitalGain + netLongTermCapitalGain);

		// ACT
		Dictionary<string, int> actLines = TaxForms.EstimatedQualDivCapGainWorksheet(line1, line2, line3);

		// ASSERT
		// This worksheet has 42 lines (40 + 2 extra line 13s)
		Assert.Equal(42, actLines.Count);

		Assert.Equal(line1, actLines["1"]);
		Assert.Equal(line2, actLines["2"]);
		Assert.Equal(line3, actLines["3"]);
		Assert.Equal(82124, actLines["4"]);
		Assert.Equal(0, actLines["5"]);
		Assert.Equal(0, actLines["6"]);
		Assert.Equal(0, actLines["7"]);
		Assert.Equal(0, actLines["8"]);
		Assert.Equal(82124, actLines["9"]);
		Assert.Equal(0, actLines["10"]);
		Assert.Equal(49450, actLines["11"]);
		Assert.Equal(0, actLines["12"]);
		Assert.Equal(0, actLines["13a"]);
		Assert.Equal(line1, actLines["13b"]);
		Assert.Equal(0, actLines["13c"]);
		Assert.Equal(0, actLines["14"]);
		Assert.Equal(49450, actLines["15"]);
		Assert.Equal(line1, actLines["16"]);
		Assert.Equal(49450, actLines["17"]);
		Assert.Equal(21378, actLines["18"]);
		Assert.Equal(545500, actLines["19"]);
		Assert.Equal(line1, actLines["20"]);
		Assert.Equal(49450, actLines["21"]);
		Assert.Equal(21378, actLines["22"]);
		Assert.Equal(21378, actLines["23"]);
		Assert.Equal(3207, actLines["24"]);
		Assert.Equal(line1, actLines["25"]);
		Assert.Equal(-1, actLines["26"]);
		Assert.Equal(-1, actLines["27"]);
		Assert.Equal(-1, actLines["28"]);
		Assert.Equal(-1, actLines["29"]);
		Assert.Equal(-1, actLines["30"]);
		Assert.Equal(-1, actLines["31"]);
		Assert.Equal(-1, actLines["32"]);
		Assert.Equal(-1, actLines["33"]);
		Assert.Equal(-1, actLines["34"]);
		Assert.Equal(-1, actLines["35"]);
		Assert.Equal(-1, actLines["36"]);
		Assert.Equal(0, actLines["37"]);
		Assert.Equal(3207, actLines["38"]);
		Assert.Equal(10294, actLines["39"]);
		Assert.Equal(3207, actLines["40"]);
	}

	[Fact]
	public void EstimatedQDCG_Skip_15pc()
	{
		// ASSEMBLE
		// TODO: In the future, lines 1 - 3 will come from other forms. Simulate that here.

		// In order to trigger "Skip line 15", we need to have no income taxed at the 0% rate.
		// The only way to do this is for ordinary income to "fill up" the 0% bracket.
		// So we have a small amount of qualified dividends, and more than 16100 + 49450 = 65550 ordinary income (single filer numbers).
		decimal qualifiedDividends = 1000; // taxes: 150
		decimal longTermCapitalGains = 0;
		decimal ordinaryDividends = 0;
		decimal shortTermCapitalGains = 0;
		decimal otherOrdinaryIncome = 70000; // taxes: 6570

		// This is (2026) Worksheet 2-9 "Annualized Estimated Tax Worksheet".
		// Line 1. The "raw" amounts keep decimal values, but each line on the worksheet is rounded as per IRS rules.
		int adjustedGrossIncome = TaxCalculations.Round(qualifiedDividends + longTermCapitalGains + ordinaryDividends + shortTermCapitalGains + otherOrdinaryIncome);

		// Line 2
		decimal annualizationFactor = 1; // 1st period = 4, 2nd period = 2.4, 3rd period = 1.5, 4th period = 1

		// Line 3
		int annualizedIncome = TaxCalculations.Round(adjustedGrossIncome * annualizationFactor);

		// Line 8.
		// TODO: Assume standard deduction for single filer.
		// TODO: Assume always 0 for lines 9a and 9b.
		int deduction = 16100;

		// Line 11. Taxable Income.
		int taxableIncome = annualizedIncome - deduction;

		// For Line 12, assume there is always qualified income, thus triggering use of the EQDCG Worksheet.
		// The term "net capital gain" means the amount by which the long-term captial gain is more than the net short-term capital loss.
		// TODO: Assume this is always the case. That is, even if ST is a loss, LT is a gain large enough to make their combination a gain.
		// TODO: Otherwise, lines 16 - 22 of Schedule D need to be taken into account. Also see line 3 of the 1040 QDCGT worksheet (i.e. the year-end version, not the estimated tax version).
		// What we want is the smaller of LT cap gain and (ST cap gain + LT cap gain).
		int netCapitalGain = Math.Min(TaxCalculations.Round(longTermCapitalGains * annualizationFactor), TaxCalculations.Round((shortTermCapitalGains + longTermCapitalGains) * annualizationFactor));

		// ACT
		int line1 = taxableIncome;
		int line2 = TaxCalculations.Round(qualifiedDividends * annualizationFactor);
		int line3 = netCapitalGain;
		Dictionary<string, int> actLines = TaxForms.EstimatedQualDivCapGainWorksheet(line1, line2, line3);

		// ASSERT
		// This worksheet has 42 lines (40 + 2 extra line 13s)
		Assert.Equal(42, actLines.Count);

		Assert.Equal(line1, actLines["1"]);
		Assert.Equal(line2, actLines["2"]);
		Assert.Equal(line3, actLines["3"]);
		Assert.Equal(1000, actLines["4"]);
		Assert.Equal(0, actLines["5"]);
		Assert.Equal(0, actLines["6"]);
		Assert.Equal(0, actLines["7"]);
		Assert.Equal(0, actLines["8"]);
		Assert.Equal(1000, actLines["9"]);
		Assert.Equal(53900, actLines["10"]);
		Assert.Equal(49450, actLines["11"]);
		Assert.Equal(49450, actLines["12"]);
		Assert.Equal(53900, actLines["13a"]);
		Assert.Equal(line1, actLines["13b"]);
		Assert.Equal(53900, actLines["13c"]);
		Assert.Equal(53900, actLines["14"]);
		Assert.Equal(-1, actLines["15"]);
		Assert.Equal(1000, actLines["16"]);
		Assert.Equal(0, actLines["17"]);
		Assert.Equal(1000, actLines["18"]);
		Assert.Equal(545500, actLines["19"]);
		Assert.Equal(54900, actLines["20"]);
		Assert.Equal(53900, actLines["21"]);
		Assert.Equal(1000, actLines["22"]);
		Assert.Equal(1000, actLines["23"]);
		Assert.Equal(150, actLines["24"]);
		Assert.Equal(1000, actLines["25"]);
		Assert.Equal(-1, actLines["26"]);
		Assert.Equal(-1, actLines["27"]);
		Assert.Equal(-1, actLines["28"]);
		Assert.Equal(-1, actLines["29"]);
		Assert.Equal(-1, actLines["30"]);
		Assert.Equal(-1, actLines["31"]);
		Assert.Equal(-1, actLines["32"]);
		Assert.Equal(-1, actLines["33"]);
		Assert.Equal(-1, actLines["34"]);
		Assert.Equal(-1, actLines["35"]);
		Assert.Equal(-1, actLines["36"]);
		Assert.Equal(6570, actLines["37"]);
		Assert.Equal(6720, actLines["38"]);
		Assert.Equal(6790, actLines["39"]);
		Assert.Equal(6720, actLines["40"]);
	}

	[Fact]
	public void EstimatedQDCG_0pc_No_Ordinary()
	{
		// ASSEMBLE
		// TODO: In the future, lines 1 - 3 will come from other forms. Simulate that here.

		decimal qualifiedDividends = 20100; // taxes: 0 (4000 after deduction is well within 0% bracket)
		decimal longTermCapitalGains = 0;
		decimal ordinaryDividends = 0;
		decimal shortTermCapitalGains = 0;
		decimal otherOrdinaryIncome = 0; // taxes: 0

		// This is (2026) Worksheet 2-9 "Annualized Estimated Tax Worksheet".
		// Line 1. The "raw" amounts keep decimal values, but each line on the worksheet is rounded as per IRS rules.
		int adjustedGrossIncome = TaxCalculations.Round(qualifiedDividends + longTermCapitalGains + ordinaryDividends + shortTermCapitalGains + otherOrdinaryIncome);

		// Line 2
		decimal annualizationFactor = 1; // 1st period = 4, 2nd period = 2.4, 3rd period = 1.5, 4th period = 1

		// Line 3
		int annualizedIncome = TaxCalculations.Round(adjustedGrossIncome * annualizationFactor);

		// Line 8.
		// TODO: Assume standard deduction for single filer.
		// TODO: Assume always 0 for lines 9a and 9b.
		int deduction = 16100;

		// Line 11. Taxable Income.
		int taxableIncome = annualizedIncome - deduction;

		// For Line 12, assume there is always qualified income, thus triggering use of the EQDCG Worksheet.
		// The term "net capital gain" means the amount by which the long-term captial gain is more than the net short-term capital loss.
		// TODO: Assume this is always the case. That is, even if ST is a loss, LT is a gain large enough to make their combination a gain.
		// TODO: Otherwise, lines 16 - 22 of Schedule D need to be taken into account. Also see line 3 of the 1040 QDCGT worksheet (i.e. the year-end version, not the estimated tax version).
		// What we want is the smaller of LT cap gain and (ST cap gain + LT cap gain).
		int netCapitalGain = Math.Min(TaxCalculations.Round(longTermCapitalGains * annualizationFactor), TaxCalculations.Round((shortTermCapitalGains + longTermCapitalGains) * annualizationFactor));

		// ACT
		int line1 = taxableIncome;
		int line2 = TaxCalculations.Round(qualifiedDividends * annualizationFactor);
		int line3 = netCapitalGain;
		Dictionary<string, int> actLines = TaxForms.EstimatedQualDivCapGainWorksheet(line1, line2, line3);

		// ASSERT
		// This worksheet has 42 lines (40 + 2 extra line 13s)
		Assert.Equal(42, actLines.Count);

		Assert.Equal(4000, actLines["1"]);
		Assert.Equal(20100, actLines["2"]);
		Assert.Equal(0, actLines["3"]);
		Assert.Equal(20100, actLines["4"]);
		Assert.Equal(0, actLines["5"]);
		Assert.Equal(0, actLines["6"]);
		Assert.Equal(0, actLines["7"]);
		Assert.Equal(0, actLines["8"]);
		Assert.Equal(20100, actLines["9"]);
		Assert.Equal(0, actLines["10"]);
		Assert.Equal(4000, actLines["11"]);
		Assert.Equal(0, actLines["12"]);
		Assert.Equal(0, actLines["13a"]);
		Assert.Equal(4000, actLines["13b"]);
		Assert.Equal(0, actLines["13c"]);
		Assert.Equal(0, actLines["14"]);
		Assert.Equal(4000, actLines["15"]);
		Assert.Equal(-1, actLines["16"]);
		Assert.Equal(-1, actLines["17"]);
		Assert.Equal(-1, actLines["18"]);
		Assert.Equal(-1, actLines["19"]);
		Assert.Equal(-1, actLines["20"]);
		Assert.Equal(-1, actLines["21"]);
		Assert.Equal(-1, actLines["22"]);
		Assert.Equal(-1, actLines["23"]);
		Assert.Equal(-1, actLines["24"]);
		Assert.Equal(-1, actLines["25"]);
		Assert.Equal(-1, actLines["26"]);
		Assert.Equal(-1, actLines["27"]);
		Assert.Equal(-1, actLines["28"]);
		Assert.Equal(-1, actLines["29"]);
		Assert.Equal(-1, actLines["30"]);
		Assert.Equal(-1, actLines["31"]);
		Assert.Equal(-1, actLines["32"]);
		Assert.Equal(-1, actLines["33"]);
		Assert.Equal(-1, actLines["34"]);
		Assert.Equal(-1, actLines["35"]);
		Assert.Equal(-1, actLines["36"]);
		Assert.Equal(0, actLines["37"]);
		Assert.Equal(0, actLines["38"]);
		Assert.Equal(400, actLines["39"]);
		Assert.Equal(0, actLines["40"]);
	}

	[Fact]
	public void EstimatedQDCG_QualDivOnly_20pc()
	{
		// ASSEMBLE
		// 600K in qualified dividends. No capital gains or ordinary income. Standard deduction of 16100.
		int line1 = 583900;
		int line2 = 600000;
		int line3 = 0;

		// ACT
		Dictionary<string, int> actLines = TaxForms.EstimatedQualDivCapGainWorksheet(line1, line2, line3);

		// ASSERT
		// This worksheet has 42 lines (40 + 2 extra line 13s)
		Assert.Equal(42, actLines.Count);

		Assert.Equal(line1, actLines["1"]);
		Assert.Equal(line2, actLines["2"]);
		Assert.Equal(line3, actLines["3"]);
		Assert.Equal(600000, actLines["4"]);
		Assert.Equal(0, actLines["5"]);
		Assert.Equal(0, actLines["6"]);
		Assert.Equal(0, actLines["7"]);
		Assert.Equal(0, actLines["8"]);
		Assert.Equal(600000, actLines["9"]);
		Assert.Equal(0, actLines["10"]);
		Assert.Equal(49450, actLines["11"]);
		Assert.Equal(0, actLines["12"]);
		Assert.Equal(0, actLines["13a"]);
		Assert.Equal(201775, actLines["13b"]);
		Assert.Equal(0, actLines["13c"]);
		Assert.Equal(0, actLines["14"]);
		Assert.Equal(49450, actLines["15"]);
		Assert.Equal(583900, actLines["16"]);
		Assert.Equal(49450, actLines["17"]);
		Assert.Equal(534450, actLines["18"]);
		Assert.Equal(545500, actLines["19"]);  // TODO: const because single; will change when supporting other filing statuses.
		Assert.Equal(545500, actLines["20"]);
		Assert.Equal(49450, actLines["21"]);
		Assert.Equal(496050, actLines["22"]);
		Assert.Equal(496050, actLines["23"]);
		Assert.Equal(74408, actLines["24"]);
		Assert.Equal(545500, actLines["25"]);
		Assert.Equal(38400, actLines["26"]);
		Assert.Equal(7680, actLines["27"]);
		Assert.Equal(0, actLines["28"]);
		Assert.Equal(600000, actLines["29"]);
		Assert.Equal(583900, actLines["30"]);
		Assert.Equal(16100, actLines["31"]);
		Assert.Equal(0, actLines["32"]);
		Assert.Equal(0, actLines["33"]);
		Assert.Equal(583900, actLines["34"]);
		Assert.Equal(0, actLines["35"]);
		Assert.Equal(0, actLines["36"]);
		Assert.Equal(0, actLines["37"]);
		Assert.Equal(82088, actLines["38"]);
		Assert.Equal(173134, actLines["39"]);
		Assert.Equal(82088, actLines["40"]);
	}
}

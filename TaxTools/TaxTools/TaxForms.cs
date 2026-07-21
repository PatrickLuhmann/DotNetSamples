namespace TaxTools;

public class TaxForms
{
	/// <summary>
	/// Calculates the values of the lines on the Estimated Tax Qualified Dividend And Capital Gains Worksheet.
	/// Lines that are skipped are given a value of -1.
	/// Note: Rounding occurs for each line. Specifically, the individual tax amounts that are summed
	///       are *NOT* kept as decimals until the summing at the end.
	/// </summary>
	/// <returns>
	/// The values of the lines of the worksheet.
	/// </returns>
	public static Dictionary<string, int> EstimatedQualDivCapGainWorksheet(int line1, int line2, int line3)
	{
        // The first three lines come directly from the input parameters.
		Dictionary<string, int> lines = new()
		{
			["1"] = line1,
			["2"] = line2,
			["3"] = line3
		};

		lines["4"] = lines["2"] + lines["3"];

		// These are always 0 for me.
		lines["5"] = 0;
		lines["6"] = 0;

		lines["7"] = lines["5"] + lines["6"];

		lines["8"] = Math.Min(lines["3"], lines["7"]);

		lines["9"] = lines["4"] + lines["8"];

		lines["10"] = Math.Max(lines["1"] - lines["9"], 0);

		lines["11"] = Math.Min(lines["1"], 49450);

		lines["12"] = Math.Min(lines["10"], lines["11"]);

		lines["13a"] = Math.Max(lines["1"] - lines["4"], 0);

		lines["13b"] = Math.Min(lines["1"], 201775);

		lines["13c"] = Math.Min(lines["10"], lines["13b"]);

		lines["14"] = Math.Max(lines["13a"], lines["13c"]);

		// If line 11 and line 12 are the same, skip line 15 and go to line 16.
		int temp15;
		if (lines["11"] == lines["12"])
		{
			temp15 = -1;
		}
		else
		{
			temp15 = lines["11"] - lines["12"];
		}
		lines["15"] = temp15;

		// If lines 1 and 11 are the same, skip lines 16 through 36 and go to line 37.
		if (lines["1"] == lines["11"])
		{
			lines["16"] = -1;
			lines["17"] = -1;
			lines["18"] = -1;
			lines["19"] = -1;
			lines["20"] = -1;
			lines["21"] = -1;
			lines["22"] = -1;
			lines["23"] = -1;
			lines["24"] = -1;
			lines["25"] = -1;
			lines["26"] = -1;
			lines["27"] = -1;
			lines["28"] = -1;
			lines["29"] = -1;
			lines["30"] = -1;
			lines["31"] = -1;
			lines["32"] = -1;
			lines["33"] = -1;
			lines["34"] = -1;
			lines["35"] = -1;
			lines["36"] = -1;
		}
		else
		{

			lines["16"] = Math.Min(lines["1"], lines["9"]);

			lines["17"] = Math.Max(0, lines["15"]);

			lines["18"] = Math.Max(lines["16"] - lines["17"], 0);

			lines["19"] = 545500;

			lines["20"] = Math.Min(lines["1"], lines["19"]);

			lines["21"] = lines["14"] + Math.Max(0, lines["15"]); // TODO: Can we get here if we skipped line 15 above?

			lines["22"] = Math.Max(lines["20"] - lines["21"], 0);

			lines["23"] = Math.Min(lines["18"], lines["22"]);

			lines["24"] = TaxCalculations.Round(lines["23"] * 0.15m); // 15% tax bracket

			lines["25"] = lines["17"] + lines["23"];

			// If line 1 equals the sum of lines 21 and 23, skip lines 26 through 36 and got to line 37.
			if (lines["1"] == lines["21"] + lines["23"])
			{
				lines["26"] = -1;
				lines["27"] = -1;
				lines["28"] = -1;
				lines["29"] = -1;
				lines["30"] = -1;
				lines["31"] = -1;
				lines["32"] = -1;
				lines["33"] = -1;
				lines["34"] = -1;
				lines["35"] = -1;
				lines["36"] = -1;
			}
			else
			{
				lines["26"] = lines["16"] - lines["25"];

				lines["27"] = TaxCalculations.Round(lines["26"] * 0.20m); // 20% tax bracket

				lines["28"] = Math.Min(lines["3"], lines["6"]);

				lines["29"] = lines["4"] + lines["14"];

				lines["30"] = lines["1"];

				lines["31"] = Math.Max(lines["29"] - lines["30"], 0);

				lines["32"] = Math.Max(lines["28"] - lines["31"], 0);

				lines["33"] = TaxCalculations.Round((lines["32"]) * 0.25m); // 25% tax bracket

				lines["34"] = lines["14"] + Math.Max(0, lines["15"]) + lines["23"] + lines["26"] + lines["32"];

				lines["35"] = lines["1"] - lines["34"];

				lines["36"] = TaxCalculations.Round((lines["35"]) * 0.28m); // 28% tax bracket
			}
		}

		lines["37"] = TaxCalculations.CalculateEstimatedTax(lines["14"]); // figure tax

		lines["38"] = Math.Max(0, lines["24"]) + Math.Max(0, lines["27"]) + Math.Max(0, lines["33"]) + Math.Max(0, lines["36"]) + lines["37"];

		lines["39"] = TaxCalculations.CalculateEstimatedTax(lines["1"]); // figure tax

		lines["40"] = Math.Min(lines["38"], lines["39"]);

		return lines;
	}
}

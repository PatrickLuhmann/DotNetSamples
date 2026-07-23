using ConsoleCC;

Console.WriteLine("Welcome to .NET Samples!");

bool quit = false;
while (!quit)
{
	Console.WriteLine("Please select a sample to run.");

	Console.WriteLine("1. Nutrtion database app");

	Console.WriteLine("2. Tax Helper app");

	Console.WriteLine("Q. Quit");

	string? input = Console.ReadLine();
	IConsoleSample? sample = null;
	switch (input?.ToLower())
	{
		case "1":
			sample = new NutritionSample();
			break;
		case "2":
			sample = new TaxHelperSample();
			break;
		case "q":
			quit = true;
			break;
		default:
			Console.WriteLine("ERROR: Input not recognized.");
			break;
	}

	sample?.Run();
}

Console.WriteLine("Thank you for trying .NET Samples!");

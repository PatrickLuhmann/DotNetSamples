using ConsoleCC;

Console.WriteLine("Welcome to .NET Samples!");

// 'args' is already populated for us.
foreach (var arg in args)
{
	Console.WriteLine($"Parameter: {arg}");
	SelectSample(arg);
}

bool quit = false;
while (!quit)
{
	Console.WriteLine();
	Console.WriteLine("Please select a sample to run.");
	Console.WriteLine("1. Nutrtion database app");
	Console.WriteLine("2. Tax Helper app");
	Console.WriteLine("3. Book Klub Korner app");
	Console.WriteLine("O. Other information");
	Console.WriteLine("Q. Quit");

	string? input = Console.ReadLine();
	quit = SelectSample(input);
}

Console.WriteLine("Thank you for trying .NET Samples!");
return;

void OtherInformation()
{
	Console.WriteLine("Here are some SpecialFolder locations");
	Console.WriteLine("=====================================");
	List<Environment.SpecialFolder> folderList =
	[
		Environment.SpecialFolder.LocalApplicationData,
		Environment.SpecialFolder.CommonApplicationData,
		Environment.SpecialFolder.CommonDesktopDirectory,
		Environment.SpecialFolder.ApplicationData,
		Environment.SpecialFolder.MyDocuments,
		Environment.SpecialFolder.UserProfile
	];
	foreach (var folder in folderList)
		Console.WriteLine($"{folder}: {Environment.GetFolderPath(folder)}");
	Console.WriteLine();
}

bool SelectSample(string? id)
{
	bool quit = false;
	IConsoleSample? sample = null;
	switch (id?.ToLower())
	{
		case "1":
			sample = new NutritionSample();
			break;
		case "2":
			sample = new TaxHelperSample();
			break;
		case "3":
			sample = new BookKlubKornerSample();
			break;
		case "o":
			OtherInformation();
			break;
		case "q":
			quit = true;
			break;
		default:
			Console.WriteLine("ERROR: Input not recognized.");
			break;
	}

	sample?.Run();
	return quit;
}
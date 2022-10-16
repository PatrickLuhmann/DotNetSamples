// See https://aka.ms/new-console-template for more information
using DotNetSamples;
using DotNetSamples.Nutrition_Sample;

Console.WriteLine("Welcome to .NET Samples!");

bool quit = false;
while (!quit)
{
	Console.WriteLine("Please select a sample to run.");

	Console.WriteLine("1. Nutrtion database app");

	Console.WriteLine("Q. Quit");

	string? input = Console.ReadLine();
	IConsoleSample sample = null;
	switch (input?.ToLower())
	{
		case "1":
			sample = new NutritionSample();
			break;
		case "q":
			quit = true;
			break;
		default:
			Console.WriteLine("ERROR: Input not recognized.");
			break;
	}
	if (sample != null)
		sample.Run();
}

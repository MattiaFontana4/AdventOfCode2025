// See https://aka.ms/new-console-template for more information
using Day1;

String path = "C:/AdventOfCode/Day1/input.txt";

if (!File.Exists(path))
{
	Console.WriteLine("Istructions file not found!");
	return;
}

// Read the istruction file
String istructionText = File.ReadAllText(path);

// Split the istruction text into lines
String[] istructionArray = istructionText.Split('\n');

// Trim whitespace and filter out empty lines
IEnumerable<String> istructionLines = istructionArray.Select(line => line.Trim()).Where(line => !String.IsNullOrWhiteSpace(line));

// Parse the istruction lines into Istruction objects
IEnumerable< Istruction> istructions = istructionLines.Select(istructionLine => new Istruction(istructionLine.Trim()));

// Initialize the dial with starting position 50
Dial dial = new Dial(50);

// Apply each istruction to the dial
foreach (Istruction instruction in istructions)
{
	dial.ApplyIstruction(instruction);
}

// Output the number of times the dial hit zero
Console.WriteLine($"Number of times the dial hit zero: {dial.NumberOfZeros}");

// Output the total number of clicks
Console.WriteLine("Number of clicks: " + dial.NumberOfClicks);



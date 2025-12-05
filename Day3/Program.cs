// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

String path = "C:\\AdventOfCode\\Day3\\input.txt";

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}

String text = File.ReadAllText(path);

string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

IEnumerable<string> lineList = lines.Select(line => line.Trim()).Where(line => !string.IsNullOrEmpty(line));

var banks = lineList.Select(line => new BatteryBank(line));

IEnumerable<byte> maxCombinations = banks.Select(bank => bank.findMaxJoltageCombination());

int sumOfMaxCombinations = maxCombinations.Sum(mc => mc);

Console.WriteLine("Sum of max joltage combinations: " + sumOfMaxCombinations);


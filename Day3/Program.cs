// See https://aka.ms/new-console-template for more information

String path = "C:\\AdventOfCode\\Day3\\input.txt";
int banksDim = 12;

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}

String text = File.ReadAllText(path);

string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

IEnumerable<string> lineList = lines.Select(line => line.Trim()).Where(line => !string.IsNullOrEmpty(line));

var banks = lineList.Select(line => new BatteryBank(line));

IEnumerable<ulong> maxCombinations = banks.Select(bank => bank.findMaxJoltageCombination(12));

Decimal sumOfMaxCombinations = maxCombinations.Sum(mc => new Decimal(mc));

Console.WriteLine("Sum of max joltage combinations: " + sumOfMaxCombinations);


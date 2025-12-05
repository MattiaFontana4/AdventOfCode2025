// See https://aka.ms/new-console-template for more information

String path = "C:\\AdventOfCode\\Day2\\input.txt";

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}

String text = File.ReadAllText(path);
String[] lines = text.Split(',', StringSplitOptions.RemoveEmptyEntries);

IEnumerable<IdRange> ranges = lines.Where(e => !String.IsNullOrEmpty(e))
	.Select(e => new IdRange(e));

IEnumerable<UInt128> invalidIds = ranges.SelectMany(r => r.GetInvalidIds()).Distinct();

UInt128 sum = invalidIds.Aggregate<UInt128, UInt128>(0, (current, id) => current + id);

Console.WriteLine("sum of the invalid ids: " + sum);


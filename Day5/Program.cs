// See https://aka.ms/new-console-template for more information

String path = "C:\\AdventOfCode\\Day5\\input.txt";


if (!File.Exists(path))
{
    Console.WriteLine("File not found.");
    return;
}
String text = File.ReadAllText(path);

String[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
var freshIdRanges = lines.Where(e => !String.IsNullOrWhiteSpace(e) && e.Contains('-')).Select(e => new IdRange(e));
var ingredietsIds = lines.Where(e => !String.IsNullOrWhiteSpace(e) && !e.Contains('-')).Select(e => UInt128.Parse(e));

var freshingredietsIds = ingredietsIds.Where(id => freshIdRanges.Any(range => range.IsIdInRange(id)));

var count = freshingredietsIds.Count();

Console.WriteLine("Number of fresh ingredient IDs: " + count);

RangeSet freshIdSets = new RangeSet();

freshIdSets.AddRange(freshIdRanges);

freshIdSets.Optimize();

freshIdSets.Optimize();

if (!freshIdSets.IsOptimized()) 
{ 
    Console.WriteLine("The RangeSet is not optimized.");
}

var countTotalFreshIds = freshIdSets.Count;

Console.WriteLine("Total number of fresh ingredient IDs: " + countTotalFreshIds);








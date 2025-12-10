// See https://aka.ms/new-console-template for more information
String path = "C:\\AdventOfCode\\Day7\\input.txt";

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}

char[][] matrix = File.ReadAllLines(path)
	.Select(line => line.PadRight(100, '.').ToCharArray())
	.ToArray();

TachyonManifold manifold = new TachyonManifold(matrix);

manifold.Execute();

int numRaysAtEnd = manifold.NumRaysAsEnd;

Console.WriteLine($"Number of rays at the end: {numRaysAtEnd}");

int numTotalSplittings = manifold.NumTotalSplittings;

Console.WriteLine($"Total number of splittings: {numTotalSplittings}");





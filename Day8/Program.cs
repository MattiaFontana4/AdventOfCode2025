// See https://aka.ms/new-console-template for more information
using Day8;

String path = "C:\\AdventOfCode\\Day8\\input.txt";

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}

string[] lines = File.ReadAllLines(path);

List<Position> positions = new List<Position>();

foreach (string line in lines)
{ 
	string[] parts = line.Trim().Split(',');

	Position pos = new Position(
		int.Parse(parts[0]),
		int.Parse(parts[1]),
		int.Parse(parts[2])
	);

	positions.Add(pos);
}

int numberOfConnessions = 1000;

EngineFast engine = new EngineFast(positions);

engine.Execute(numberOfConnessions);

int product = engine.ProductOfCircuits;

Console.WriteLine($"Product of circuits: {product}");

int prodX = engine.ProdX;

Console.WriteLine($"Prod of X: {prodX}");




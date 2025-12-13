using Day9;

String path = "C:\\AdventOfCode\\Day9\\input.txt";

if (!File.Exists(path))
{
    Console.WriteLine("File not found.");
    return;
}

string[] lines = File.ReadAllLines(path);

List<Tile> tiles = new List<Tile>(lines.Count());

foreach (string line in lines)
{
    string[] parts = line.Split(',');
    int x1 = int.Parse(parts[0]);
    int y1 = int.Parse(parts[1]);

    Tile tile = new Tile(x1, y1);
    tiles.Add(tile);
}

ulong maxArea = MaxAreaFinder.CalculateMaxArea(tiles);

Console.WriteLine($"Max Area: {maxArea}");

PlaneTiles planeTiles = new PlaneTiles(tiles);

ulong maxAreaInside = planeTiles.GetBiggestArea();

Console.WriteLine($"Max Area Inside: {maxAreaInside}");


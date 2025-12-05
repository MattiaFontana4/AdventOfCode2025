// See https://aka.ms/new-console-template for more information

String path = "C:\\AdventOfCode\\Day4\\input.txt";

RollOfPaperHashMap rollOfPaper;

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}
else
{
	String text = File.ReadAllText(path);

	rollOfPaper = new RollOfPaperHashMap(text);
}

uint accessibleRollsOfPaper = rollOfPaper.countAccessibleRollOfPaper(3);

Console.WriteLine("Number of accessible rolls of paper: " + accessibleRollsOfPaper);

uint totalRollsOfPaperFemoved = rollOfPaper.RemoveAllAccessibleRollsOfPaper(3);

Console.WriteLine("Total rolls of paper removed: " + totalRollsOfPaperFemoved);




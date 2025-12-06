// See https://aka.ms/new-console-template for more information

String path = "C:\\AdventOfCode\\Day6\\input.txt";

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}

String text = File.ReadAllText(path);

MathProblemBuilder builder = new MathProblemBuilder(text);
var mathProblem = builder.Build();

decimal finalResult = mathProblem.Select(e=> e.Solve()).Sum();

Console.WriteLine("The final result is: " + finalResult);




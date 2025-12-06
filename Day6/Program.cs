// See https://aka.ms/new-console-template for more information

String path = "C:\\AdventOfCode\\Day6\\input.txt";

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}

String text = File.ReadAllText(path);

HumanMathProblemBuilder builder = new HumanMathProblemBuilder(text);
var mathProblem = builder.Build();

decimal finalResult = mathProblem.Select(e=> e.Solve()).Sum();

Console.WriteLine("Part One - The final result is: " + finalResult);

CephalopodMathProblemBuilder cephalopodMathProblemBuilder = new CephalopodMathProblemBuilder(text);

decimal cephalopodFinalResult = cephalopodMathProblemBuilder.Build().Select(e => e.Solve()).Sum();

Console.WriteLine("Part Two - The final result is: " + cephalopodFinalResult);


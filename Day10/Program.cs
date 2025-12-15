// See https://aka.ms/new-console-template for more information

using Day10;
using System.Reflection.PortableExecutable;

String path = "C:\\AdventOfCode\\Day10\\input.txt";

if (!File.Exists(path))
{
	Console.WriteLine("File not found.");
	return;
}

string[] lines = File.ReadAllLines(path);

List<Macchine> machines = lines.Select(l => new Macchine(l)).ToList();

if (false)
{
    Console.WriteLine("macchine list initialized");
    Console.WriteLine($"Macchine created: {machines.Count}");

    List<int> nums = new List<int>();

    for (int i = 0; i < machines.Count; i++)
    {
        Console.WriteLine($"{i} - Starting calculating macchine  - Number of States:{machines[i].NumStates} - Number of buttons: {machines[i].NumButtons}");
        int maxDepth = 8;
        DateTime startTime = DateTime.Now;
        Console.WriteLine($"{i} - Max depth {maxDepth}");


        int n = machines[i].FindMinNumApplyTo(maxDepth);

        if (n > 1000)
            throw new Exception("Increse maxCallStack");

        nums.Add(n);

        DateTime endTime = DateTime.Now;
        Console.WriteLine($"{i} - Finish calculating macchine - Time {(endTime - startTime).TotalSeconds}");
    }

    int res1 = nums.Sum(x => x);

    Console.WriteLine($"Result part 1: {res1}");
}
Console.WriteLine("Starting part 2");

Console.WriteLine("Re-initializing macchine list");

machines = lines.Select(l => new Macchine(l)).ToList();

List<int> nums2 = new List<int>(machines.Count);

for (int i = 0; i < machines.Count; i++)
{
    Console.WriteLine($"{i} - Starting calculating macchine  - Number of States:{machines[i].NumStates} - Number of buttons: {machines[i].NumButtons}");
    int maxDepth = machines[i].getJoltageMaxCallStack();
    DateTime startTime = DateTime.Now;
    Console.WriteLine($"{i} - Max depth {maxDepth}");
    int n = machines[i].GetMinNumApplyToJoltageFast();


    if (n > 1000)
        throw new Exception("Error dunring calculation");
    else
        nums2.Add(n);

    DateTime endTime = DateTime.Now;
    Console.WriteLine($"{i} - Finish calculating macchine - Time {(endTime - startTime).TotalSeconds }");
}

int res2 = nums2.Sum(x => x);

Console.WriteLine($"Result part 2: {res2}");






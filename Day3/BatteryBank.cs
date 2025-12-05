// See https://aka.ms/new-console-template for more information

internal class BatteryBank
{
    private Byte[] joltageRatings;

    public BatteryBank(string line)
    {
        joltageRatings = line.Where(c => Char.IsDigit(c)).Select(c => byte.Parse(c.ToString())).ToArray();
    }

    public Byte findMaxJoltageCombination()
    {
        var sub1 = joltageRatings[..^1];
        var max = sub1.Max();
        var indexOfMax = Array.IndexOf(joltageRatings, max);
        var sub2 = joltageRatings[(indexOfMax + 1)..];
        var secondMax = sub2.Max();
        return (Byte)((max * 10) + secondMax);
	}
}
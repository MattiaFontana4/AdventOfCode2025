// See https://aka.ms/new-console-template for more information

internal class BatteryBank
{
    private Byte[] joltageRatings;

    public BatteryBank(string line)
    {
        joltageRatings = line.Where(c => Char.IsDigit(c)).Select(c => byte.Parse(c.ToString())).ToArray();
    }

    public ulong findMaxJoltageCombination(int n)
    {
        Byte[] maxes = new byte[n];
        int startIndex = -1;
        for (int i = 0; i < n; i++)
        {
            var sub = joltageRatings[(startIndex + 1)..^(n - i - 1)];
            byte currentMax = sub.Max();

            maxes[i] = currentMax;

            var indexOfMax = Array.IndexOf(sub, currentMax);

            startIndex = startIndex + 1 + indexOfMax;
		}

        ulong sumResult = 0;

		for (int i = 0; i < n; i++)
        {
            sumResult += maxes[i] * ((ulong)System.Numerics.BigInteger.Pow(10,n-i-1));
		}

        return sumResult;
	}
}
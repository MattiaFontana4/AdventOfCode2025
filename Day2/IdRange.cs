// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

internal class IdRange
{
	public UInt128 MinId { get; private set; }
	public UInt128 MaxId { get; private set; }

	private static readonly Regex rangeRegex = new Regex(@"^(\d+)(?:\1){1,}$", RegexOptions.Compiled);

	public IdRange(String range)
	{
		String[] parts = range.Split('-', StringSplitOptions.RemoveEmptyEntries);
		if (parts.Length != 2)
		{
			throw new ArgumentException("Invalid range format", nameof(range));
		}

		MinId = UInt128.Parse(parts[0]);
		MaxId = UInt128.Parse(parts[1]);
	}

	public IEnumerable<UInt128> GetInvalidIds()
	{
		List<UInt128> invalid = new List<UInt128>();

		for (UInt128 id = MinId; id <= MaxId; id++)
		{
			String idStr = id.ToString();

			if (rangeRegex.IsMatch(idStr))
			{
				invalid.Add(id);
			}
		}

		return invalid;
	}


}
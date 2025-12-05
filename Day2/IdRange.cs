// See https://aka.ms/new-console-template for more information

internal class IdRange
{
	public UInt128 MinId { get; private set; }
	public UInt128 MaxId { get; private set; }

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

			if (idStr.Length % 2 == 0) 
			{
				String idStrSub1 = idStr.Substring(0, idStr.Length / 2);
				String idStrSub2 = idStr.Substring(idStr.Length / 2, idStr.Length / 2);
				
				if (idStrSub1 == idStrSub2)
				{
					invalid.Add(id);
				}
			}
		}

		return invalid;
	}


}
// See https://aka.ms/new-console-template for more information

internal class IdRange
{
    private UInt128 minId;
    private UInt128 maxId;

	public IdRange(string e)
    {
        String[] parts = e.Split('-', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid range format", nameof(e));
        }
        minId = UInt128.Parse(parts[0]);
        maxId = UInt128.Parse(parts[1]);
	}

    public bool IsIdInRange(UInt128 id)
    {
        return id >= minId && id <= maxId;
	}
}
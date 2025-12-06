// See https://aka.ms/new-console-template for more information

public class IdRange
{
    private UInt128 minId;
    private UInt128 maxId;

    public UInt128 MinId => minId;
    public UInt128 MaxId => maxId;

	public IdRange(string e)
    {
        String[] parts = e.Split('-', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid range format", nameof(e));
        }
        minId = UInt128.Parse(parts[0]);
        maxId = UInt128.Parse(parts[1]);

        if (minId > maxId)
        {
            throw new ArgumentException("Invalid range: minId is greater than maxId", nameof(e));
        }
    }

    public IdRange(UInt128 minId, UInt128 maxId)
    {
        if (minId > maxId)
        {
            throw new ArgumentException("Invalid range: minId is greater than maxId", nameof(minId));
        }
        this.minId = minId;
        this.maxId = maxId;
    }

    public bool IsIdInRange(UInt128 id)
    {
        return id >= minId && id <= maxId;
    }


    public ISet<UInt128> ToSet()
    {
        var result = new HashSet<UInt128>();

        for (var i = minId; i <= maxId; i++)
        {
            result.Add(i);
        }

        return result;
    }

    public bool IsMergeableWith(IdRange other)
    {
        bool resut = this.maxId + 1 >= other.minId && this.minId - 1 <= other.maxId;

        return resut;
    }

    public IdRange MergeWith(IdRange other)
    {
        if (!IsMergeableWith(other))
        {
            throw new InvalidOperationException("Ranges are not mergeable");
        }

        UInt128 newMin = this.minId < other.minId ? this.minId : other.minId;
        UInt128 newMax = this.maxId > other.maxId ? this.maxId : other.maxId;
        return new IdRange(newMin, newMax);
    }

    public static IdRange operator +(IdRange a, IdRange b)
    {
        return a.MergeWith(b);    
    }

    public UInt128 Count()
    {
        return (maxId - minId) + 1;
	}

} 
// See https://aka.ms/new-console-template for more information

public class RangeSet
{
	public List<IdRange> Ranges { get; private set; }

	public RangeSet()
	{
		Ranges = new List<IdRange>();
	}

	public void Add(IdRange range) 
	{
        IdRange? mergeableRanges = Ranges.Find(e => e.IsMergeableWith(range));

		if (mergeableRanges != null) { 
			var newMergeableRange = mergeableRanges.MergeWith(range);

			Ranges.Remove(mergeableRanges);
			Ranges.Add(newMergeableRange);
		}
        else
        {
            Ranges.Add(range);
		}
    }

	public void AddRange(IEnumerable<IdRange> ranges)
	{
		foreach (var r in ranges)
		{
			Add(r);
		}
	}

	public UInt128 Min()
	{
		if (Ranges.Count == 0)
		{
			throw new InvalidOperationException("RangeSet is empty");
		}
		return Ranges.Min(e => e.MinId);
	}

	public UInt128 Max()
	{
		if (Ranges.Count == 0)
		{
			throw new InvalidOperationException("RangeSet is empty");
		}
		return Ranges.Max(e => e.MaxId);
	}

	public UInt128 Count
	{
		get
		{
			UInt128 total = 0;

			foreach (var range in Ranges)
			{
				total += range.Count();
			}

			return total;
		}
	}

	public bool IsOptimized()
	{
		bool result = !Ranges.Any(e1 => Ranges.Any(e2 => e1 != e2 && e1.IsMergeableWith(e2)));
		return result;
	}

	public void Optimize()
	{
		var optimizedRanges = new List<IdRange>();
		foreach (var range in Ranges)
		{
			IdRange? mergeableRange = optimizedRanges.Find(e => e.IsMergeableWith(range));
			if (mergeableRange != null)
			{
				var newMergeableRange = mergeableRange.MergeWith(range);
				optimizedRanges.Remove(mergeableRange);
				optimizedRanges.Add(newMergeableRange);
			}
			else
			{
				optimizedRanges.Add(range);
			}
		}
		Ranges = optimizedRanges;
	}


}
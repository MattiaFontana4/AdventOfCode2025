// See https://aka.ms/new-console-template for more information
using Day7;
using System.Linq;

internal class TachyonManifold
{
	int _totalWidth;
	int _totalHeight;

	int _beamEnterWidthIndex;

	int _numRaysasEnd;
	int _numTotalSplittings;
	long _numTimelines;

	public int NumRaysAsEnd { get { return _numRaysasEnd; } }
	public int NumTotalSplittings { get { return _numTotalSplittings; } }

	public long NumTimelines { get { return _numTimelines; } }

	private List<Splitter> _splitters;

	public List<Splitter> Splitters { get { return _splitters; } }

	public TachyonManifold(char[][] matrix)
	{

		this._totalWidth = matrix[0].Length;
		this._totalHeight = matrix.Length;

		_beamEnterWidthIndex = matrix[0].IndexOf('S');
		_numTotalSplittings = 0;

		if (_beamEnterWidthIndex == -1)
			throw new Exception("beam enter not found.");

		_splitters = new List<Splitter>();

		for (int y = 0; y < _totalHeight; y++)
		{
			for (int x = 0; x < _totalWidth; x++)
			{
				if (matrix[y][x] == '^')
				{
					_splitters.Add(new Splitter(x, y));
				}
			}
		}
	}

	public void Execute() 
	{
		int totalSplits = 0;
		List<Ray> rays = new List<Ray>();
		List<Ray> newrays = new List<Ray>();
		List<Ray> toAddList = new List<Ray>();


		rays.Add(new Ray(_beamEnterWidthIndex, 0, _totalHeight));


		while (rays.Any(r => !r.IsBlocked))
        {

            newrays.Clear();

            foreach (var ray in rays)
            {

                if (!ray.IsBlocked && Splitters.Any(e => e.isBlockingRay(ray)))
                {
                    var blockingSlitter = Splitters.Find(e => e.isBlockingRay(ray));

                    if (blockingSlitter != null)
                    {
                        // Split the ray
                        IEnumerable<Ray> toAdd = blockingSlitter.SplitRay(ray);
						// Prepare the list of rays to add
						toAddList.Clear();
						// Remove rays that are already present in new rays
						foreach (var rayT in toAdd) 
						{ 
							var existingRay = newrays.Find(r => r.isRayInside(rayT));
							if (existingRay != null) 
							{
								existingRay.MergeTimelines(rayT);
							}
							else 
							{
								toAddList.Add(rayT);
							}
						}

                        // Add the new rays
                        newrays.AddRange(toAddList);

						// Block the original ray
						ray.Block();

                        totalSplits++;
                    }
                }
				ray.ExecuteStep();
            }

			toAddList.Clear();

			foreach (var rayT in newrays)
			{
				var existingRay = rays.Find(r => r.isRayInside(rayT));
				if (existingRay != null)
				{
					existingRay.MergeTimelines(rayT);
				}
				else
				{
					toAddList.Add(rayT);
				}
			}

			rays.AddRange(toAddList);
            
			rays = RemoveDuplicateRays(rays);
        }

        _numRaysasEnd = rays.Count(e => e.isBlockedAtEnd());
		_numTotalSplittings = totalSplits;

		_numTimelines = rays.Where(e => e.isBlockedAtEnd()).Sum(e => (long)e.NumTimelines);

	}

    private List<Ray> RemoveDuplicateRays(List<Ray> rays)
    {
        var filteredRays = rays.Where(e => !rays.Any(i => (i.isRayInside(e) && !i.Equals(e))))
			.ToList();
        return filteredRays;
    }

}
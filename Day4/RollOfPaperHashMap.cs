// See https://aka.ms/new-console-template for more information

public class RollOfPaperHashMap
{
	private Element[,] hashMap;

	public RollOfPaperHashMap(string text)
	{
		string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

		hashMap = new Element[lines.Length, lines[0].Trim().Length];

		for (int i = 0; i < lines.Length; i++)
		{
			string line = lines[i].Trim();
			for (int j = 0; j < line.Length; j++)
			{
				hashMap[i, j] = new Element(line[j].ToString());
			}
		}
	}

	public bool IsRollOfPaperAt(int row, int col)
	{
		if (row < 0 || row >= hashMap.GetLength(0) || col < 0 || col >= hashMap.GetLength(1))
		{
			return false;
		}

		return hashMap[row, col].IsRollOfPaper;
	}

	public bool IsRollOfPaperAccessible(int row, int col, int maxAdjacentPapers)
	{
		if (maxAdjacentPapers < 0)
			throw new ArgumentException("maxAdjacentPapers cannot be negative.");

		int numberOfAdjacentPapers = 0;

		if (IsRollOfPaperAt(row + 1, col - 1))
			numberOfAdjacentPapers++;

		if (IsRollOfPaperAt(row + 1, col))
			numberOfAdjacentPapers++;

		if (IsRollOfPaperAt(row + 1, col + 1))
			numberOfAdjacentPapers++;

		if (IsRollOfPaperAt(row, col - 1))
			numberOfAdjacentPapers++;

		if (IsRollOfPaperAt(row, col + 1))
			numberOfAdjacentPapers++;

		if (IsRollOfPaperAt(row - 1, col - 1))
			numberOfAdjacentPapers++;

		if (IsRollOfPaperAt(row - 1, col))
			numberOfAdjacentPapers++;

		if (IsRollOfPaperAt(row - 1, col + 1))
			numberOfAdjacentPapers++;

		bool result = (numberOfAdjacentPapers <= maxAdjacentPapers);

		return result;
	}

	public uint countAccessibleRollOfPaper(int maxAdjacentPapers)
	{
		if (maxAdjacentPapers < 0)
			throw new ArgumentException("maxAdjacentPapers cannot be negative.");

		uint count = 0;

		for (int i = 0; i < hashMap.GetLength(0); i++)
		{
			for (int j = 0; j < hashMap.GetLength(1); j++)
			{
				if (IsRollOfPaperAt(i, j) && IsRollOfPaperAccessible(i, j, maxAdjacentPapers))
				{
					count++;
				}
			}
		}
		return count;
	}

	public uint RemoveAccessibleRollsOfPaper(int maxAdjacentPapers)
	{
		if (maxAdjacentPapers < 0)
			throw new ArgumentException("maxAdjacentPapers cannot be negative.");

		uint count = 0;
		for (int i = 0; i < hashMap.GetLength(0); i++)
		{
			for (int j = 0; j < hashMap.GetLength(1); j++)
			{
				if (IsRollOfPaperAt(i, j) && IsRollOfPaperAccessible(i, j, maxAdjacentPapers))
				{
					hashMap[i, j] = new Element(".");
					count++;
				}
			}
		}
		return count;
	}

	public uint RemoveAllAccessibleRollsOfPaper(int maxAdjacentPapers)
	{
		if (maxAdjacentPapers < 0)
			throw new ArgumentException("maxAdjacentPapers cannot be negative.");
		uint totalRemoved = 0;
		uint removedInIteration;
		do
		{
			removedInIteration = RemoveAccessibleRollsOfPaper(maxAdjacentPapers);
			totalRemoved += removedInIteration;
		} while (removedInIteration > 0);
		return totalRemoved;
	}

}



internal class Element
{
	private bool isRollOfPaper;

	public bool IsRollOfPaper
	{
		get { return isRollOfPaper; }
		private set { isRollOfPaper = value; }
	}

	public Element(String s) 
	{
		if (s == null)
			throw new ArgumentNullException("s");

		if (String.Equals(s, "@"))
		{
			this.isRollOfPaper = true;
		}
		else if (s == ".")
		{
			this.isRollOfPaper = false;
		}
		else
		{
			throw new ArgumentException("Invalid character for Element: " + s);
		}
	}
}
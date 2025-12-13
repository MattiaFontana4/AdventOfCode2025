using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day9
{
    public static class MaxAreaFinder
    {
		public static ulong CalculateMaxArea(List<Tile> tiles)
		{

			ulong maxArea = 0;

			for (int i = 0; i < tiles.Count; i++)
			{
				for (int j = i + 1; j < tiles.Count; j++)
				{
					Tile tileA = tiles[i];
					Tile tileB = tiles[j];
					ulong area = Box.GetArea(tileA, tileB);
					if (area > maxArea)
					{
						maxArea = area;
					}
				}
			}

			return maxArea;
		}
	}

	[TestClass]
	public class MaxAreaFinderTestClass
	{
		string textText = "7,1\n11,1\n11,7\n9,7\n9,5\n2,5\n2,3\n7,3";

		List<Tile> tiles;

		public MaxAreaFinderTestClass()
		{
			tiles = new List<Tile>();

			string[] lines = textText.Split('\n');
			foreach (string line in lines)
			{
				string[] parts = line.Split(',');
				int x1 = int.Parse(parts[0]);
				int y1 = int.Parse(parts[1]);
				Tile tile = new Tile(x1, y1);
				tiles.Add(tile);
			}
		}

		[TestMethod]
		public void MaxAreaFinderTest()
		{
			ulong maxArea = MaxAreaFinder.CalculateMaxArea(tiles);
			Assert.AreEqual((ulong)50, maxArea);
		}
	
	}

}

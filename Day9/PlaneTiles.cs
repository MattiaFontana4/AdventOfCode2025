using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace Day9
{

	public class PlaneTiles
	{
		private List<Tile> _tiles;
		Polygon _polygon;

		int width = 0;
		int height = 0;


		public PlaneTiles(List<Tile> tiles)
		{
			_tiles = tiles;

			width = tiles.Max(t => t.X) + 1;
			height = tiles.Max(t => t.Y) + 1;

			_polygon = new Polygon(tiles);

		}


		public ulong GetBiggestArea()
		{
			ulong maxArea = (ulong)1;

			Mutex maxAreaMutex = new Mutex();

			Mutex iCompletedMutex = new Mutex();

			Console.WriteLine("Starting area calculations...");

			int iCompleted = 0;

			Parallel.For(0,
				_tiles.Count,
				new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
				i =>
				{

					Console.WriteLine($"Processing tile {i + 1} of {_tiles.Count}...");
					Tile tile = _tiles[i];

					for (int j = i; j < _tiles.Count; j++)
					{
						Tile otherTile = _tiles[j];
						if (!tile.Equals(otherTile) && tile.X != otherTile.X && tile.Y != otherTile.Y)
						{
							Box box = new Box(tile, otherTile);
							ulong area = box.GetArea();
							if (area > maxArea)
							{
								if (_polygon.Contains(box))
								{
									maxAreaMutex.WaitOne();

									if (area > maxArea)
									{
										maxArea = area;
									}
									maxAreaMutex.ReleaseMutex();
								}
							}
						}
					}
					int myICompleted = 0;

					iCompletedMutex.WaitOne();
					myICompleted = iCompleted++;
					iCompletedMutex.ReleaseMutex();

					Console.WriteLine($"Completed {myICompleted + 1} of {_tiles.Count} tiles.");
					Console.WriteLine($"Terminating thread {i}");
				});

			Console.WriteLine("Area calculations completed.");

			return maxArea;
		}

		public void SaveBitmap(string path)
		{
			if (path == null)
				throw new ArgumentNullException("path");

			if (path.Length == 0)
				throw new ArgumentException("path");

			if (!path.Contains(".bmp"))
				throw new ArgumentException("path");

			Bitmap bmp = new Bitmap(width, height);

			for (var i = 0; i < width; i++)
			{
				for (var j = 0; j < height; j++)
				{
					Color color = Color.White;
					//switch (plane[i][j])
					//{
					//    case TileType.Neutral:
					//        color = Color.White;
					//        break;
					//    case TileType.Red:
					//        color = Color.Red;
					//        break;
					//    case TileType.Green:
					//        color = Color.Green;
					//        break;
					//}
					bmp.SetPixel(i, j, color);
				}
			}

			bmp.Save(path);
		}

	}

	public enum TileType : Byte
	{
		Neutral = 0,
		Red,
		Green
	}

	[TestClass]
	public class PlaneTilesTestClass
	{
		string textText = "7,1\n11,1\n11,7\n9,7\n9,5\n2,5\n2,3\n7,3";

		string textText2 = "1,1\n" +
						   "1,6\n" +
						   "8,6\n" +
						   "8,3\n" +
						   "3,3\n" +
						   "3,4\n" +
						   "6,4\n" +
						   "6,1";


		List<Tile> tiles;

		List<Tile> tiles2;

		public PlaneTilesTestClass()
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

			tiles2 = new List<Tile>();
			lines = textText2.Split('\n');

			foreach (string line in lines)
			{
				string[] parts = line.Split(',');
				int x1 = int.Parse(parts[0]);
				int y1 = int.Parse(parts[1]);
				Tile tile = new Tile(x1, y1);
				tiles2.Add(tile);
			}
		}

		[TestMethod]
		public void MaxAreaFinderTest()
		{
			PlaneTiles planeTiles = new PlaneTiles(tiles);
			ulong maxAreaInside = planeTiles.GetBiggestArea();
			Assert.AreEqual((ulong)24, maxAreaInside);

		}

		[TestMethod]
		public void MaxAreaFinderTest2()
		{
			PlaneTiles planeTiles = new PlaneTiles(tiles2);
			ulong maxAreaInside = planeTiles.GetBiggestArea();
			Assert.AreEqual((ulong)24, maxAreaInside);


		}


	}
}
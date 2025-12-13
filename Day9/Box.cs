using System;
using System.Collections.Generic;
using System.Text;

namespace Day9
{
	public class Box
	{
		private Tile a;
		private Tile b;

		public int MinX
		{
			get
			{
				int min = a.X > b.X ? b.X : a.X;
				return min;
			}
		}

		public int MaxX
		{
			get
			{
				int max = a.X > b.X ? a.X : b.X;
				return max;
			}
		}

		public int MinY
		{
			get
			{
				int min = a.Y > b.Y ? b.Y : a.Y;
				return min;
			}
		}

		public int MaxY
		{
			get
			{
				int max = a.Y > b.Y ? a.Y : b.Y;
				return max;
			}
		}


		public Box(Tile a, Tile b)
		{
			this.a = a;
			this.b = b;
		}

		public Tile[] GetVertices()
		{
			Tile[] result = new Tile[4];

			result[0] = a;
			result[1] = new Tile(a.X,b.Y);
			result[2] = b;
			result[3] = new Tile(b.X,a.Y);

			return result;
		}

		public Segment[] GetSegments() 
		{
			Segment[] result = new Segment[4];
			var tiles = GetVertices();

			for (int i = 0; i < 4; i++)
			{
				result[i] = new Segment(tiles[i], tiles[(i + 1) % 4]);
			}

			return result;
		}


		public ulong GetArea()
		{
			ulong area = GetArea(a, b);

			return area;
		}

		public static ulong GetArea(Tile a, Tile b)
		{
			ulong area = 0;

			ulong width = (ulong)Math.Abs(b.X - a.X) + 1;
			ulong height = (ulong)Math.Abs(b.Y - a.Y) + 1;
			area = height * width;
			return area;
		}

		public bool isInside(Tile t)
		{
			bool inside = false;
			if (t.X >= MinX && t.X <= MaxX && t.Y >= MinY && t.Y <= MaxY)
			{
				inside = true;
			}
			return inside;
		}

		public bool isInside(Box box)
		{
			bool inside = false;
			if (box.MinX >= MinX && box.MaxX <= MaxX && box.MinY >= MinY && box.MaxY <= MaxY)
			{
				inside = true;
			}
			return inside;
		}

		public bool isInside(int x, int y)
		{
			bool inside = false;
			if (x >= MinX && x <= MaxX && y >= MinY && y <= MaxY)
			{
				inside = true;
			}
			return inside;
		}

		public List<Tile> GetTiles()
		{
			int capacity = (MaxX - MinX + 1) * (MaxY - MinY + 1);
			List<Tile> tiles = new List<Tile>(capacity);
			for (int x = MinX; x <= MaxX; x++)
			{
				for (int y = MinY; y <= MaxY; y++)
				{
					tiles.Add(new Tile(x, y));
				}
			}
			return tiles;
		}

	}
}
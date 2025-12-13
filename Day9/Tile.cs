using System;
using System.Collections.Generic;
using System.Text;

namespace Day9
{
    public class Tile
    {
        private int x;
        private int y;

        public int X { get { return x; } }
        public int Y { get { return y; } }

		public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
		}

        public override bool Equals(object? obj)
        {
            return obj is Tile tile &&
                   X == tile.X &&
                   Y == tile.Y;
        }
    }
}

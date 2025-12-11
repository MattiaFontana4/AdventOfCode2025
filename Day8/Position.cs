using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
	public class Position
	{
		private int x;
		private int y;
		private int z;


		public Position(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public int X { get { return x; } }
		public int Y { get { return y; } }
		public int Z { get { return z; } }

		public double DistanceTo(Position other)
		{
			int deltaX = other.x - this.x;
			int deltaY = other.y - this.y;
			int deltaZ = other.z - this.z;
			var result = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);

			if (double.IsNaN(result))
				result = double.MaxValue;

			return result;
		}


		public double DistanceToZero() 
		{
			int deltaX = this.x;
			int deltaY = this.y;
			int deltaZ = this.z;
			var result = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
			return result;
		}

		public static double Distance(Position a, Position b)
		{
			return a.DistanceTo(b);
		}

		public override string ToString()
		{
			return $"Position({x}, {y}, {z})";
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (obj is Position other)
			{
				return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
			}
			return false;
		}

        public override int GetHashCode()
        {
			int hash;

			unsafe
			{
				double d = DistanceToZero();
				double* pd = &d;
				int* pi = (int*)pd;
				int lo = pi[0]; // parte bassa (dipende da endianness)
				int hi = pi[1]; // parte alta

				hash = hi + lo;
			}

			hash += this.X + this.Y + this.Z;

			return hash;
		}
	}
}

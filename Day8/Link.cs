using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
    public class Link : IComparable<Link> , IEquatable<Link>
    {
        private Position position1;
        private Position position2;

        private double distance;

        public Position Position1 { get { return position1; } }

        public Position Position2 { get { return position2; } }

        public double Distance { get { return distance; } }

        public (double, Position, Position) Get { get { return (distance, position1, position2); } }

        public Link(Position a, Position b)
        {
            ArgumentNullException.ThrowIfNull(a);
            ArgumentNullException.ThrowIfNull(b);

            position1 = a;
            position2 = b;

            distance = Position.Distance(a, b);
        }

        public override bool Equals(object? obj)
        {
            return obj is Link link &&
				   distance == link.distance &&
                   toEquals(link.position1,link.position2);
        }

        private bool toEquals(Position a, Position b) 
        {
            bool eq1 = Position.Equals(a, position1) && Position.Equals(b,position2);
			bool eq2 = Position.Equals(a, position2) && Position.Equals(b, position1);

			bool resut = eq1 || eq2;
			return resut;
        }

        public bool Equals(Position a, Position b)
        { 
            return toEquals(a, b); 
        }

        public int CompareTo(Link? other)
        {
            int result = 0;

            if (other == null)
            {
                result = 1;
            }
            else
            {
                if (other is Link link) 
                {
                    result = this.distance.CompareTo(link.distance);
                }
            }
            return result;
        }

        public override int GetHashCode()
        {
            int HashCode = ComputeHashCode(Position1,Position2);

            return HashCode;
        }

        public static int ComputeHashCode(Position a, Position b) 
        {
			int HashCode = 0;

			int hash1 = a.GetHashCode();
			int hash2 = b.GetHashCode();
			int result = hash1 ^ hash2;

			return HashCode;
		}

        public bool Equals(Link? other)
        {
            if (other == null)
                return false;

            bool result = distance == other.distance &&
                   toEquals(other.position1, other.position2);

            return result;
		}
    }
}

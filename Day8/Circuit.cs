using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day8
{
	public class Circuit
	{
		private Dictionary<Position, List<Position>> connections;

		public Circuit(Position position)
		{
			if (position is null)
			{
				throw new ArgumentNullException("position");
			}

			connections = new Dictionary<Position, List<Position>>();
			connections.Add(position, new List<Position>());
		}

		public void Add(Position pos, Position dest)
		{
			if (!connections.ContainsKey(dest))
			{
				throw new Exception("not found dest");
			}

			connections[dest].Add(pos);
			connections.Add(pos, new List<Position>());
			connections[pos].Add(dest);
		}

		public int GetLength()
		{
			return connections.Count;
		}

		public bool Contains(Position pos)
		{
			return connections.ContainsKey(pos);
		}

		public bool areNearly(Position posA, Position posB) 
		{
			bool result = false;

			//var posAFinded = positions.Find(posA);
			//if (posAFinded != null) 
			//{
			//	result = posB.Equals(posAFinded?.Next?.Value) || posB.Equals(posAFinded?.Previous?.Value);
			//}

			return result;
		}

		public void Merge(Circuit other, Position dest, Position destOther)
		{
			connections[dest].Add(destOther);
			connections.Add(destOther, new List<Position>());
			connections[destOther].Add(dest);

			foreach (var kvp in other.connections)
			{
				if (!kvp.Key.Equals(destOther))
				{
					connections.Add(kvp.Key, kvp.Value);
				}
			}
		}

        public override bool Equals(object? obj)
        {
            return obj is Circuit circuit &&
                   EqualityComparer<Dictionary<Position, List<Position>>>.Default.Equals(connections, circuit.connections);
        }
    }
}

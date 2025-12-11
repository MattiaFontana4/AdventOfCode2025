using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
	public class EngineFast
	{
		private List<Position> positions;


		private int productOfCircuits;

		public int ProductOfCircuits { get { return productOfCircuits; } }

		public EngineFast(List<Position> positions)
		{
			this.positions = positions;
		}

		public Link FindClosestPair(double last, Queue<Link> links)
		{
			double minDistance = double.MaxValue;
			Position closestA = null;
			Position closestB = null;

			for (int i = 0; i < positions.Count; i++)
			{
				Position posA = positions[i];
				for (int j = i + 1; j < positions.Count; j++)
				{
					Position posB = positions[j];

					double distance = posA.DistanceTo(posB);

					if (distance > last && distance < minDistance && distance != 0)
					{
						minDistance = distance;
						closestA = posA;
						closestB = posB;
					}
				}
			}

			if (closestA == null || closestB == null)
				throw new Exception("Dont find");

			return new Link(closestA, closestB);
		}


		public void Execute(int numberOfLinks)
		{
			Queue<Link> links = new Queue<Link>();
			double lastDistance = double.MinValue;

			for (int i = 0; i < numberOfLinks; i++)
			{
				var link = FindClosestPair(lastDistance, links);
				lastDistance = link.Distance;

				links.Enqueue(link);
			}

			IEnumerable<Circuit> circuits = BuildCircuit(links);

			var circuitsDim = circuits.Select(e => e.GetLength()).Order().TakeLast(3).ToList();
			productOfCircuits = 1;
			foreach (var dim in circuitsDim)
			{
				productOfCircuits *= dim;
			}

		}

        private IEnumerable<Circuit> BuildCircuit(Queue<Link> links)
        {
            var circuits = new List<Circuit>();

            while (links.Count > 0)
            {
				var link = links.Dequeue();

                Circuit circuit1 = circuits.Find(e => e.Contains(link.Position1));
                Circuit circuit2 = circuits.Find(e => e.Contains(link.Position2));

                if (circuit1 == null && circuit2 == null)
                {
                    Circuit newCircuit = new Circuit(link.Position2);
                    newCircuit.Add(link.Position1, link.Position2);
                    circuits.Add(newCircuit);
                }
                else if (circuit2 == null && circuit1 != null)
                {
                    circuit1.Add(link.Position2, link.Position1);
                }
                else if (circuit1 == null && circuit2 != null)
                {
                    circuit2.Add(link.Position1, link.Position2);
                }
                else if (circuit1 != null && circuit2 != null && !circuit1.Equals(circuit2))
                {
                    // Only call Merge if both circuitA and circuitB are not null and not the same instance
                    circuit1.Merge(circuit2, link.Position1, link.Position2);
                    circuits.Remove(circuit2);
                }
            }

            return circuits;
        }
    }
}

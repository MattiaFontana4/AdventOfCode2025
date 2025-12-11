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

		public int prodX;

		public int ProdX { get { return prodX; } }

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
			{
				throw new InvalidOperationException("No valid closest pair found.");
			}

			return new Link(closestA, closestB);
		}


		public void Execute(int numberOfLinks)
		{
			List<Link> links = BuildLinks();

			var orderdLinks = links.OrderBy(e => e.Distance);

			var topNPart1 = orderdLinks.Take(numberOfLinks);

			IEnumerable<Circuit> circuits = BuildCircuit(topNPart1);

			var circuitsDim = circuits.Select(e => e.GetLength()).Order().TakeLast(3).ToList();

			productOfCircuits = 1;
			foreach (var dim in circuitsDim)
			{
				productOfCircuits *= dim;
			}

			// Part 2
			Link lastLinkPositions = CompleteCircuit(orderdLinks.ToList());

			prodX = lastLinkPositions.Position1.X * lastLinkPositions.Position2.X;
		}

        private Link CompleteCircuit(List<Link> orderdLinks)
        {
			var circuits = new List<Circuit>(positions.Count);

			positions.ForEach(e => 
			{
				Circuit newCircuit = new Circuit(e);
				circuits.Add(newCircuit);
			});

			Link lastLink = null;
			int index = 0;

			while (circuits.Count > 1) 
			{
				lastLink = orderdLinks[index];

				ProcessLinkForCircuits(circuits, lastLink);

				index++;
			}

			return lastLink;
		}

        private List<Link> BuildLinks()
        {
			List<Link> links = new List<Link>(positions.Count * (positions.Count -1) / 2);

			for (int i = 0; i < positions.Count; i++) 
			{
				for (int j = i + 1; j < positions.Count; j++) 
				{
					links.Add(new Link(positions[i], positions[j]));
				}
			}

			return links;
        }

        private IEnumerable<Circuit> BuildCircuit(IEnumerable<Link> links)
        {
            var circuits = new List<Circuit>();

            foreach (var link in links)
            {
                ProcessLinkForCircuits(circuits, link);
            }

            return circuits;
        }

		private void ProcessLinkForCircuits(List<Circuit> circuits, Link link)
		{
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
	}
}

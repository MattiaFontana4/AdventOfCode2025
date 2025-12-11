using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
    public class Engine
    {
        private List<Position> positions;
        private List<Circuit> circuits;
        private List<(Position, Position)> ingoreTuple;

		private int productOfCircuits;

        public int ProductOfCircuits { get { return productOfCircuits; } }

		public Engine(List<Position> positions)
        {
            this.positions = positions;
            this.circuits = new List<Circuit>();
            this.ingoreTuple = new List<(Position, Position)>();
		}

        public (Position,Position) FindClosestPair()
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

					if (!circuits.Any(e => e.areNearly(posA,posB)) && !isIngnoreTuple(posA,posB))
					{
						double distance = posA.DistanceTo(posB);
						if (distance < minDistance && distance != 0)
						{
							minDistance = distance;
							closestA = posA;
							closestB = posB;
						}
					}
				}
			}

            return (closestA, closestB);
		}

        private bool isIngnoreTuple(Position posA, Position posB)
        {
            bool found = ingoreTuple.Any(e =>
                (e.Item1.Equals(posA) && e.Item2.Equals(posB)) ||
                (e.Item1.Equals(posB) && e.Item2.Equals(posA))
            );
            return found;
		}


		public void Execute(int numberOfLinks) 
        {
			for (int i = 0; i < numberOfLinks; i++)
            {
                var (posA, posB) = FindClosestPair();

                var circuitPosA = circuits.FirstOrDefault(e => e.Contains(posA));
                var circuitPosB = circuits.FirstOrDefault(e => e.Contains(posB));

                if (circuitPosA != null && circuitPosB != null)
                {
                    if (circuitPosA != circuitPosB)
                    {
                        circuitPosA.Merge(circuitPosB, posA, posB);

                        circuits.Remove(circuitPosB);
                    }
                    else 
                    {
						// both positions are already in the same circuit
                        this.ingoreTuple.Add((posA, posB));
					}
				}
				else if (circuitPosA != null)
                {
                    circuitPosA.Add(posB, posA);
                }
                else if (circuitPosB != null)
                {
                    circuitPosB.Add(posA, posB);
                }
                else
                {
                    Circuit newCircuit = new Circuit(posA);
                    newCircuit.Add(posB, posA);
					circuits.Add(newCircuit);
                }
			}

			var circuitsDim = circuits.Select(e => e.GetLength()).Order().TakeLast(3).ToList();
            productOfCircuits = 1;
            foreach (var dim in circuitsDim)
            {
                productOfCircuits *= dim;
			}

		}






	}
}

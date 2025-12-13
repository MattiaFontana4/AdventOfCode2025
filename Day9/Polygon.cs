using System;
using System.Collections.Generic;
using System.Text;

namespace Day9
{
    internal class Polygon
    {
        private List<Tile> _vertices;
        private List<Segment> _edges;

        public Polygon(List<Tile> vertices)
        {
            ArgumentNullException.ThrowIfNull(vertices);
            if (vertices.Count < 4)
            {
                throw new ArgumentException("A polygon must have at least 4 vertices.");
            }


            _vertices = vertices;
            _edges = new List<Segment>();

            for (int i = 0; i < vertices.Count; i++)
            {
                Tile start = vertices[i];
                Tile end = vertices[(i + 1) % vertices.Count];
                _edges.Add(new Segment(start, end));
            }
		}




        public bool Contains(Tile tile)
        {
            ArgumentNullException.ThrowIfNull(tile);
            bool result = false;

            if (_edges.Any(e => e.Contains(tile)))
            {
                result = true;
            }
            else 
            {
				int intersectionCount = 0;
				Segment ray = new Segment(tile, new Tile(int.MaxValue, tile.Y));

				foreach (Segment edge in _edges)
				{
					if (edge.Intersects(ray) && !edge.isVertex(tile))
					{
						intersectionCount++;
					}
				}

				result = (intersectionCount % 2) == 1;
			}

            return result;
		}

        public bool Contains(int x, int y)
        {
            Tile tile = new Tile(x, y);
            return this.Contains(tile);
        }

		public bool Contains(Polygon other)
        {
            ArgumentNullException.ThrowIfNull(other);
            foreach (Tile vertex in other._vertices)
            {
                if (!this.Contains(vertex))
                {
                    return false;
                }
            }
            return true;
		}

        public bool Contains(Box box)
        {
            bool result = true;
            ArgumentNullException.ThrowIfNull(box);

            var verticesBox = box.GetVertices();
            var boxSegments = box.GetSegments();

            bool areAllVerticesInside = verticesBox.All(e => this.Contains(e));
            bool areAllSermentsNonSecants = boxSegments.All(s => _edges.All(e => !e.Intersects(s)));

            result = areAllVerticesInside && areAllSermentsNonSecants;

            return result;
		}


	}
}

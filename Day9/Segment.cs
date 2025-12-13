using System;

namespace Day9
{
    public class Segment
    {
        private Tile _start;
        private Tile _end;

        private bool isHorizontal => _start.Y == _end.Y;
        private bool isVertical => _start.X == _end.X;

        public Segment(Tile tile1, Tile tile2)
        {
            ArgumentNullException.ThrowIfNull(tile1);
            ArgumentNullException.ThrowIfNull(tile2);

            if (!(tile1.X == tile2.X || tile1.Y == tile2.Y))
            {
                throw new ArgumentException("Segment must be either horizontal or vertical.");
            }
            else if (tile1.X == tile2.X && tile1.Y == tile2.Y)
            {
                throw new ArgumentException("Segment must have non-zero length.");
            }
            else if (tile1.X == tile2.X)
            {
                if (tile1.Y < tile2.Y)
                {
                    _start = tile1;
                    _end = tile2;
                }
                else
                {
                    _start = tile2;
                    _end = tile1;
                }
            }
            else if (tile1.Y == tile2.Y)
            {
                if (tile1.X < tile2.X)
                {
                    _start = tile1;
                    _end = tile2;
                }
                else
                {
                    _start = tile2;
                    _end = tile1;
                }
            }
            else
            {
                throw new ArgumentException("Segment must be either horizontal or vertical.");
            }
        }

        public bool Intersects(Segment other)
        {
            ArgumentNullException.ThrowIfNull(other);

            // Orthogonal intersection
            if (isHorizontal && other.isVertical)
            {
                // other.X is within [this.Xstart, this.Xend] and this.Y equals within other's Y range
                return other._start.X > _start.X && other._start.X < _end.X &&
                       _start.Y > other._start.Y && _start.Y < other._end.Y;
            }

            if (isVertical && other.isHorizontal)
            {
                return _start.X > other._start.X && _start.X < other._end.X &&
                       other._start.Y > _start.Y && other._start.Y < _end.Y;
            }

            // Parallel overlap (collinear and ranges overlap)
            if (isHorizontal && other.isHorizontal && _start.Y == other._start.Y)
            {
                // Overlap if ranges on X intersect
                return false;
                return _start.X <= other._end.X && other._start.X <= _end.X;
            }

            if (isVertical && other.isVertical && _start.X == other._start.X)
            {
                // Overlap if ranges on Y intersect
                return false;
				return _start.Y <= other._end.Y && other._start.Y <= _end.Y;
            }

            return false;
        }

        public bool Intersects(Tile tile)
        {
            ArgumentNullException.ThrowIfNull(tile);

            if (isHorizontal)
            {
                // Same Y, X within [start.X, end.X]
                return tile.Y == _start.Y && tile.X >= _start.X && tile.X <= _end.X;
            }

            if (isVertical)
            {
                // Same X, Y within [start.Y, end.Y]
                return tile.X == _start.X && tile.Y >= _start.Y && tile.Y <= _end.Y;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Segment"/> is parallel to this segment.
        /// </summary>
        /// <param name="other">The segment to compare.</param>
        /// <returns>true if the segments are parallel; otherwise, false.</returns>
        public bool IsParallel(Segment other)
        {
            ArgumentNullException.ThrowIfNull(other);

            if (isHorizontal && other.isHorizontal)
            {
                return true;
            }

            if (isVertical && other.isVertical)
            {
                return true;
            }

            return false;
        }

        int GetLength()
        {
            int length = 0;

            if (isHorizontal)
            {
                length = Math.Abs(_end.X - _start.X);
            }
            else if (isVertical)
            {
                length = Math.Abs(_end.Y - _start.Y);
            }

            return length;
        }

        public bool Contains(Tile tile) 
        {
            ArgumentNullException.ThrowIfNull(tile);

            bool result = false;

            if (isHorizontal) 
            {
                result = _start.Y == tile.Y && _start.X <= tile.X && _end.X >= tile.X;
            }
            else if (isVertical) 
            {
                result = _start.X == tile.X && _start.Y <= tile.Y && _end.Y >= tile.Y;
            }

            return result;
        }

        public bool isVertex(Tile tile) 
        {
            ArgumentNullException.ThrowIfNull(tile);

            bool result = tile.Equals(this._start) || tile.Equals(this._end);

            return result;
        }
    }
}

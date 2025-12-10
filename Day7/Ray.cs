using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    public class Ray
    {
        private int _startX;
		private int _startY;
        private int _endY;

        private bool _isBlocked;

        private int _actualY;
        private int _numTimelines;

        public Ray(int startX, int startY, int endY, int timelines = 1)
        {
            _startX = startX;
            
            _startY = startY;
			_actualY = startY;
            _numTimelines = timelines;

			_endY = endY;


            _isBlocked = false;
        }

        public int StartX { get { return _startX; } }
        public int StartY { get { return _startY; } }
        public int ActualY { get { return _actualY; } }
        public int EndY { get { return _endY; } }
        public int NumTimelines { get { return _numTimelines; } }

		public bool IsBlocked { get { return _isBlocked; } private set { _isBlocked = value; } }

        public void ExecuteStep()
        {
            if (!_isBlocked)
            {
                _actualY++;

                if (_actualY >= _endY)
                {
                    _isBlocked = true;
                }
            }
        }



        public bool isRayInside(Ray ray) 
        {
            bool result = false;

            if( this.StartX == ray.StartX )
            {
                if( this.StartY <= ray.StartY && this.ActualY >= ray.StartY )
                {
                    result = true;
                }
			}

			return result;
		}

        public void MergeTimelines(Ray ray)
        {
            _numTimelines += ray.NumTimelines;
		}

		public void Block()
        {
            _isBlocked = true;
        }

        public bool isBlockedAtPosition(int x, int y)
        {
            return (x == _startX && y == getNextY()) || (x == _startX && y == _actualY);
        }

        public int getNextY()
        {
            return _actualY + 1;
        }

        public bool isBlockedAtEnd()
        {
            return _actualY >= _endY;
        }

        public override bool Equals(object? obj)
        {
            return obj is Ray ray &&
                   StartX == ray.StartX &&
                   StartY == ray.StartY &&
                   ActualY == ray.ActualY &&
                   EndY == ray.EndY &&
                   IsBlocked == ray.IsBlocked &&
                   NumTimelines == ray.NumTimelines;
        }
    }
}

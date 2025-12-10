using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    internal class Splitter
    {
        int _positionX;
        int _positionY;
        public Splitter(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;
        }
        public int PositionX { get { return _positionX; } }
        public int PositionY { get { return _positionY; } }

        public bool isCurrentPosition(int x, int y)
        {
            return (x == _positionX && y == _positionY);
        }

        public Ray[] SplitRay(Ray incomingRay)
        {
            ArgumentNullException.ThrowIfNull(incomingRay, nameof(incomingRay));

            if (!isBlockingRay(incomingRay))
            {
                throw new InvalidOperationException("The incoming ray is not at the splitter position.");
            }

            Ray[] rays = new Ray[2];
            rays[0] = new Ray(incomingRay.StartX - 1, incomingRay.getNextY(), incomingRay.EndY);
            rays[1] = new Ray(incomingRay.StartX + 1, incomingRay.getNextY(), incomingRay.EndY);
            return rays;
        }

        public bool isBlockingRay(Ray incomingRay)
        {
            ArgumentNullException.ThrowIfNull(incomingRay, nameof(incomingRay));
            bool result = false;

            if (!incomingRay.IsBlocked) 
            {
                result = isCurrentPosition(incomingRay.StartX, incomingRay.getNextY()) || isCurrentPosition(incomingRay.StartX, incomingRay.ActualY);
			}

			return result;
		}
	}
}

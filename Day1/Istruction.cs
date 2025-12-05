using System;
using System.Collections.Generic;
using System.Text;

namespace Day1
{
    public class Istruction
    {
        public Istruction(string instruction)
        {
            if (instruction.StartsWith("R"))
            {
                Rotation = RotationSense.Right;
            }
            else if (instruction.StartsWith("L"))
            {
                Rotation = RotationSense.Left;
            }
            else
            {
                throw new ArgumentException("Invalid instruction format");
            }

            if (uint.TryParse(instruction[1..], out uint steps))
            {
                Steps = steps;
            }
            else
            {
                throw new ArgumentException("Invalid step count in instruction");
            }
		}

		public RotationSense Rotation { get; private set;}
        public uint Steps { get; private  set; }

	}
}

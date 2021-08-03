using System;
using System.Text.RegularExpressions;

namespace MarsRovers
{
    public class Rover : Coordinate
    {
        public Heading Z { get; private set; }

        public Rover(byte x, byte y, Heading z) : base(x, y)
        {
            Z = z;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }
    }
}

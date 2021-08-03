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

        public static bool TryParse(string s, out Rover result)
        {
            Regex pattern = new(@"^(\d)+ (\d)+ [NESW]$");

            if (pattern.IsMatch(s))
            {
                var roverProps = s.Split(' ');

                if (byte.TryParse(roverProps[0], out byte x) &&
                    byte.TryParse(roverProps[1], out byte y) &&
                    Enum.TryParse(roverProps[2], out Heading z))
                {
                    result = new Rover(x, y, z);
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}

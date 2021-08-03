using System.Text.RegularExpressions;

namespace MarsRovers
{
    public class Grid
    {
        public Coordinate Origin { get; } = new(0, 0);
        public Coordinate Bound { get; }

        public Grid(byte leftBound, byte upperBound)
        {
            Bound = new Coordinate(leftBound, upperBound);
        }

        public override string ToString()
        {
            return $"{Bound.X} {Bound.Y}";
        }

        public static bool TryParse(string s, out Grid result)
        {
            Regex pattern = new(@"^(\d)+ (\d)+$");

            if (pattern.IsMatch(s))
            {
                var bounds = s.Split(' ');

                if (byte.TryParse(bounds[0], out byte leftBound) &&
                    byte.TryParse(bounds[1], out byte rightBound))
                {
                    result = new Grid(leftBound, rightBound);
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}

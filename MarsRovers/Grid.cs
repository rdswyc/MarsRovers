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
    }
}

using System.Text.RegularExpressions;

namespace MarsRovers
{
    /// <summary>
    /// The exploration plateau for the app. The origin is a fixed coordinate, but the upper right boundary can be set.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Fixed, read-only origin boundary of the grid.
        /// </summary>
        public Coordinate Origin { get; } = new(0, 0);

        /// <summary>
        /// Read-only upper right boundary of the grid. This property is only set up at the class instantiation.
        /// </summary>
        public Coordinate Bound { get; }

        public Grid(byte leftBound, byte upperBound)
        {
            Bound = new Coordinate(leftBound, upperBound);
        }

        /// <summary>
        /// Helper override method to display a friendly string output of the grid.
        /// </summary>
        /// <returns>A string representation of the grid, consisting of the upper right bound coordinates.</returns>
        public override string ToString()
        {
            return $"{Bound.X} {Bound.Y}";
        }

        /// <summary>
        /// Helper static method to allow parsing of a string into a grid.
        /// It will parse based on the pattern X Y, where X and Y are numbers.
        /// </summary>
        /// <param name="s">The string representation of the grid.</param>
        /// <param name="result">The out grid variable to be instantiated.</param>
        /// <returns>True for a successful parse; false otherwise</returns>
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

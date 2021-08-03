using System;
using System.Text.RegularExpressions;

namespace MarsRovers
{
    /// <summary>
    /// An representation of each rover. It inherits coordinates and adds a heading property.
    /// It also has a move method based on the instructions available.
    /// </summary>
    public class Rover : Coordinate
    {
        /// <summary>
        /// The current heading direction of the rover.
        /// For immutability, it can only be set by this class, and not updated externally.
        /// </summary>
        public Heading Z { get; private set; }

        public Rover(byte x, byte y, Heading z) : base(x, y)
        {
            Z = z;
        }

        /// <summary>
        /// The move behavior for the hover, based on the instruction.
        /// If it is left or right, it should only turn. Otherwise, it will go forward.
        /// </summary>
        /// <param name="instruction">A move to instruct the rover.</param>
        public void Move(in Instruction instruction)
        {
            switch (instruction)
            {
                case Instruction.L:
                    TurnLeft();
                    break;
                case Instruction.M:
                    GoFoward();
                    break;
                case Instruction.R:
                    TurnRight();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Helper override method to display a friendly string output of the rover.
        /// </summary>
        /// <returns>A string representation of the rover, consisting of the coordinates and heading value.</returns>
        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }

        /// <summary>
        /// Private method to move the rover forward, based on it's actual heading value.
        /// </summary>
        private void GoFoward()
        {
            switch (Z)
            {
                case Heading.N:
                    Y++;
                    break;
                case Heading.E:
                    X++;
                    break;
                case Heading.S:
                    Y--;
                    break;
                case Heading.W:
                    X--;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Private method to update the heading of the rover when moving left.
        /// </summary>
        private void TurnLeft() => Z = Z == Heading.N ? Heading.W : Z - 1;

        /// <summary>
        /// Private method to update the heading of the rover when moving right.
        /// </summary>
        private void TurnRight() => Z = Z == Heading.W ? Heading.N : Z + 1;

        /// <summary>
        /// Helper static method to allow parsing of a string into a rover.
        /// It will parse based on the pattern X Y Z, where X and Y are numbers and Z is a heading character.
        /// </summary>
        /// <param name="s">The string representation of the rover.</param>
        /// <param name="result">The out rover variable to be instantiated.</param>
        /// <returns>True for a successful parse; false otherwise</returns>
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

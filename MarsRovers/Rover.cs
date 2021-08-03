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

        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }

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

        private void TurnLeft() => Z = Z == Heading.N ? Heading.W : Z - 1;
        private void TurnRight() => Z = Z == Heading.W ? Heading.N : Z + 1;

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

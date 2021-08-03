using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MarsRovers
{
    public class RoverNavigation
    {
        private readonly Grid _grid;
        private readonly Queue<Rover> _rovers = new();

        public RoverNavigation(Grid grid)
        {
            _grid = grid;
        }

        public void AddRover(Rover rover, string instructions)
        {
            if (IsInvalidRoverPosition(rover))
            {
                throw new RoverOutOfRangeException("Oops, the rover is set out of grid bounds.");
            }

            foreach (var move in instructions)
            {
                var direction = Enum.Parse<Instruction>(move.ToString());

                if (direction == Instruction.M && IsInvalidMoveForward(rover))
                {
                    throw new RoverOutOfRangeException("Oops, the move caused the rover to go out of the grid bounds.");
                }
                else
                {
                    rover.Move(direction);
                }
            }

            _rovers.Enqueue(rover);
        }

        public IEnumerable<string> RoverPositions()
        {
            while (_rovers.Count > 0)
            {
                var rover = _rovers.Peek();
                yield return rover.ToString();
                _rovers.Dequeue();
            }
        }

        private bool IsInvalidMoveForward(Rover rover)
        {
            return (rover.X == _grid.Origin.X && rover.Z == Heading.W) ||
                (rover.X == _grid.Bound.X && rover.Z == Heading.E) ||
                (rover.Y == _grid.Origin.Y && rover.Z == Heading.S) ||
                (rover.Y == _grid.Bound.Y && rover.Z == Heading.N);
        }

        private bool IsInvalidRoverPosition(Rover rover)
        {
            return rover.X < _grid.Origin.X ||
                rover.X > _grid.Bound.X ||
                rover.Y < _grid.Origin.Y ||
                rover.Y > _grid.Bound.Y;
        }
    }
}

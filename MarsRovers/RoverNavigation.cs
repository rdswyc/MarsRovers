using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MarsRovers
{
    /// <summary>
    /// Main application logic separated from the Main Program flow.
    /// It has a private Grid instance and a queue of the rovers.
    /// The choice for a queue is to be able to enqueue rovers sequentially, and discard them later after output (FIFO).
    /// </summary>
    public class RoverNavigation
    {
        private readonly Grid _grid;
        private readonly Queue<Rover> _rovers = new();

        public RoverNavigation(Grid grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// Method for adding rovers to the queue and move it according to the instructions given.
        /// </summary>
        /// <param name="rover">The rover instance to add to the queue.</param>
        /// <param name="instructions">The inscructions given to the rover.</param>
        /// <exception cref="RoverOutOfRangeException">Exception thrown in case the rover goes out of the grid bounds.</exception>
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

        /// <summary>
        /// Method to list the rovers output sequentially and dequeue them to free up memory after use.
        /// </summary>
        /// <returns>A collection of the rovers output as a string.</returns>
        public IEnumerable<string> RoverPositions()
        {
            while (_rovers.Count > 0)
            {
                var rover = _rovers.Peek();
                yield return rover.ToString();
                _rovers.Dequeue();
            }
        }

        /// <summary>
        /// Method to check for the validity of the next rover move, considering the grid bounds.
        /// </summary>
        /// <param name="rover">The rover to be moved.</param>
        /// <returns>True if the move would cause the rover to be out of the grid bound. False for a valid movement.</returns>
        private bool IsInvalidMoveForward(Rover rover)
        {
            return (rover.X == _grid.Origin.X && rover.Z == Heading.W) ||
                (rover.X == _grid.Bound.X && rover.Z == Heading.E) ||
                (rover.Y == _grid.Origin.Y && rover.Z == Heading.S) ||
                (rover.Y == _grid.Bound.Y && rover.Z == Heading.N);
        }

        /// <summary>
        /// Method to check for the validity of rover position, considering the grid bounds.
        /// </summary>
        /// <param name="rover">The rover to be validated.</param>
        /// <returns>True if the rover's position is out of the grid bounds. False if the rover is within the grid bounds.</returns>
        private bool IsInvalidRoverPosition(Rover rover)
        {
            return rover.X < _grid.Origin.X ||
                rover.X > _grid.Bound.X ||
                rover.Y < _grid.Origin.Y ||
                rover.Y > _grid.Bound.Y;
        }

        /// <summary>
        /// Static method to validate the move intructions string. It should only contain a sequence of L, M or R characters.
        /// </summary>
        /// <param name="instructions">The instructions string.</param>
        /// <returns>True if it only has the allowed characters. False otherwise.</returns>
        public static bool IsInvalidTurnMoveInstructions(string instructions)
        {
            Regex pattern = new(@"^[LMR]+$");
            return !pattern.IsMatch(instructions);
        }
    }
}

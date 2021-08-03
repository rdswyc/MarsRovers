using System;

namespace MarsRovers
{
    /// <summary>
    /// Custom exception to map cases where the rover is set outside the grid bounds or moves towards it.
    /// It includes the three constructors as a best practice:
    /// The parameterless, one with an error message and one with a message and inner exeption.
    /// </summary>
    public class RoverOutOfRangeException : Exception
    {
        public RoverOutOfRangeException()
        {
        }

        public RoverOutOfRangeException(string message)
            : base(message)
        {
        }

        public RoverOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

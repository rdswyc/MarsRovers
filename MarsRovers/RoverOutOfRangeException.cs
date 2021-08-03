using System;

namespace MarsRovers
{
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

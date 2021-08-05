namespace MarsRovers
{
    /// <summary>
    /// The coordinate base class with the X and Y pair.
    /// By design, this class is not abstract due to direct instantiation by other classes.
    /// It is also not a struct to allow inheritance.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// The X horizontal coordinate position limited from 0 to 255 units.
        /// For immutability, it can only be set by this or derived classes.
        /// </summary>
        public byte X { get; protected set; }

        /// <summary>
        /// The Y vertical coordinate position limited from 0 to 255 units.
        /// For immutability, it can only be set by this or derived classes.
        /// </summary>
        public byte Y { get; protected set; }

        public Coordinate(byte x, byte y)
        {
            X = x;
            Y = y;
        }
    }
}

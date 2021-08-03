namespace MarsRovers
{
    public class Coordinate
    {
        public byte X { get; protected set; }
        public byte Y { get; protected set; }

        public Coordinate(byte x, byte y)
        {
            X = x;
            Y = y;
        }
    }
}

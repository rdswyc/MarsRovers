using MarsRovers;
using Xunit;

namespace Tests
{
    public class RoverTest
    {
        [Theory]
        [InlineData("3 5 E")]
        [InlineData("0 0 N")]
        [InlineData("1 10 W")]
        [InlineData("200 100 S")]
        [InlineData("255 255 W")]
        public void TryParse_WithValidInput_ReturnTrue(string input)
        {
            var parse = Rover.TryParse(input, out Rover rover);

            Assert.True(parse);
        }

        [Theory]
        [InlineData("3 5 E", 3, 5, Heading.E)]
        [InlineData("0 0 N", 0, 0, Heading.N)]
        [InlineData("1 10 W", 1, 10, Heading.W)]
        [InlineData("200 100 S", 200, 100, Heading.S)]
        [InlineData("255 255 W", 255, 255, Heading.W)]
        public void TryParse_WithValidInput_ShouldSetRover(string input, byte x, byte y, Heading z)
        {
            Rover rover;

            Rover.TryParse(input, out rover);

            Assert.Equal(x, rover.X);
            Assert.Equal(y, rover.Y);
            Assert.Equal(z, rover.Z);
        }

        [Theory]
        [InlineData("0 o N")]
        [InlineData("1 -10 W")]
        [InlineData("200 100 L")]
        [InlineData("200N")]
        [InlineData("255 W")]
        public void TryParse_WithInvalidInput_ReturnFalse(string input)
        {
            var parse = Rover.TryParse(input, out Rover rover);

            Assert.False(parse);
        }

        [Theory]
        [InlineData("0 o N")]
        [InlineData("1 -10 W")]
        [InlineData("200 100 L")]
        [InlineData("200N")]
        [InlineData("255 W")]
        public void TryParse_WithInvalidInput_ShouldNotSetRover(string input)
        {
            Rover rover;

            Rover.TryParse(input, out rover);

            Assert.Null(rover);
        }
    }
}
